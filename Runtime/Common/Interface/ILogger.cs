namespace JFramework.Common.Interface
{
    public interface ILogger
    {
        void Log(object message);  
        void LogError(object message);
    }
}
