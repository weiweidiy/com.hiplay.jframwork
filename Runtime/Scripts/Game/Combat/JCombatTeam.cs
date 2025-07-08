using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTeam : DictionaryContainer<IJCombatUnit>, IJCombatTeam
    {
        public JCombatTeam(string uid, List<IJCombatUnit> units,  Func<IJCombatUnit, string> keySelector) : base(keySelector)
        {
            AddRange(units);
            this.Uid = uid;
        }

        public string Uid { get; protected set; }

        public List<IJCombatUnit> GetAllUnit()
        {
            return GetAll();
        }

        public IJCombatUnit GetUnit(string uid)
        {
            return Get(uid);
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
