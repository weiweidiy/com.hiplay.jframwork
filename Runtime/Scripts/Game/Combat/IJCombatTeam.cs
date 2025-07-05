using JFramework;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatTeam : IUnique
    {
        List<IJCombatUnit> GetAllUnit();

        IJCombatUnit GetUnit(string uid);

        bool IsAllDead();
    }

}
