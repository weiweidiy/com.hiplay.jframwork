using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFramework.Game
{
    public interface ICombatAnimationPlayer
    {
        Task PlayAcion( string casterUid, string actionUid, Dictionary<string, List<ActionEffectInfo>> effect);

    }

    public class JCombatTurnBasedEventRunner : BaseRunable, IDisposable
    {
        ICombatAnimationPlayer AnimationPlayer { get; set; }

        public virtual void Dispose()
        {
        }

        public override async Task Start(RunableExtraData extraData, TaskCompletionSource<bool> tcs = null)
        {
            if (IsRunning)
            {
                throw new Exception(this.GetType().ToString() + " is running , can't run again! ");
            }
            this.ExtraData = extraData;
            this.IsRunning = true;

            await DoStart(extraData);         
        }

        protected async Task DoStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            var combatEvents = (JCombatTurnBasedEvent)extraData.Data;
            var casterUid = combatEvents.CasterUid; //实现了icaster接口的对象
            var curFrame = combatEvents.CurFrame;
            var actionUid = combatEvents.CastActionUid;
            

            var allTargetUids = combatEvents.ActionEffect
                .SelectMany(effect => effect.Value)
                .Select(actionEffectInfo => actionEffectInfo.TargetUid)
                .ToList();

            //播放技能动画等（监听动画命中事件，然后继续）
            await AnimationPlayer?.PlayAcion(casterUid, actionUid, combatEvents.ActionEffect);

            //combatEvents.ActionEffect.ToList().ForEach(effect =>
            //{
            //    var eventType = effect.Key;
            //    //将eventType转换成CombatEventType类型
            //    CombatEventType eventTypeEnum = (CombatEventType)Enum.Parse(typeof(CombatEventType), eventType);

            //    effect.Value.ForEach(actionEffectInfo =>
            //    {
            //        var targetUid = actionEffectInfo.TargetUid;
            //        var value = actionEffectInfo.Value;
            //        //根据类型在目标对象上 播放数值变化动画等

            //    });
            //});


            Stop();
        }
    }
}
