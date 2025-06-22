using System;

namespace JFramework
{
    /// <summary>
    /// 网络消息处理策略基类
    /// </summary>
    public class JNetworkMessageProcessStrate : INetworkMessageProcessStrate
    {

        private readonly JDataProcesserManager outProcesser;
        private readonly JDataProcesserManager comingProcesser;
        private readonly INetMessageSerializerStrate serializer;
        private readonly IMessageTypeResolver typeResolver;

        /// <summary>
        /// 处理出去的消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] ProcessOutMessage(IJNetMessage message)
        {
            //转json->byte[]
            var byteMsg = GetSerializer().Serialize(message);

            //数据处理（比如加密，编码等）
            return GetDataOutProcesser() != null ? GetDataOutProcesser().GetResult(byteMsg) : byteMsg;
        }

        /// <summary>
        /// 处理收到的消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IJNetMessage ProcessComingMessage(byte[] data)
        {
            //数据加工           
            data = GetDataComingProcesser() != null ? GetDataComingProcesser().GetResult(data) : data;

            return GetSerializer().Deserialize(data, typeResolver);

        }

        /// <summary>
        /// 获取序列化工具，子类实现
        /// </summary>
        /// <returns></returns>
        public INetMessageSerializerStrate GetSerializer() => serializer;

        /// <summary>
        /// 数据出去前的处理工具
        /// </summary>
        /// <returns></returns>
        public virtual JDataProcesserManager GetDataOutProcesser() => outProcesser;

        /// <summary>
        /// 数据进来时候的处理工具
        /// </summary>
        /// <returns></returns>
        public virtual JDataProcesserManager GetDataComingProcesser() => comingProcesser;

        ///// <summary>
        ///// 序列化（应该可以扩展，比如json，或者protobuf等）
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public byte[] Serlialize(IUnique obj)
        //{
        //    var json = serializer.ToJson(obj);
        //    return Encoding.UTF8.GetBytes(json);
        //}

        ///// <summary>
        ///// 反序列化成对象（应该可以扩展，比如自定义json，protobuf等
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        ///// <exception cref="Exception"></exception>
        //public IUnique Deserialize(byte[] data)
        //{
        //    //处理接收的数据=>反序列化等()
        //    string msg;
        //    try
        //    {
        //        msg = Encoding.UTF8.GetString(data);
        //    }
        //    catch (DecoderFallbackException)
        //    {
        //        throw new Exception("Invalid UTF-8 data received.");
        //    }

        //    //to do: 要序列化成指定的类型
        //    return serializer.ToObject<IUnique>(msg);
        //}



        public JNetworkMessageProcessStrate(INetMessageSerializerStrate serializer, IMessageTypeResolver typeResolver, JDataProcesserManager outProcesser, JDataProcesserManager comingProcesser)
        {
            this.serializer = serializer;
            this.outProcesser = outProcesser;
            this.comingProcesser = comingProcesser;
        }
    }
}