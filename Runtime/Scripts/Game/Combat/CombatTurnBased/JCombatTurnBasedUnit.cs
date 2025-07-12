using JFramework;
using JFramework.Game;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedUnit : JCombatCasterTargetableUnit, IJCombatTurnBasedUnit
    {

        public JCombatTurnBasedUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery,  List<IJCombatAction> actions, IJCombatEventListener eventListener = null) 
            : base(uid, attrList, keySelector, combatAttrNameQuery, actions,eventListener)
        {

        }


        public int GetActionPoint()
        {
            var query = combatAttrNameQuery as IJCombatTurnBasedAttrNameQuery;
            var attr = GetAttribute(query.GetActionPointName());
            var attrInt = attr as GameAttributeInt;
            return attrInt.CurValue;
        }

        public override void Cast()
        {
            base.Cast();

            if (actions == null)
                return;

            foreach (var action in actions)
            {
                action.Cast();
            }
        }
    }
}
