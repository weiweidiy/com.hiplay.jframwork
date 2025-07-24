using JFramework;
using JFramework.Game;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedUnit : JCombatCasterTargetableUnit, IJCombatTurnBasedUnit
    {
        IJCombatUnitInfo unitInfo;
        public JCombatTurnBasedUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery,  List<IJCombatAction> actions, IJCombatEventListener eventListener = null) 
            : base(uid, attrList, keySelector, combatAttrNameQuery, actions,eventListener)
        {

        }

        public JCombatTurnBasedUnit(IJCombatUnitInfo unitInfo, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery, IJCombatEventListener eventListener = null) 
            : this(unitInfo.Uid, unitInfo.AttrList, (u)=>u.Uid, combatAttrNameQuery, unitInfo.Actions, eventListener)
        {
            this.unitInfo = unitInfo;
        }

        public IJCombatUnitInfo GetUnitInfo()
        {
            return unitInfo;
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
