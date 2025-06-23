namespace JFramework
{
    public interface IConverter<T>
    {
        T Convert(byte[] bytes);
    }

    public class JBytesconverter : IConverter<byte[]>
    {
        byte[] IConverter<byte[]>.Convert(byte[] bytes)
        {
            return bytes;
        }
    }
}
