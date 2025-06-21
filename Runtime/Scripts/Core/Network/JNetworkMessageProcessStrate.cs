using System.Text;
using System;
using JFramework.Common;

namespace JFramework
{
    /// <summary>
    /// 网络消息处理策略基类
    /// </summary>
    public abstract class JNetworkMessageProcessStrate : INetworkMessageProcessStrate
    {
        /// <summary>
        /// 处理出去的消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] ProcessOutMessage(IUnique message)
        {
            //转json->byte[]
            var byteMsg = GetSerializerStrate().Serlialize(message);

            //数据处理（比如加密，编码等）
            return GetDataOutProcesser() != null ? GetDataOutProcesser().GetResult(byteMsg) : byteMsg;
        }

        /// <summary>
        /// 处理收到的消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IUnique ProcessComingMessage(byte[] data)
        {
            //数据加工           
            data = GetDataComingProcesser() != null ? GetDataComingProcesser().GetResult(data) : data;

            //处理接收的数据=>反序列化等
            string msg;
            try
            {
                msg = Encoding.UTF8.GetString(data);
            }
            catch (DecoderFallbackException)
            {
                throw new Exception("Invalid UTF-8 data received.");
            }


            return GetSerializerStrate().Deserialize(data);

        }

        /// <summary>
        /// 获取序列化工具，子类实现
        /// </summary>
        /// <returns></returns>
        public abstract ISerializerStrate GetSerializerStrate();

        /// <summary>
        /// 数据出去前的处理工具
        /// </summary>
        /// <returns></returns>
        public virtual JDataProcesserManager GetDataOutProcesser() => null;

        /// <summary>
        /// 数据进来时候的处理工具
        /// </summary>
        /// <returns></returns>
        public virtual JDataProcesserManager GetDataComingProcesser() => null;
    }
}