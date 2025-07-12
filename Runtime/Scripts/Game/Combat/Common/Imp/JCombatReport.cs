using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatReportData
    {
        public string winnerTeamUid;
        public List<CombatEvent> events;
    }


    public class JCombatReport : IJCombatReport
    {
        List<CombatEvent> events;

        IJCombatTeam winner;

        public void SetCombatEvents(List<CombatEvent> events) => this.events = events;

        public void SetCombatWinner(IJCombatTeam team) => this.winner = team;

        public JCombatReportData GetCombatReportData()
        {
            var data = new JCombatReportData();

            data.winnerTeamUid = winner.Uid;
            data.events = events;

            return data;
        }
    }
}
