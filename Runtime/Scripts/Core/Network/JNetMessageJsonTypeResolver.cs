using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework
{
   /// <summary>
   ///
   /// </summary>
    public class JNetMessageJsonTypeResolver : IMessageTypeResolver
    {
        private readonly Dictionary<int, Type> messageTypes = new Dictionary<int, Type>();

        IJsonSerializer serializer;

        public JNetMessageJsonTypeResolver(IJsonSerializer serializer) { this.serializer = serializer; }
        public JNetMessageJsonTypeResolver RegisterMessageType(int messageId, Type messageType)
        {
            messageTypes[messageId] = messageType;
            return this;
        }

        public Type ResolveMessageType(byte[] data)
        {
            // 假设前4字节是消息ID
            //var messageId = BitConverter.ToInt32(data, 0);

            string json;
            try
            {
                json = Encoding.UTF8.GetString(data);
            }
            catch (DecoderFallbackException)
            {
                throw new Exception("Invalid UTF-8 data received.");
            }

            //to do: 这句有问题
            var obj = serializer.ToObject<JNetMessage>(json);
            var messageId = obj.TypeId;

            if (messageTypes.TryGetValue(messageId, out var type))
            {
                return type;
            }
            throw new InvalidOperationException($"Unknown message ID: {messageId}");
        }
    }
}