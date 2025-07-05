using JFramework;
using JFramework.Game;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JFrame.Game
{
    public class JCombatTurnBasedUnit : JCombatUnit, IJCombatTurnBasedUnit
    {
        List<IJCombatAction> actions;
        public JCombatTurnBasedUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery, List<IJCombatAction> actions) : base(uid, attrList, keySelector, combatAttrNameQuery)
        {
            this.actions = actions;
            if(this.actions != null)
            {
                foreach(var  action in actions)
                    action.SetCaster(new JCombatUnitCasterQuery(this));
            }
        }

        public override void Start(IJCombatQuery query)
        {
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    
                    action.Start(query);
                }
            }

        }

        public override void Stop()
        {
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    action.Stop();
                }
            }
        }


        public void Act()
        {
            if (actions == null)
                return;

            foreach (var action in actions)
            {
                action.Act();
            }
        }

        public bool CanAction()
        {
            return true;
        }

        public int GetActionPoint()
        {
            var query = combatAttrNameQuery as IJCombatTurnBasedAttrNameQuery;
            var attr = GetAttribute(query.GetActionPointName());
            var attrInt = attr as GameAttributeInt;
            return attrInt.CurValue;
        }


    }
}
