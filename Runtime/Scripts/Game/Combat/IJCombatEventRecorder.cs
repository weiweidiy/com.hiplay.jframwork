using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 战斗事件记录器
    /// </summary>
    public interface IJCombatEventRecorder
    {
        List<IJCombatEvent> GetAllCombatEvents();
    }
}
