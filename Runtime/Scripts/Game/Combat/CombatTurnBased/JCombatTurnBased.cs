using System;
using System.Collections.Generic;
using System.Linq;

namespace JFramework.Game
{
    /// <summary>
    /// 回合制游戏的逻辑帧（回合）更新逻辑
    /// </summary>
    public class JCombatTurnBased : JCombat
    {
        /// <summary>
        /// 负责获取行动单位的接口
        /// </summary>
        IJCombatTurnBasedCasterSelector casterSelector;

        /// <summary>
        /// 帧记录器
        /// </summary>
        IJCombatFrameRecorder frameRecorder;

        IJCombatQuery jCombatQuery;

       // List<IJCombatTurnBasedEventListener> listeners;

        bool firstUpdate = true;

        public JCombatTurnBased(IJCombatTurnBasedCasterSelector casterSelector, IJCombatFrameRecorder frameRecorder, IJCombatQuery jCombatQuery, List<IRunable> runables/*, List<IJCombatTurnBasedEventListener> listeners = null*/) : base(runables)
        {
            this.casterSelector = casterSelector;
            this.frameRecorder = frameRecorder;
            this.jCombatQuery = jCombatQuery;
            //this.listeners = listeners ?? new List<IJCombatTurnBasedEventListener>();
        }

        //public void AddListener(IJCombatTurnBasedEventListener listener)
        //{
        //    this.listeners.Add(listener);
        //}

        protected override void OnUpdate(RunableExtraData extraData)
        {
            //更新战斗 如果战斗没有决出胜负，则继续战斗
            while (!jCombatQuery.IsCombatOver())
            {
                //if (firstUpdate)
                //{
                //    foreach (var listener in listeners)
                //    {
                //        listener.OnTurnStart(frameRecorder.GetCurFrame());
                //    }

                //    firstUpdate = false;
                //}                
                //如果没有行动者了，回合数+1
                if (casterSelector.IsAllComplete())
                {

                    //foreach (var listener in listeners)
                    //{
                    //    listener.OnTurnEnd(frameRecorder.GetCurFrame());
                    //}

                    frameRecorder.NextFrame();

                    //foreach (var listener in listeners)
                    //{
                    //    listener.OnTurnStart(frameRecorder.GetCurFrame());
                    //}

                    casterSelector.ResetActionUnits();
                    continue;
                }

                DoUpdate();
            }
        }

        /// <summary>
        /// 一次行动一个单位
        /// </summary>
        /// <param name="frameRecorder"></param>
        protected virtual void DoUpdate()
        {
            //子类重写：可以实现队伍技能更新
            //行动单位
            var actionUnit = casterSelector.PopActionUnit();
            actionUnit.Cast();
        }


    }
}
