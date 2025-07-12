using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatTargetsFinder : IJCombatActionComponent
    {
        List<IJCombatCasterTargetableUnit> GetTargets(/*IJCombatQuery query*/);
    }
}
