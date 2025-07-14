using System;

namespace JFramework.Game
{
    public class JCombatTurnBasedEventRunner : BaseRunable, IDisposable
    {
        public virtual void Dispose()
        {
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            var combatEvents = (JCombatTurnBasedEvent)extraData.Data;
            var casterUid = combatEvents.CasterUid; //实现了icaster接口的对象

        }
    }
}
