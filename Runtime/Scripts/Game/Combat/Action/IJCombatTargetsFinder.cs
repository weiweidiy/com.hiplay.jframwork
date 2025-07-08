using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatTargetsFinder : IJCombatActionComponent
    {
        List<IJCombatUnit> GetTargets(/*IJCombatQuery query*/);
    }
}
