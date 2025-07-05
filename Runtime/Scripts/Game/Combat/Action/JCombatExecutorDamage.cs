using System.Collections.Generic;

namespace JFrame.Game
{
    public class JCombatExecutorDamage : JCombatExecutorBase
    {
        public JCombatExecutorDamage(IJCombatTargetsFinder finder) : base(finder)
        {
        }

        protected override void DoExecute(List<IJCombatUnit> finalTargets)
        {
            if(finalTargets != null)
            {
                foreach(var target in finalTargets)
                {
                    var sourceUnitUid = GetOwner().GetCaster();
                    var sourceActionUid = GetOwner().Uid;
                    var data = new JCombatDamageData(sourceUnitUid, sourceActionUid, 5, 0);
                    target.OnDamage(data);
                }
            }
        }
    }
}
