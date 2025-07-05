using System.Net.WebSockets;
using System;
using System.Threading.Tasks;


namespace JFramework
{
    public enum SocketStatusCodes
    {
        NormalClosure
    }

    public interface IJNetwork
    {
        event Action onOpen;
        event Action<SocketStatusCodes, string> onClose;
        event Action<IJNetMessage> onMessage;
        //event Action<byte[]> onBinary;
        event Action<string> onError;

        IJSocket Socket { get; }

        Task Connect(string url);

        void Disconnect();

        Task<TResponse> SendMessage<TResponse>(IJNetMessage pMsg, TimeSpan? timeout = null) where TResponse : class, IJNetMessage;

        bool IsConnecting();
    }
}