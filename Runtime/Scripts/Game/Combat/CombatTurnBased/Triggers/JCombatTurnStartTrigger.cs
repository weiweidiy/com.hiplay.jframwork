using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnStartTrigger : JCombatTriggerBase
    {
        /// <summary>
        /// 触发的回合数 
        /// </summary>
        int triggerTurn;

        /// <summary>
        /// 触发模式 0:每回合都触发，1:只在特定回合触发 2: 奇偶数触发
        /// </summary>
        int turnMode;
        public JCombatTurnStartTrigger(int triggerMode, int triggerTurn, float[] args) : base(args)
        {
            this.turnMode = triggerMode;
            this.triggerTurn = triggerTurn;
        }

        protected override int GetValidArgsCount()
        {
            return 0; // 不需要参数
        }

        //public override void OnTurnStart(int frame)
        //{
        //    base.OnTurnStart(frame);

        //    if (turnMode == 0)
        //    {
        //        TriggerOn(null);
        //        return;
        //    }

        //    if (turnMode == 1 && frame == triggerTurn)
        //    {
        //        TriggerOn(null);
        //        return;
        //    }

        //    if (turnMode == 2 && frame % 2 == triggerTurn) //riggerTurn为1时，奇数回合触发，为0时偶数回合触发
        //    {
        //        TriggerOn(null);
        //        return;
        //    }

        //}
    }
}
