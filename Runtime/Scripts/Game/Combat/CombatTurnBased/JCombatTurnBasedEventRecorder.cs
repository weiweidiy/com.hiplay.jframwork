using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedEventRecorder : DictionaryContainer<JCombatTurnBasedEvent>, IJCombatTurnBasedEventRecorder , IJCombatEventListener
    {
        IJCombatFrameRecorder frameRecorder;
        int curIndex;

        public JCombatTurnBasedEventRecorder(IJCombatFrameRecorder frameRecorder,  Func<JCombatTurnBasedEvent,string> keySelector):base(keySelector)
        { 
            this.frameRecorder = frameRecorder;
        }

        public JCombatTurnBasedEventRecorder(IJCombatFrameRecorder frameRecorder) : this(frameRecorder, (e) => e.Uid)
        {
        }

        public List<JCombatTurnBasedEvent> GetAllCombatEvents() => GetAll();

        public void OnDamage(IJCombatDamageData damageData)
        {
            //这个UID可能已经存在了，需要合并数据
            var dataUid = damageData.Uid;
            var combatEvent = Get(dataUid);
            //说明已经存在
            if (combatEvent.Uid != null && combatEvent.Uid != "")
            {
                //合并目标和伤害
                var lstTargetEffect = combatEvent.ActionEffect[CombatEventType.Damage.ToString()];
                lstTargetEffect.Add(new ActionEffectInfo() {TargetUid = damageData.GetTargetUid(), Value = damageData.GetDamage() });
                Update(combatEvent);
            }
            else
            {
                combatEvent = CreateEvent();
                combatEvent.Uid = dataUid;
                combatEvent.CurFrame = frameRecorder.GetCurFrame();
                combatEvent.CasterUid = damageData.GetCasterUid();
                combatEvent.CastActionUid = damageData.GetActionSourceUid();
                combatEvent.ActionEffect = new Dictionary<string, List<ActionEffectInfo>> ();
                var lstTargetEffect = new List<ActionEffectInfo>();
                lstTargetEffect.Add(new ActionEffectInfo() { TargetUid = damageData.GetTargetUid(), Value = damageData.GetDamage() });
                combatEvent.ActionEffect.Add(CombatEventType.Damage.ToString(), lstTargetEffect);
                Add(combatEvent);
            }        
        }

        JCombatTurnBasedEvent CreateEvent()
        {
            var combatEvent = new JCombatTurnBasedEvent();
            combatEvent.SortIndex = GetIndex();
            return combatEvent;
        }

        private int GetIndex()
        {
            return curIndex++;
        }
    }
}
