using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JFramework.Game
{

    /// <summary>
    /// 伤害执行器
    /// </summary>
    public class JCombatExecutorDamage : JCombatExecutorBase
    {


        public JCombatExecutorDamage(IJCombatFilter filter, IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args) : base(filter,finder, formulua, args)
        {
        }
        protected override int GetValidArgsCount()
        {
            return 0; // 只需要一个参数，通常是伤害值或相关系数
        }

        protected override IJCobmatExecuteArgsHistroy DoExecute(IJCombatExecutorExecuteArgs executeArgs, IJCombatCasterTargetableUnit target)
        {          
            //优先使用查找器找到的目标
            if (target != null)
            {
                return DoDamage(target);              
            }
            throw new Exception("JCombatExecutorDamage: No targets found for damage execution.");
        }

        IJCobmatExecuteArgsHistroy DoDamage(IJCombatCasterTargetableUnit target)
        {
            var executeArgsHistroy = new JCombatExecutorExecuteArgsHistroy();

            var uid = Guid.NewGuid().ToString();
            
            if(objEvent != null)
            {
                if (!objEvent.ActionEffect.ContainsKey(CombatEventType.Damage.ToString()))
                {
                    objEvent.ActionEffect.Add(CombatEventType.Damage.ToString(), new List<ActionEffectInfo>());
                }
            }

            float hitValue = 0;
            formulua.CalcHitValue(target, ref hitValue);

            var sourceUnitUid = GetOwner().GetCaster();
            var sourceActionUid = GetOwner().Uid;
            var data = new JCombatDamageData(uid, sourceUnitUid, sourceActionUid, (int)hitValue, 0, target.Uid);
            //设置执行历史记录
            executeArgsHistroy.DamageData = data;

            var sourceUnit = query.GetUnit(sourceUnitUid);
            var caster = sourceUnit as IJCombatCasterUnit;
            caster.NotifyBeforeHitting(data);
            target.NotifyBeforeHurt(data);

            // 受伤
            var minusHp = target.OnHurt(data);

            var logger = query.GetLogger();
            if (logger != null)
            {
                logger.Log( $"Frame: {query.GetCurFrame()}, Damage: {data.GetDamage()} from {sourceUnitUid} to {target.Uid}" +
                    $", hitValue: {hitValue}, minusHp: {minusHp} , targetHp: {target.GetCurHp()}");
            }

            caster.NotifyAfterHitted(data);
            target.NotifyAfterHurt(data);

            var casterTargetUnit = caster as IJCombatCasterTargetableUnit;

            if (objEvent != null)
            {
                var actionEffectInfos = objEvent.ActionEffect[CombatEventType.Damage.ToString()];
                actionEffectInfos.Add(new ActionEffectInfo()
                {
                    TargetUid = target.Uid,
                    Value = data.GetDamage()
                    ,
                    TargetHp = target.GetCurHp(),
                    TargetMaxHp = target.GetMaxHp()
                    ,
                    CasterHp = casterTargetUnit.GetCurHp(),
                    CasterMaxHp = casterTargetUnit.GetMaxHp()
                });
            }

            return executeArgsHistroy;
        }
    }
}
