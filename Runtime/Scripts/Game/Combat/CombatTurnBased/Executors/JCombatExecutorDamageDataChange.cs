using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 伤害提升执行器
    /// </summary>
    public class JCombatExecutorDamageDataChange : JCombatExecutorBase
    {
        public JCombatExecutorDamageDataChange(IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args) : base(finder, formulua, args)
        {
        }

        protected override int GetValidArgsCount()
        {
            return 0; // 只需要一个参数，通常是伤害值或相关系数
        }


        protected override void DoExecute(object triggerArgs, List<IJCombatCasterTargetableUnit> finderTargets)
        {
            var damageData = triggerArgs as IJCombatDamageData;
            if (damageData == null)
            {
                throw new Exception("JCombatExecutorDamageUp: triggerArgs is not a valid IJCombatDamageData.");
            }

            var value = damageData.GetDamage();
            var newValue = value * formulua.CalcHitValue(null); // 假设伤害提升20%
            damageData.SetDamage((int)newValue);
        }
    }
}
