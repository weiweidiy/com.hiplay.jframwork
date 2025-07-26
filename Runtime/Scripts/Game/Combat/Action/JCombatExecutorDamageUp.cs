using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 伤害提升执行器
    /// </summary>
    public class JCombatExecutorDamageUp : JCombatExecutorBase
    {
        public JCombatExecutorDamageUp(IJCombatTargetsFinder finder, IJCombatFormula formulua) : base(finder, formulua)
        {
        }

        protected override void DoExecute(object triggerArgs, List<IJCombatCasterTargetableUnit> finderTargets)
        {
            var damageData = triggerArgs as IJCombatDamageData;
            if(damageData == null)
            {
                throw new Exception("JCombatExecutorDamageUp: triggerArgs is not a valid IJCombatDamageData.");
            }

            var value = damageData.GetDamage();
            var newValue = value * 1.2f; // 假设伤害提升20%
            damageData.SetDamage((int)newValue);
        }
    }
}
