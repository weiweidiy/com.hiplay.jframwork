using JFramework;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{

    public interface IJCombatTrigger : IJCombatActionComponent, IJCombatLifeCycle
    {
        event Action<List<IJCombatUnit>> onTriggerOn;

        bool IsTriggerOn(IJCombatQuery query, out List<IJCombatUnit> targets);

        
    }
}
