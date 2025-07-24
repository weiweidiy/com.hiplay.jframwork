namespace JFramework.Game
{
    public interface IJCombatTurnBasedPlayer<T> : IJCombatPlayer where T : IJCombatUnitData
    {
        void Play(JCombatTurnBasedReportData<T> reportData);
    }
}
