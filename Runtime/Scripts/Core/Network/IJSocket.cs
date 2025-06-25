using System;

namespace JFramework
{
    public interface IJSocket
    {
        event Action<IJSocket> onOpen;
        event Action<IJSocket, SocketStatusCodes, string> onClosed;
        event Action<IJSocket, string> onError;
        event Action<IJSocket, byte[]> onBinary;
        //event Action<IJFrameworkSocket, string> onMessage;
        bool IsOpen { get; }
        void Init(string url);
        void Open();
        void Close();

        void Send(byte[] data);
    }
}
