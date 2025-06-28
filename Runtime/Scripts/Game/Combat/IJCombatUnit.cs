namespace JFrame.Game
{
    public interface IJCombatUnit
    {
        /// <summary>
        /// 是否已死亡
        /// </summary>
        /// <returns></returns>
        bool IsDead();

        /// <summary>
        /// 是否可行动
        /// </summary>
        /// <returns></returns>
        bool CanAction();



        /// <summary>
        /// 开始行动
        /// </summary>
        /// <param name="jCombatQuery"></param>
        void Action(IJCombatQuery jCombatQuery);

    }

    public interface IJTurnBasedCombatUnit : IJCombatUnit
    {
        /// <summary>
        /// 获取行动点，用于排序
        /// </summary>
        /// <returns></returns>
        int GetActionPoint();
    }
}
