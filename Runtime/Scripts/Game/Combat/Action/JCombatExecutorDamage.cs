using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatExecutorDamage : JCombatExecutorBase
    {
        public JCombatExecutorDamage(IJCombatQuery query, IJCombatTargetsFinder finder) : base(query, finder)
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
                    var data = new JCombatDamageData(sourceUnitUid, sourceActionUid, 20, 0);
                    target.OnDamage(data);
                }
            }
        }
    }
}
