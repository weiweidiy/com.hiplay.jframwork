using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 伤害执行器
    /// </summary>
    public class JCombatExecutorDamage : JCombatExecutorBase
    {
        public JCombatExecutorDamage(IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args) : base(finder, formulua, args)
        {
        }
        protected override int GetValidArgsCount()
        {
            return 0; // 只需要一个参数，通常是伤害值或相关系数
        }

        protected override void DoExecute(object triggerArgs, List<IJCombatCasterTargetableUnit> FinderTargets)
        {          
            //优先使用查找器找到的目标
            if (FinderTargets != null)
            {
                DoDamage(FinderTargets);
                return;
            }

            //触发器找到的目标
            var triggerTargets = triggerArgs as List<IJCombatCasterTargetableUnit>;
            if (triggerTargets != null)
            {
                DoDamage(triggerTargets);
                return;
            }

            throw new Exception("JCombatExecutorDamage: No targets found for damage execution.");
        }



        void DoDamage(List<IJCombatCasterTargetableUnit> finalTargets)
        {
            var uid = Guid.NewGuid().ToString();
            
            if(!objEvent.ActionEffect.ContainsKey(CombatEventType.Damage.ToString()))
            {
                objEvent.ActionEffect.Add(CombatEventType.Damage.ToString(), new List<ActionEffectInfo>());
            }

            foreach (var target in finalTargets)
            {
                var actionEffectInfos = objEvent.ActionEffect[CombatEventType.Damage.ToString()];

                var hitValue = formulua.CalcHitValue(target);
                var sourceUnitUid = GetOwner().GetCaster();
                var sourceActionUid = GetOwner().Uid;
                var data = new JCombatDamageData(uid, sourceUnitUid, sourceActionUid, (int)hitValue, 0, target.Uid);
                var minusHp = target.OnDamage(data);


                actionEffectInfos.Add(new ActionEffectInfo() { TargetUid = target.Uid, Value = data.GetDamage() });
            }
        }
    }
}
