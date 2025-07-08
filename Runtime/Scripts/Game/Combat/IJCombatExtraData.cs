namespace JFramework.Game
{
    public interface IJCombatExtraData
    {
        /// <summary>
        /// 伤害源uid
        /// </summary>
        /// <returns></returns>
        string GetUnitSourceUid();

        /// <summary>
        /// 技能源
        /// </summary>
        /// <returns></returns>
        string GetActionSourceUid();
    }
}
