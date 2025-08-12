using System.Threading.Tasks;

namespace JFramework.Game
{
    /// <summary>
    /// 游戏控制器基类，游戏业务逻辑写在这里
    /// </summary>
    public class BaseViewController : BaseRunable
    {
        protected EventManager eventManager;

        public BaseViewController(EventManager eventManager)
        {
            this.eventManager = eventManager;

        }

        public override Task Start(RunableExtraData extraData, TaskCompletionSource<bool> tcs = null)
        {
            base.Start(extraData, tcs);

            return Task.CompletedTask;
        }



        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        protected void SendEvent<T>(object arg) where T : Event, new()
        {
            eventManager.Raise<T>(arg);
        }

    }
}