using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public class JCombatSeatBasedQuery : JCombatQuery, IJCombatSeatBasedQuery
    {
        Func<string, int> seatSelector;
        public JCombatSeatBasedQuery(Func<string,int> seatSelector,  List<IJCombatTeam> teams, Func<IJCombatTeam, string> keySelector, IJCombatFrameRecorder frameRecorder) : base(teams, keySelector, frameRecorder)
        {
            this.seatSelector = seatSelector;
        }

        public int GetSeat(string unitUid)
        {
            return seatSelector(unitUid);
        }
    }
}
