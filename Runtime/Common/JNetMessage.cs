namespace JFramework.Common
{
    public abstract class JNetMessage : IUnique
    {
        public abstract string Uid { get; }
        public string MessageType => GetType().Name;
    }
}
