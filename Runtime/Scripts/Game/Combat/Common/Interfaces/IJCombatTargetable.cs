namespace JFramework.Game
{
    /// <summary>
    /// 可作为目标的接口
    /// </summary>
    public interface IJCombatTargetable
    {
        /// <summary>
        /// 收到伤害
        /// </summary>
        /// <param name="damageData"></param>
        int OnDamage(IJCombatDamageData damageData);
    }
}
