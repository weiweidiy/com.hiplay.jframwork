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

<<<<<<< HEAD
        Task Connect(string url, string token = null);
=======
        Task Connect(string url);
>>>>>>> f8a4427db04b5a612039c0968aacfe5ba3a96f07

        void Disconnect();

        Task<TResponse> SendMessage<TResponse>(IJNetMessage pMsg, TimeSpan? timeout = null) where TResponse : class, IJNetMessage;

        bool IsConnecting();
    }
}