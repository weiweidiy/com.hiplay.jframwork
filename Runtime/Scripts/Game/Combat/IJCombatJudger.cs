namespace JFrame.Game
{
    /// <summary>
    /// 战斗胜负判官
    /// </summary>
    public interface IJCombatJudger
    {
        bool IsCombatOver();

        IJCombatTeam GetWinner();
    }
}
