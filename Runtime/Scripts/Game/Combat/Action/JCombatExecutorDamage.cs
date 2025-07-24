using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatExecutorDamage : JCombatExecutorBase
    {
        public JCombatExecutorDamage(IJCombatTargetsFinder finder, IJCombatFormula formulua) : base(finder, formulua)
        {
        }

        protected override void DoExecute(List<IJCombatCasterTargetableUnit> finalTargets)
        {
            if(finalTargets != null)
            {
                var uid = Guid.NewGuid().ToString();
                foreach(var target in finalTargets)
                {
                    var hitValue = formulua.CalcHitValue(target);
                    var sourceUnitUid = GetOwner().GetCaster();
                    var sourceActionUid = GetOwner().Uid;
                    var data = new JCombatDamageData(uid, sourceUnitUid, sourceActionUid, hitValue, 0, target.Uid);
                    target.OnDamage(data);
                }
            }
        }
    }
}
