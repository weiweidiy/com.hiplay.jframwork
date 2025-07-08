using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatExecutor : IJCombatActionComponent /*IJCombatLifeCycle*/
    {
        void Execute( List<IJCombatUnit> targets);
    }
}
