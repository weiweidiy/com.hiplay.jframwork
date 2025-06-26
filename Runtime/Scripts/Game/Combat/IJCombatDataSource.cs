namespace JFrame.Game
{
    /// <summary>
    /// 战斗数据源
    /// </summary>
    public interface IJCombatDataSource
    {
        IJCombatTeam GetTeam(int teamId);
    }
}
