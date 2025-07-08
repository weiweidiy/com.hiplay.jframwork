using System;
using System.Linq;

namespace JFramework.Game
{
    /// <summary>
    /// 回合制游戏战报生成
    /// </summary>
    public class JTurnBasedCombat : JCombat
    {
        /// <summary>
        /// 负责获取行动单位的接口
        /// </summary>
        IJCombatTurnBasedActionSelector actionSelector;

        /// <summary>
        /// 帧记录器
        /// </summary>
        IJCombatFrameRecorder frameRecorder;

        public JTurnBasedCombat(IJCombatTurnBasedActionSelector actionSelector, IJCombatFrameRecorder frameRecorder, IJCombatQuery jCombatQuery, IJCombatRunner jCombatRunner) : base(jCombatQuery, jCombatRunner)
        {
            this.actionSelector = actionSelector;
            this.frameRecorder = frameRecorder;

            var allUnit = jCombatQuery.GetUnits().OfType<IJCombatTurnBasedUnit>().ToList();
            actionSelector.SetUnits(allUnit);
        }

        public override void OnUpdate()
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
        protected virtual void DoUpdate(IJCombatFrameRecorder frameRecorder)
        {
            //子类重写：可以实现队伍技能更新

            //行动单位
            var actionUnit = actionSelector.PopActionUnit();
            actionUnit.OnUpdate();
        }


    }
}
