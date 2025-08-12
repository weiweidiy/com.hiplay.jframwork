using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 伤害提升执行器，一定是伤害执行前，比如JCombatBeforeHurtTrigger触发
    /// </summary>
    public class JCombatExecutorDamageDataChange : JCombatExecutorBase
    {

        public JCombatExecutorDamageDataChange(IJCombatFilter filter, IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args) : base(filter,finder, formulua, args)
        {
        }

        protected override int GetValidArgsCount()
        {
            return 0; // 只需要一个参数，通常是伤害值或相关系数
        }


        protected override IJCobmatExecuteArgsHistroy DoExecute(IJCombatExecutorExecuteArgs executeArgs, IJCombatCasterTargetableUnit target)
        {
            var damageData = executeArgs.DamageData;
            if (damageData == null)
            {
                return new JCombatExecutorExecuteArgsHistroy();
            }

            var value = (float)damageData.GetDamage();
            formulua.CalcHitValue(null, ref value); // 假设伤害提升20%
            damageData.SetDamage((int)value);
            return new JCombatExecutorExecuteArgsHistroy() { DamageData = damageData};
        }
    }
}
