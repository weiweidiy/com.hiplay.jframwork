namespace JFramework.Game
{
    public interface IJCombatTurnBasedUnit : IJCombatUnit
    {
        /// <summary>
        /// 是否可行动
        /// </summary>
        /// <returns></returns>
        bool CanAction();

        /// <summary>
        /// 获取行动点，用于排序
        /// </summary>
        /// <returns></returns>
        int GetActionPoint();

        ///// <summary>
        ///// 开始行动
        ///// </summary>
        ///// <param name="jCombatQuery"></param>
        //void Act();
    }
}
