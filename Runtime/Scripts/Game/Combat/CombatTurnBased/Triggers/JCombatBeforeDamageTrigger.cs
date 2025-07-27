using System;

namespace JFramework.Game
{
    public class JCombatBeforeDamageTrigger : JCombatTriggerBase
    {
        IJCombatTargetable targetable;

        public JCombatBeforeDamageTrigger(float[] args) : base(args)
        {
        }

        protected override int GetValidArgsCount()
        {
            return 0; // 不需要参数
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            var casterUid = GetOwner().GetCaster();
            var caster = query.GetUnit(casterUid);
            targetable = caster as IJCombatTargetable;
            targetable.onBeforeDamage += OnBeforeDamage;
        }

        private void OnBeforeDamage(IJCombatTargetable targetable, IJCombatDamageData data)
        {
            TriggerOn(data);
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (targetable != null)
            {
                targetable.onBeforeDamage -= OnBeforeDamage;
                targetable = null;
            }
        }


    }


}
