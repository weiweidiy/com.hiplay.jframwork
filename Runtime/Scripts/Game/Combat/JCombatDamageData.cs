namespace JFramework.Game
{
    public class JCombatDamageData : JCombatExtraData, IJCombatDamageData
    {
        int damage;
        int damageType;
        public JCombatDamageData(string sourceUnitUid, string actionUid, int damage, int damageType) : base(sourceUnitUid, actionUid)
        {
            this.damage = damage;
            this.damageType = damageType;
        }

        public int GetDamage()
        {
            return damage;
        }

        public int GetDamageType()
        {
            return (int)damageType;
        }
    }
}
