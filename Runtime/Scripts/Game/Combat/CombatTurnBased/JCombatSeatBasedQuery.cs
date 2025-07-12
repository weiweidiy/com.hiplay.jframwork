using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatSeatBasedQuery : JCombatQuery, IJCombatSeatBasedQuery
    {
        Func<string, int> seatSelector;
        Func<IJCombatUnit, string> unitSelector;
        public JCombatSeatBasedQuery(Func<string,int> seatSelector, /* List<IJCombatTeam> teams,*/ Func<IJCombatTeam, string> keySelector, Func<IJCombatUnit, string> unitSelector, IJCombatFrameRecorder frameRecorder) : base(/*teams,*/ keySelector, frameRecorder)
        {
            this.seatSelector = seatSelector;
            this.unitSelector = unitSelector;
        }

        /// <summary>
        /// 获取指定单位的座位号
        /// </summary>
        /// <param name="unitUid"></param>
        /// <returns></returns>
        public int GetSeat(string unitUid)
        {
            return seatSelector(unitUid);
        }

        public IJCombatCasterTargetableUnit GetUnit(IJCombatTeam team, int seat)
        {
            var units = GetUnits(_keySelector(team));
            foreach (var unit in units)
            {
                var uSeat = seatSelector(unitSelector(unit));
                if (uSeat == seat)
                    return unit as IJCombatCasterTargetableUnit;
            }

            return null;
        }
    }
}
