﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace JFramework.Game
{
    public class JCombatCasterTargetableUnit : RunableDictionaryContainer<IUnique>, IJCombatCasterTargetableUnit
    {
        public event Action<IJCombatCasterUnit, IJCombatAction> onCast;
        public event Action<IJCombatTargetable, IJCombatDamageData> onBeforeHurt;
        public event Action<IJCombatTargetable, IJCombatDamageData> onAfterHurt;
        public event Action<IJCombatCasterUnit, IJCombatDamageData> onBeforeHitting;
        public event Action<IJCombatCasterUnit, IJCombatDamageData> onAfterHitted;

        public void NotifyBeforeHitting( IJCombatDamageData data)
        {
            onBeforeHitting?.Invoke(this, data);
        }

        public void NotifyAfterHitted(IJCombatDamageData data)
        {
            onAfterHitted?.Invoke(this, data);
        }

        public void NotifyBeforeHurt( IJCombatDamageData data)
        {
            onBeforeHurt?.Invoke(this, data);
        }

        public void NotifyAfterHurt( IJCombatDamageData data)
        {
            onAfterHurt?.Invoke(this, data);
        }


        public string Uid { get; private set; }

        protected IJCombatAttrNameQuery combatAttrNameQuery;

        protected List<IJCombatAction> actions;

        protected List<GameAttributeInt> originAttrs;

        public JCombatCasterTargetableUnit(string uid, List<IUnique> attrList,  Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery, List<IJCombatAction> actions) : base(keySelector)
        {
            Utility utility = new Utility();
            try
            {
                originAttrs = utility.DeepClone(attrList.OfType<GameAttributeInt>().ToList());
            }
            catch(Exception ex)
            {
                originAttrs = new List<GameAttributeInt>();
            }
  

            AddRange(attrList);

            this.combatAttrNameQuery = combatAttrNameQuery;
            this.Uid = uid;


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

                //var triggers = action.GetTriggers();
                //if (triggers != null)
                //{
                //    eventListeners.InsertRange(0, triggers.OfType<IJCombatUnitEventListener>());
                //}
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

        public IUnique GetOriginAttribute(string uid)
        {
            return originAttrs.Where(attr => attr.Uid == uid).FirstOrDefault();
        }


        public int GetCurHp()
        {
            var attr = GetAttribute(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;
            return attr != null ? attr.CurValue : 0;
        }

        public int GetMaxHp()
        {
            var attr = GetAttribute(combatAttrNameQuery.GetMaxHpAttrName()) as GameAttributeInt;
            return attr != null ? attr.MaxValue : 0;
        }



        public bool IsDead()
        {
            var attr = GetAttribute(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;

            if (attr == null)
                return true;

            return attr.CurValue <= 0;
        }

        public int OnHurt(IJCombatDamageData damageData)
        {
            ////通知监听器
            //onBeforeHurt?.Invoke(this, damageData); // 触发事件监听器的伤害前事件，可以在这里处理一些逻辑，比如触发其他技能或效果。

            var attrHp = Get(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;
            var preValue = attrHp.CurValue;
            var damage = damageData.GetDamage();
            var curValue = attrHp.Minus(damage);

            //onAfterHurt?.Invoke(this, damageData); // 触发事件监听器的伤害后事件，可以在这里处理一些逻辑，比如触发其他技能或效果。

            return preValue - curValue;
        }
        #endregion

        #region 可释放技能接口
        public virtual void Cast() {
            if (actions == null)
                return;

            foreach (var action in actions)
            {
                onCast?.Invoke(this, action);
                action.Cast();
            }
        }

        public virtual bool CanCast()
        {
            return !IsDead();
        }






        #endregion
    }
}
