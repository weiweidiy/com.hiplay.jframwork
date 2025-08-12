using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatTriggerBase: JCombatActionComponent,  IJCombatTrigger/*, IJCombatUnitEventListener, IJCombatTurnBasedEventListener*/
    {
        public event System.Action<IJCombatTrigger, IJCombatExecutorExecuteArgs> onTriggerOn;

        bool isTriggerOn = false;

        protected IJCombatTargetsFinder finder;

        protected IJCombatExecutorExecuteArgs executeArgs = new ExecutorExecuteArgs();

        protected JCombatTriggerBase(float[] args, IJCombatTargetsFinder finder ) : base(args)
        {
            this.finder = finder;
        }

        public bool IsTriggerOn() => isTriggerOn;
        public void Reset() => isTriggerOn = false;
        public virtual void TriggerOn(IJCombatExecutorExecuteArgs triggerArgs)
        {
            isTriggerOn = true;
            onTriggerOn?.Invoke(this, triggerArgs);
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);
            if(finder != null)
            {
                finder.Start(extraData);
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (finder != null)
            {
                finder.Stop();
            }
        }

        protected override void OnUpdate(RunableExtraData extraData)
        {
            base.OnUpdate(extraData);
            if(finder != null)
            {
                finder.Update(extraData);
            }
        }

        public override void SetOwner(IJCombatAction owner)
        {
            base.SetOwner(owner);
            if (finder != null)
            {
                finder.SetOwner(owner);
            }
        }

        public override void SetQuery(IJCombatQuery query)
        {
            base.SetQuery(query);
            if (finder != null)
            {
                finder.SetQuery(query);
            }
        }
    }
}
