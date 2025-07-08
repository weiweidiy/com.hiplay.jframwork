using JFramework;
using JFramework.Game;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatUnit : DictionaryContainer<IUnique>, IJCombatUnit
    {
        public string Uid { get; private set; }

        protected IJCombatAttrNameQuery combatAttrNameQuery;

        protected IJCombatQuery query;

        public JCombatUnit(string uid, List<IUnique> attrList,  Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery, IJCombatQuery query) : this(uid, attrList,keySelector, combatAttrNameQuery)
        {
            this.query = query;
        }

        public JCombatUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery) : base(keySelector)
        {
            AddRange(attrList);

            this.combatAttrNameQuery = combatAttrNameQuery;
            this.Uid = uid;
        }

        public IUnique GetAttribute(string uid)
        {
            var attr = Get(uid);
            return attr;
        }

        public bool IsDead()
        {
            var attr = Get(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;

            if (attr == null)
                return true;

            return attr.CurValue <= 0;
        }

        public virtual void OnStart() { }

        public virtual void OnUpdate() { }

        public virtual void OnStop() { }


        public int OnDamage(IJCombatDamageData damageData)
        {
            var attrHp = Get(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;
            var preValue = attrHp.CurValue;

            var damage = damageData.GetDamage();

            var curValue = attrHp.Minus(damage);

            return preValue - curValue;
        }
    }
}
