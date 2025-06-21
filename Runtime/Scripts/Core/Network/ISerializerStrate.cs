namespace JFramework
{
    public interface ISerializerStrate
    {
        byte[] Serlialize(IUnique obj);
        IUnique Deserialize(byte[] data);
    }

    /// <summary>
    /// 网络数据处理策略
    /// </summary>
    public interface INetworkMessageProcessStrate
    {
        byte[] ProcessOutMessage(IUnique message);

        IUnique ProcessComingMessage(byte[] data);
    }
}