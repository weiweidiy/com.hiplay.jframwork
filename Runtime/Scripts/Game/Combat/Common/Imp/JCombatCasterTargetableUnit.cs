using System;
using System.Collections.Generic;

namespace JFramework.Game
{

    public class JCombatCasterTargetableUnit : RunableDictionaryContainer<IUnique>, IJCombatCasterTargetableUnit
    {
        public string Uid { get; private set; }

        protected IJCombatAttrNameQuery combatAttrNameQuery;


        protected IJCombatEventListener eventListener;

        protected List<IJCombatAction> actions;

        public JCombatCasterTargetableUnit(string uid, List<IUnique> attrList,  Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery, List<IJCombatAction> actions,  IJCombatEventListener eventListener) : base(keySelector)
        {
            this.eventListener = eventListener;

            AddRange(attrList);

            this.combatAttrNameQuery = combatAttrNameQuery;
            this.Uid = uid;

            this.actions = actions;
            if (this.actions != null)
            {
                foreach (var action in actions)
                    action.SetCaster(this);
            }
        }


        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);
            if (actions != null)
            {
                foreach (var action in actions)
                {

                    action.Start(extraData);
                }
            }
        }

        protected override void OnUpdate(RunableExtraData extraData)
        {
            base.OnUpdate(extraData);
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    action.Stop();
                }
            }
        }

        #region 可操作战斗属性接口
        public virtual IUnique GetAttribute(string uid)
        {
            var attr = Get(uid);
            return attr;
        }

        public bool IsDead()
        {
            var attr = GetAttribute(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;

            if (attr == null)
                return true;

            return attr.CurValue <= 0;
        }

        public int OnDamage(IJCombatDamageData damageData)
        {
            var attrHp = Get(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;
            var preValue = attrHp.CurValue;

            var damage = damageData.GetDamage();

            var curValue = attrHp.Minus(damage);

            eventListener?.OnDamage(damageData);

            return preValue - curValue;
        }
        #endregion

        #region 可释放技能接口
        public virtual void Cast() { }

        public virtual bool CanCast()
        {
            return !IsDead();
        }
        #endregion
    }
}
