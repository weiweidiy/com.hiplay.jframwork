using System.Collections.Generic;

namespace JFramework.Game
{

    public abstract class JCombatExecutorBase : JCombatActionComponent, IJCombatExecutor
    {
        IJCombatTargetsFinder finder;

        public JCombatExecutorBase(IJCombatQuery query, IJCombatTargetsFinder finder) : base(query) 
        {
            this.finder = finder;

        }

        public void Execute(List<IJCombatCasterTargetableUnit> targets)
        {
            var finalTargets = targets;
            if(finder != null)
            {
                finalTargets = finder.GetTargets(/*query*/);
            }

            DoExecute(finalTargets);
        }

        protected abstract void DoExecute(List<IJCombatCasterTargetableUnit> finalTargets);

        public override void SetOwner(IJCombatAction owner)
        {
            base.SetOwner(owner);
            if(finder != null)
                finder.SetOwner(owner);
        }
    }
}
