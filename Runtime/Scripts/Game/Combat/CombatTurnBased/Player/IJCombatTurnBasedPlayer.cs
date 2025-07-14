namespace JFramework.Game
{
    public interface IJCombatTurnBasedPlayer : IJCombatPlayer
    {
        void Play(JCombatTurnBasedReportData reportData);
    }
}
