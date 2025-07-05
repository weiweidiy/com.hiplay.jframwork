using System.Collections.Generic;

namespace JFrame.Game
{

    public abstract class JCombatExecutorBase : JCombatActionComponent, IJCombatExecutor
    {
        IJCombatTargetsFinder finder;

        IJCombatQuery query;
        public JCombatExecutorBase(IJCombatTargetsFinder finder)
        {
            this.finder = finder;
        }

        public virtual void OnStart(IJCombatQuery query)
        {
            this.query = query; 
        }

        public virtual void OnStop()
        {
            //throw new System.NotImplementedException();
        }

        public void Execute(List<IJCombatUnit> targets)
        {
            var finalTargets = targets;
            if(finder != null)
            {
                finalTargets = finder.GetTargets(query);
            }

            DoExecute(finalTargets);
        }

        protected abstract void DoExecute(List<IJCombatUnit> finalTargets);

        public override void SetOwner(IJCombatAction owner)
        {
            base.SetOwner(owner);
            if(finder != null)
                finder.SetOwner(owner);
        }
    }
}
