using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatExecutor : IJCombatActionComponent /*IJCombatLifeCycle*/
    {
        void AddCombatEvent(JCombatTurnBasedEvent combatEvent);

        void Execute(object triggerArgs);
 
    }
}
