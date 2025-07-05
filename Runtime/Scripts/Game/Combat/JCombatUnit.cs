using JFramework;
using JFramework.Game;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public class JCombatUnit : DictionaryContainer<IUnique>, IJCombatUnit
    {
        public string Uid { get; private set; }

        protected IJCombatAttrNameQuery combatAttrNameQuery;

        public JCombatUnit(string uid, List<IUnique> attrList,  Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery) : base(keySelector)
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

        public virtual void Start(IJCombatQuery query) { }


        public virtual void Stop() { }


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
