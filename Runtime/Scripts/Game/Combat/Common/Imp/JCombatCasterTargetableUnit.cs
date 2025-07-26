using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace JFramework.Game
{
    public class JCombatCasterTargetableUnit : RunableDictionaryContainer<IUnique>, IJCombatCasterTargetableUnit
    {
        public string Uid { get; private set; }

        protected IJCombatAttrNameQuery combatAttrNameQuery;


        protected List<IJCombatUnitEventListener> eventListeners;

        protected List<IJCombatAction> actions;

        public JCombatCasterTargetableUnit(string uid, List<IUnique> attrList,  Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery, List<IJCombatAction> actions, List<IJCombatUnitEventListener> eventListeners) : base(keySelector)
        {
            this.eventListeners = eventListeners;

            AddRange(attrList);

            this.combatAttrNameQuery = combatAttrNameQuery;
            this.Uid = uid;

            if (eventListeners == null)
            {
                eventListeners = new List<IJCombatUnitEventListener>();
            }

            this.actions = actions;
            if (this.actions != null)
            {
                foreach (var action in actions)
                {
                    action.SetCaster(this);
                }
                    
            }
        }


        public void SetQuery(IJCombatQuery jCombatQuery)
        {
            foreach(var action in actions)
            {
                action.SetQuery(jCombatQuery);

                var triggers = action.GetTriggers();
                if (triggers != null)
                {
                    eventListeners.InsertRange(0, triggers.OfType<IJCombatUnitEventListener>());
                }
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
            //通知监听器
            if(eventListeners !=null)
            {
                foreach (var listener in eventListeners)
                {
                    listener.OnBeforeDamage(damageData); // 触发事件监听器的伤害前事件，可以在这里处理一些逻辑，比如触发其他技能伤害加成、减免等。
                }
            }

           
            var attrHp = Get(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;
            var preValue = attrHp.CurValue;
            var damage = damageData.GetDamage();
            var curValue = attrHp.Minus(damage);

            //通知监听器
            if (eventListeners != null)
            {
                foreach (var listener in eventListeners)
                {
                    listener.OnAfterDamage(damageData); // 触发事件监听器的伤害后事件，可以在这里处理一些逻辑，比如触发其他技能或效果。
                }
            }
            
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
