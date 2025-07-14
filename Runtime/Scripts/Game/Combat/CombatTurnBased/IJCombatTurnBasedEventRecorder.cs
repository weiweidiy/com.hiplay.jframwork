﻿using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 战斗事件记录器
    /// </summary>
    public interface IJCombatTurnBasedEventRecorder
    {
        List<JCombatTurnBasedEvent> GetAllCombatEvents();
    }
}
