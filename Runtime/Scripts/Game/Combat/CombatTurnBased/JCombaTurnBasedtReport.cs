using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedReportData
    {
        public string winnerTeamUid;
        public List<JCombatTurnBasedEvent> events;
    }


    public class JCombaTurnBasedtReport : IJCombatTurnBasedReport
    {
        List<JCombatTurnBasedEvent> events;

        IJCombatTeam winner;

        public void SetCombatEvents(List<JCombatTurnBasedEvent> events) => this.events = events;

        public void SetCombatWinner(IJCombatTeam team) => this.winner = team;

        public JCombatTurnBasedReportData GetCombatReportData()
        {
            var data = new JCombatTurnBasedReportData();

            data.winnerTeamUid = winner.Uid;
            data.events = events;

            return data;
        }
    }
}
