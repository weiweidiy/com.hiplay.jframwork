using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{

    public interface IJCombatTrigger : IJCombatActionComponent/*, IJCombatLifeCycle*/
    {
        event Action<List<IJCombatUnit>> onTriggerOn;

        bool IsTriggerOn(IJCombatQuery query, out List<IJCombatUnit> targets);

        
    }
}
