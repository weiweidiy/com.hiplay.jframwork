using System;

namespace JFramework
{
    public interface IJsonSerializer
    {
        string ToJson(object obj);

        T ToObject<T>(string str);

        object ToObject(string json, Type type);
    }
}
