using JFramework;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatEventRecorder : DictionaryContainer<CombatEvent>, IJCombatEventRecorder , IJCombatEventListener
    {
        IJCombatFrameRecorder frameRecorder;
        public JCombatEventRecorder(IJCombatFrameRecorder frameRecorder,  Func<CombatEvent,string> keySelector):base(keySelector)
        { 
            this.frameRecorder = frameRecorder;
        }

        public List<CombatEvent> GetAllCombatEvents() => GetAll();

        public void OnDamage(IJCombatDamageData damageData)
        {
            //这个UID可能已经存在了，需要合并数据
            var dataUid = damageData.Uid;
            var combatEvent = Get(dataUid);
            //说明已经存在
            if (combatEvent.Uid != null && combatEvent.Uid != "")
            {
                //合并目标和伤害
                var lstTargetEffect = combatEvent.ActionEffect[CombatEventType.Damage];
                lstTargetEffect.Add(new KeyValuePair<string, int>(damageData.GetTargetUid(), damageData.GetDamage()));
                Update(combatEvent);
            }
            else
            {
                combatEvent = new CombatEvent();
                combatEvent.Uid = dataUid;
                combatEvent.CurFrame = frameRecorder.GetCurFrame();
                combatEvent.CasterUid = damageData.GetCasterUid();
                combatEvent.CastActionUid = damageData.GetActionSourceUid();
                combatEvent.ActionEffect = new Dictionary<CombatEventType, List<KeyValuePair<string, int>>>();
                var lstTargetEffect = new List<KeyValuePair<string, int>>();
                lstTargetEffect.Add(new KeyValuePair<string, int>(damageData.GetTargetUid(), damageData.GetDamage()));
                combatEvent.ActionEffect.Add(CombatEventType.Damage, lstTargetEffect);
                Add(combatEvent);
            }        
        }
    }
}
