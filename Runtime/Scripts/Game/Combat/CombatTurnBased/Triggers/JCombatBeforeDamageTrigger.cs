namespace JFramework.Game
{
    public class JCombatBeforeDamageTrigger : JCombatTriggerBase
    {
        public override void OnBeforeDamage(IJCombatDamageData damageData)
        {
            base.OnBeforeDamage(damageData);
            TriggerOn(damageData);
        }
    }
}
