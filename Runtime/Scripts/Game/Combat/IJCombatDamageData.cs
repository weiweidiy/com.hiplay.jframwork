namespace JFramework.Game
{
    public interface IJCombatDamageData 
    {
        /// <summary>
        /// 获取伤害值
        /// </summary>
        /// <returns></returns>
        int GetDamage();

        /// <summary>
        /// 伤害类型
        /// </summary>
        /// <returns></returns>
        int GetDamageType();    
    }
}
