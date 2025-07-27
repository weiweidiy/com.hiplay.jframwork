using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatTriggerBase: JCombatActionComponent,  IJCombatTrigger/*, IJCombatUnitEventListener, IJCombatTurnBasedEventListener*/
    {
        public event System.Action<IJCombatTrigger, object> onTriggerOn;

        bool isTriggerOn = false;

        protected JCombatTriggerBase(float[] args) : base(args)
        {
        }

        public bool IsTriggerOn() => isTriggerOn;
        public void Reset() => isTriggerOn = false;
        public virtual void TriggerOn(object triggerArgs)
        {
            isTriggerOn = true;
            onTriggerOn?.Invoke(this, triggerArgs);
        }

        //public virtual void OnBeforeDamage(IJCombatDamageData damageData) { }
        //public virtual void OnAfterDamage(IJCombatDamageData damageData) { }
        //public virtual void OnTurnStart(int frame) { }
        //public virtual void OnTurnEnd(int frame) { }


    }
}
