using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatExecutorDamage : JCombatExecutorBase
    {
        public JCombatExecutorDamage(IJCombatQuery query, IJCombatTargetsFinder finder) : base(query, finder)
        {
        }

        protected override void DoExecute(List<IJCombatCasterTargetableUnit> finalTargets)
        {
            if(finalTargets != null)
            {
                var uid = Guid.NewGuid().ToString();
                foreach(var target in finalTargets)
                {
                    var sourceUnitUid = GetOwner().GetCaster();
                    var sourceActionUid = GetOwner().Uid;
                    var data = new JCombatDamageData(uid, sourceUnitUid, sourceActionUid, 50, 0, target.Uid);
                    target.OnDamage(data);
                }
            }
        }
    }
}
