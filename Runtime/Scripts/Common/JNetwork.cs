using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
//using System.Reactive.Linq;
//using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JFramework.Common.Interface;

namespace JFramework.Common
{
    //public class MessageEvent<TMessage> where TMessage : JFrameworkNetMessage
    //{
    //    public event Action<TMessage> OnMessage;

    //    public void Invoke(TMessage message)
    //    {
    //        OnMessage?.Invoke(message);
    //    }
    //}

    public class JNetwork<TSocket> : IJNetwork where TSocket : IJSocket, new()
    {
        /// <summary>
        /// 接口事件
        /// </summary>
        public event Action onOpen;
        public event Action<SocketStatusCodes, string> onClose;
        public event Action<string> onMessage;
        public event Action<string> onError;

        //public string MessageNamespace { get; set; }
        /// <summary>
        /// 加密解密工具
        /// </summary>
        JDataProcesserManager msgEncode = null;
        JDataProcesserManager msgDecode = null;

        /// <summary>
        /// 请求返回的任务线程
        /// </summary>
        ConcurrentDictionary<string, TaskCompletionSource<string>> pendingResponses = new ConcurrentDictionary<string, TaskCompletionSource<string>>();

        /// <summary>
        /// 序列化工具
        /// </summary>
        ISerializer serializer;

        /// <summary>
        /// socket对象
        /// </summary>
        TSocket socket;

        //private readonly Subject<JFrameworkNetMessage> _messageStream = new Subject<JFrameworkNetMessage>();

        //public IObservable<TMessage> GetMessageStream<TMessage>() where TMessage : JFrameworkNetMessage
        //{
        //    return _messageStream.OfType<TMessage>();
        //}

        //private void Socket_OnBinary(IJFrameworkSocket s, byte[] data)
        //{
        //    var json = serializer.ToJson(data);
        //    var message = serializer.ToObject<JFrameworkNetMessage>(json);
        //    _messageStream.OnNext(message);
        //}


        //// 存储不同消息类型的事件
        //private readonly Dictionary<Type, object> messageEvents = new Dictionary<Type, object>();

        //// 注册事件
        //public void RegisterMessageHandler<TMessage>(Action<TMessage> handler) where TMessage : JFrameworkNetMessage
        //{
        //    if (!messageEvents.TryGetValue(typeof(TMessage), out var eventObj))
        //    {
        //        eventObj = new MessageEvent<TMessage>();
        //        messageEvents[typeof(TMessage)] = eventObj;
        //    }

        //    ((MessageEvent<TMessage>)eventObj).OnMessage += handler;
        //}

        //// 取消注册
        //public void UnregisterMessageHandler<TMessage>(Action<TMessage> handler)
        //    where TMessage : JFrameworkNetMessage
        //{
        //    if (messageEvents.TryGetValue(typeof(TMessage), out var eventObj))
        //    {
        //        ((MessageEvent<TMessage>)eventObj).OnMessage -= handler;
        //    }
        //}

        //// 触发事件
        //private void InvokeMessage<TMessage>(TMessage message)
        //    where TMessage : JFrameworkNetMessage
        //{
        //    if (messageEvents.TryGetValue(typeof(TMessage), out var eventObj))
        //    {
        //        ((MessageEvent<TMessage>)eventObj).Invoke(message);
        //    }
        //}



        #region 公开接口
        /// <summary>
        /// 发起连接，RPC调用风格，直接等待响应
        /// </summary>
        /// <param name="socketName"></param>
        /// <param name="url"></param>
        /// <param name="msgEncode"></param>
        /// <param name="msgDecode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Connect(string url)
        {

            var tcs = new TaskCompletionSource<bool>();
            //创建并初始化
            socket = new TSocket();
            socket.Init(url);


            //监听事件
            socket.onOpen += (s) => { Socket_OnOpen(s, tcs); };
            socket.onClosed += (s, code, message) => { Socket_OnClose(s, code, message); };
            socket.onBinary += (s, data) => { Socket_OnBinary(s, data); };
            //socket.onMessage += (s, message) => { Socket_OnMessage(s, message); };
            socket.onError += (s, message) => { Scoket_OnError(s, message, tcs); };

            //链接
            socket.Open();

            //等待链接成功：等待tcs.setresult(true)
            await tcs.Task;
        }


