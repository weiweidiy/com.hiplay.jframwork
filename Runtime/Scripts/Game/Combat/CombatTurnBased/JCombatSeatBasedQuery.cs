using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatSeatBasedQuery : JCombatQuery, IJCombatSeatBasedQuery
    {
        Func<string, int> seatSelector;
        Func<IJCombatUnit, string> unitSelector;
        public JCombatSeatBasedQuery(Func<string,int> seatSelector,  Func<IJCombatTeam, string> keySelector, Func<IJCombatUnit, string> unitSelector, IJCombatFrameRecorder frameRecorder) : base(/*teams,*/ keySelector, frameRecorder)
        {
            this.seatSelector = seatSelector;
            this.unitSelector = unitSelector;
        }

        public JCombatSeatBasedQuery(IJCombatSeatDelegateBuilder delegateBuilder,  Func<IJCombatTeam, string> keySelector, Func<IJCombatUnit, string> unitSelector, IJCombatFrameRecorder frameRecorder) 
            : this(delegateBuilder.Build(), keySelector, unitSelector, frameRecorder)
        {
        }

        public JCombatSeatBasedQuery(IJCombatSeatDelegateBuilder delegateBuilder,IJCombatFrameRecorder frameRecorder) 
            : this(delegateBuilder.Build(), (team) => team.Uid, (unit) => unit.Uid, frameRecorder)
        {
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
