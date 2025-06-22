using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
//using System.Reactive.Linq;
//using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JFramework.Common;
using JFramework.Common.Interface;

namespace JFramework
{
    public class JNetwork : IJNetwork
    {
        /// <summary>
        /// 接口事件
        /// </summary>
        public event Action onOpen;
        public event Action<SocketStatusCodes, string> onClose;
        public event Action<IJNetMessage> onMessage;
        public event Action<string> onError;

        /// <summary>
        /// socket对象
        /// </summary>
        IJSocket socket = null;

        /// <summary>
        /// 任务管理器
        /// </summary>
        IJTaskCompletionSourceManager<IUnique> taskManager = null;

        /// <summary>
        /// 消息处理策略
        /// </summary>
        INetworkMessageProcessStrate messageProcessStrate = null;


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
            try
            {
                InitSocket(url, tcs);
                GetSocket().Open();
                await tcs.Task;
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
                throw;
            }
        }

        /// <summary>
        /// 关闭链接
        /// </summary>
        public void Disconnect()
        {
            var socket = GetSocket();
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

        /// <summary>
        /// 是否链接
        /// </summary>
        /// <returns></returns>
        public bool IsConnecting()
        {
            var socket = GetSocket();

            if (socket == null)
                return false;

            return socket.IsOpen;
        }

        /// <summary>
        /// 发送消息， RPC风格调用， 如果像protobuf这种无法实现接口的，可以自己定义个适配器，实现iunique接口即可
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="pMsg"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TResponse> SendMessage<TResponse>(IJNetMessage pMsg, TimeSpan? timeout = null) where TResponse : class, IJNetMessage
        {
            var socket = GetSocket();

            if (socket == null || !socket.IsOpen)
                throw new Exception("链接已断开，无法发送消息 socket");

            //创建任务
            var tcs = GetTaskManager().AddTask(pMsg.Uid);
            if (tcs == null)
                throw new Exception("Duplicate UID detected.");

            try
            {
                //处理消息
                var byteMsg = GetNetworkMessageProcessStrate().ProcessOutMessage(pMsg);
                //发送
                socket.Send(byteMsg);

                //等待任务完成或者超时
                var result = await GetTaskManager().WaitingTask(pMsg.Uid, timeout); //可能超时

                return result as TResponse; // 等待直到 OnWebSocketMessage 调用 TrySetResult
            }
            catch (Exception ex)
            {
                GetTaskManager().SetException(pMsg.Uid, ex);
                throw ex;
            }
            finally
            {
                var result = GetTaskManager().RemoveTask(pMsg.Uid);
            }

        }
        #endregion

        #region 响应事件
        public void Scoket_OnError(IJSocket s, string message, TaskCompletionSource<bool> tcs)
        {
            tcs.SetException(new Exception(message));

            onError?.Invoke(message);
        }


        /// <summary>
        /// 收到消息了
        /// </summary>
        /// <param name="s"></param>
        /// <param name="data"></param>
        public void Socket_OnBinary(IJSocket s, byte[] data)
        {
            var obj = GetNetworkMessageProcessStrate().ProcessComingMessage(data);

            try
            {
                //如果没有tcs，那可能是一个推送消息
                var tcs = GetTaskManager().GetTask(obj.Uid);
                if (tcs != null)
                {
                    tcs.TrySetResult(obj); // 完成等待的任务
                }
            }
            catch (Exception ex)
            {
                // 处理解析错误
                Console.WriteLine($"Error parsing message: {ex.Message}");
            }

            onMessage?.Invoke(obj);

        }

        /// <summary>
        /// 链接关闭了
        /// </summary>
        /// <param name="s"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void Socket_OnClose(IJSocket s, SocketStatusCodes code, string message)
        {
            onClose?.Invoke(code, message);
        }

        /// <summary>
        /// 链接成功了
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="tcs"></param>
        public void Socket_OnOpen(IJSocket webSocket, TaskCompletionSource<bool> tcs)
        {
            tcs.SetResult(true);

            //在完成异步之后，再进行事件通知
            onOpen?.Invoke();
        }
        #endregion


        /// <summary>
        /// 创建socket
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tcs"></param>
        /// <returns></returns>
        void InitSocket(string url, TaskCompletionSource<bool> tcs)
        {
            var socket = GetSocket();
            socket.Init(url);

            //监听事件
            socket.onOpen += (s) => { Socket_OnOpen(s, tcs); };
            socket.onClosed += (s, code, message) => { Socket_OnClose(s, code, message); };
            socket.onBinary += (s, data) => { Socket_OnBinary(s, data); };
            //socket.onMessage += (s, message) => { Socket_OnMessage(s, message); };
            socket.onError += (s, message) => { Scoket_OnError(s, message, tcs); };
        }

        /// <summary>
        /// 获取socket
        /// </summary>
        /// <returns></returns>
        public IJSocket GetSocket() => socket;

        /// <summary>
        /// 获取任务管理器
        /// </summary>
        /// <returns></returns>
        public IJTaskCompletionSourceManager<IUnique> GetTaskManager() => taskManager;

        /// <summary>
        /// 获取消息处理策略对象
        /// </summary>
        /// <returns></returns>
        public INetworkMessageProcessStrate GetNetworkMessageProcessStrate() => messageProcessStrate;


        public JNetwork(IJSocket socket, IJTaskCompletionSourceManager<IUnique> taskManager, INetworkMessageProcessStrate messageProcessStrate)
        {
            this.socket = socket;
            this.taskManager = taskManager;
            this.messageProcessStrate = messageProcessStrate;
        }
    }
}