        public void Disconnect()
        {
            try
            {
                if (socket == null || !socket.IsOpen)
                    return;

                socket.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsConnecting()
        {
            if (socket == null)
                return false;

            return socket.IsOpen;
        }

        /// <summary>
        /// 发送消息， RPC风格调用
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="pMsg"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TResponse> SendMessage<TResponse>(JNetMessage pMsg, TimeSpan? timeout = null) where TResponse : JNetMessage
        {
            if (socket == null || !socket.IsOpen)
                throw new Exception("链接已断开，无法发送消息 socket");

            var tcs = new TaskCompletionSource<string>();
            if (!pendingResponses.TryAdd(pMsg.Uid, tcs)) // 使用 TryAdd 避免冲突
                throw new Exception("Duplicate UID detected.");

            try
            {
                //转json->byte[]
                var json = serializer.ToJson(pMsg);
                var byteMsg = Encoding.UTF8.GetBytes(json);

                //可能加密
                if (msgEncode != null)
                    byteMsg = msgEncode.GetResult(byteMsg);

                //发送
                socket.Send(byteMsg);

                // 使用 Task.WhenAny 实现超时
                var timeoutTask = Task.Delay(timeout ?? TimeSpan.FromSeconds(10));
                var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

                if (completedTask == timeoutTask)
                    throw new TimeoutException("Request timed out.");

                var resJson = await tcs.Task; // 等待直到 OnWebSocketMessage 调用 TrySetResult
                return serializer.ToObject<TResponse>(resJson);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
                throw ex;
            }
            finally
            {
                pendingResponses.TryRemove(pMsg.Uid, out _);
                //pendingResponses.Remove(pMsg.Uid); // 清理
                //cts.Dispose();
            }

        }
        #endregion

        #region 响应事件
        private void Scoket_OnError(IJSocket s, string message, TaskCompletionSource<bool> tcs)
        {
            tcs.SetException(new Exception(message));

            onError?.Invoke(message);
        }



        private void Socket_OnBinary(IJSocket s, byte[] data)
        {
            //数据加工
            if (msgDecode != null)
                data = msgDecode.GetResult(data);

            string msg;
            try
            {
                msg = Encoding.UTF8.GetString(data);
            }
            catch (DecoderFallbackException)
            {
                throw new Exception("Invalid UTF-8 data received.");
            }


            var obj = serializer.ToObject<JNetMessage>(msg);

            try
            {
                //如果没有tcs，那可能是一个推送消息
                if (pendingResponses.TryGetValue(obj.Uid, out var tcs))
                {
                    tcs.TrySetResult(msg); // 完成等待的任务
                }
            }
            catch (Exception ex)
            {
                // 处理解析错误
                Console.WriteLine($"Error parsing message: {ex.Message}");
            }

            onMessage?.Invoke(msg);

            //try
            //{
            //    // 解码数据
            //    if (msgDecode != null)
            //        data = msgDecode.GetResult(data);

            //    string json = Encoding.UTF8.GetString(data);
            //    var baseMsg = serializer.ToObject<JFrameworkNetMessage>(json);

            //    // (1) 如果是请求-响应消息
            //    if (pendingResponses.TryGetValue(baseMsg.Uid, out var tcs))
            //    {
            //        tcs.TrySetResult(json);
            //    }
            //    // (2) 否则尝试解析为结构化消息
            //    else
            //    {
            //        // 动态调用 InvokeMessage<T>
            //        // 这里需要反射或类型推断（简化版示例）
            //        try
            //        {
            //            // 假设消息类型可以从 JSON 的某个字段获取（例如 "MessageType"）
            //            var messageType = GetMessageTypeFromJson(json); // 需实现
            //            var method = typeof(JFrameNetwork<T>)
            //                .GetMethod(nameof(InvokeMessage), BindingFlags.NonPublic | BindingFlags.Instance)
            //                .MakeGenericMethod(messageType);

            //            var structuredMsg = serializer.ToObject(json, messageType);
            //            method.Invoke(this, new[] { structuredMsg });
            //        }
            //        catch (Exception ex)
            //        {
            //            onError?.Invoke($"Failed to parse pushed message: {ex.Message}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    onError?.Invoke($"Failed to process binary data: {ex.Message}");
            //}

        }

        //Type GetMessageTypeFromJson(string json)
        //{
        //    var baseMsg = serializer.ToObject<JFrameworkNetMessage>(json);
        //    return Type.GetType($"{MessageNamespace}.{baseMsg.MessageType}");
        //}

        private void Socket_OnClose(IJSocket s, SocketStatusCodes code, string message)
        {
            onClose?.Invoke(code, message);
        }

        private void Socket_OnOpen(IJSocket webSocket, TaskCompletionSource<bool> tcs)
        {
            tcs.SetResult(true);

            //在完成异步之后，再进行事件通知
            onOpen?.Invoke();
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="serializer">序列化器：用作对象转json</param>
        public JNetwork(ISerializer serializer, JDataProcesserManager msgEncode, JDataProcesserManager msgDecode)
        {
            this.serializer = serializer;
            this.msgEncode = msgEncode;
            this.msgDecode = msgDecode;
        }

        public JNetwork(ISerializer serializer) : this(serializer, null, null) { }
        #endregion

    }
}
