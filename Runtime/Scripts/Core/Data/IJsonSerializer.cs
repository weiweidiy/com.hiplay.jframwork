using System;

namespace JFramework
{
    public interface IJsonSerializer : IDeserializer
    {
        string ToJson(object obj);
    }

    public interface IDeserializer
    {
        T ToObject<T>(string str);

        object ToObject(string str, Type type);

        object ToObject(byte[] bytes, Type type);
    }
}
