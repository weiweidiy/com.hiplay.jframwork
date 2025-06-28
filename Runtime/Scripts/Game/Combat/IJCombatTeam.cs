using JFramework;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatTeam
    {
        List<IJCombatUnit> GetAllUnit();

        bool IsAllDead();
    }

    public class JCombatTeam : DictionaryContainer<IJCombatUnit>, IJCombatTeam
    {
        public JCombatTeam(List<IJCombatUnit> units,  Func<IJCombatUnit, string> keySelector) : base(keySelector)
        {
            AddRange(units);
        }

        public List<IJCombatUnit> GetAllUnit()
        {
            return GetAll();
        }

        public bool IsAllDead()
        {
            var allUnits = GetAllUnit();

            foreach (var unit in allUnits)
            {
                if (!unit.IsDead())
                    return false;
            }

            return true;
        }
    }
}
