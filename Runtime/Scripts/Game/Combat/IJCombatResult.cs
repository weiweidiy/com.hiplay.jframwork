using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 战斗结果，player就是读取这个对象
    /// </summary>
    public interface IJCombatResult
    {
        /// <summary>
        /// 设置所有战斗事件
        /// </summary>
        /// <param name="events"></param>
        void SetCombatEvents(List<IJCombatEvent> events);

        /// <summary>
        /// 设置胜利队伍
        /// </summary>
        /// <param name="team"></param>
        void SetCombatWinner(IJCombatTeam team);
    }
}
