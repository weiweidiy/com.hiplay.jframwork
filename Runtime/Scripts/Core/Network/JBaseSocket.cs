using System;
using JFramework.Common.Interface;

namespace JFramework
{
    public abstract class JBaseSocket : IJSocket
    {
        public event Action<IJSocket> onOpen;
        public event Action<IJSocket, string> onError;
        public event Action<IJSocket, byte[]> onBinary;
        public event Action<IJSocket, string> onMessage;
        public event Action<IJSocket, SocketStatusCodes, string> onClosed;

        public abstract bool IsOpen { get; }
        public abstract void Open();

        public abstract void Close();
        public abstract void Init(string url);
        public abstract void Send(byte[] data);
    }
}
