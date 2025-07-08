using JFramework;
using JFramework.Game;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedUnit : JCombatUnit, IJCombatTurnBasedUnit
    {
        List<IJCombatAction> actions;
        public JCombatTurnBasedUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery, IJCombatQuery query, List<IJCombatAction> actions) : base(uid, attrList, keySelector, combatAttrNameQuery, query)
        {
            this.actions = actions;
            if(this.actions != null)
            {
                foreach(var  action in actions)
                    action.SetCaster(new JCombatUnitCasterQuery(this));
            }
        }

        public override void OnStart()
        {
            base.OnStart();
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    
                    action.OnStart(/*query*/);
                }
            }

        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (actions == null)
                return;

            foreach (var action in actions)
            {
                action.OnUpdate();
            }
        }

        public override void OnStop()
        {
            base.OnStop();
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    action.OnStop();
                }
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
