using System;
using System.Linq;

namespace JFrame.Game
{

    /// <summary>
    /// 回合制游戏战报生成
    /// </summary>
    public class JTurnBasedCombat : JCombat
    {
        /// <summary>
        /// 负责获取行动单位的接口
        /// </summary>
        ITurnBasedCombatActionSelector actionSelector;

        /// <summary>
        /// 帧记录器
        /// </summary>
        IJCombatFrameRecorder frameRecorder;
        public JTurnBasedCombat(ITurnBasedCombatActionSelector actionSelector, IJCombatFrameRecorder frameRecorder, IJCombatQuery jCombatQuery, IJCombatEventRecorder eventRecorder, IJCombatResult jCombatResult) : base(jCombatQuery, eventRecorder, jCombatResult)
        {
            this.actionSelector = actionSelector;
            this.frameRecorder = frameRecorder;

            var allUnit = jCombatQuery.GetUnits().OfType<IJTurnBasedCombatUnit>().ToList();
            actionSelector.SetUnits(allUnit);
        }

        protected override void Update()
        {
            //更新战斗 如果战斗没有决出胜负，则继续战斗
            while (!jCombatQuery.IsCombatOver())
            {
                //如果没有行动者了，回合数+1
                if (actionSelector.IsAllComplete())
                {
                    frameRecorder.NextFrame();
                    actionSelector.ResetActionUnits();
                }

                DoUpdate(frameRecorder);
            }
        }

        /// <summary>
        /// 一次行动一个单位
        /// </summary>
        /// <param name="frameRecorder"></param>
        protected void DoUpdate(IJCombatFrameRecorder frameRecorder)
        {
            var actionUnit = actionSelector.PopActionUnit();

            actionUnit.Action(jCombatQuery);
        }
    }
}
