using JFramework;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatExecutor : IJCombatActionComponent, IJCombatLifeCycle
    {
        void Execute( List<IJCombatUnit> targets);
    }
}
