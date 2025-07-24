using System.Collections.Generic;

namespace JFramework.Game
{

    public abstract class JCombatExecutorBase : JCombatActionComponent, IJCombatExecutor
    {
        IJCombatTargetsFinder finder;

        protected IJCombatFormula formulua;

        public JCombatExecutorBase(IJCombatTargetsFinder finder, IJCombatFormula formulua) 
        {
            this.finder = finder;
            this.formulua = formulua;
        }

        public void Execute(List<IJCombatCasterTargetableUnit> targets)
        {
            var finalTargets = targets;
            if(finder != null)
            {
                finalTargets = finder.GetTargets();
            }

            DoExecute(finalTargets);
        }

        protected abstract void DoExecute(List<IJCombatCasterTargetableUnit> finalTargets);

        public override void SetOwner(IJCombatAction owner)
        {
            base.SetOwner(owner);
            if(finder != null)
                finder.SetOwner(owner);

            if(formulua != null)
            {
                formulua.SetOwner(owner);
            }
        }

        public override void SetQuery(IJCombatQuery query)
        {
            base.SetQuery(query);

            if (finder != null)
                finder.SetQuery(query);

            if (formulua != null)
            {
                formulua.SetQuery(query);
            }
        }
    }
}
