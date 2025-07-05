using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatTargetsFinder : IJCombatActionComponent
    {
        List<IJCombatUnit> GetTargets(IJCombatQuery query);
    }
}
