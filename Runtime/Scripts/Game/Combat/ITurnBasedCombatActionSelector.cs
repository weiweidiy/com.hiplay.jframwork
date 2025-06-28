using JFramework;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 战斗行动的选择器 
    /// </summary>
    public interface ITurnBasedCombatActionSelector
    {
        /// <summary>
        /// 获取出手序列（已排序）
        /// </summary>
        /// <returns></returns>
        List<IJTurnBasedCombatUnit> GetActionUnits();
        /// <summary>
        /// 获取当前可行动的单位
        /// </summary>
        /// <returns></returns>
        IJTurnBasedCombatUnit PopActionUnit();

        /// <summary>
        /// 重置actionList
        /// </summary>
        void ResetActionUnits();

        /// <summary>
        /// 设置列表（未排序）
        /// </summary>
        /// <param name="units"></param>
        void SetUnits(List<IJTurnBasedCombatUnit> units);

        /// <summary>
        /// 是否全部完成了
        /// </summary>
        /// <returns></returns>
        bool IsAllComplete();
    }


    public class JCombatSpeedBasedActionSelector : DictionaryContainer<IJTurnBasedCombatUnit>, ITurnBasedCombatActionSelector
    {
        /// <summary>
        /// 排序后的行动顺序列表
        /// </summary>
        List<IJTurnBasedCombatUnit> actionList = new List<IJTurnBasedCombatUnit>();

        public JCombatSpeedBasedActionSelector(Func<IJCombatUnit, string> keySelector) : base(keySelector)
        {
            ///to do:需要监听ijcombatUnit属性变化，比如速度变化，要动态调整序列
        }

        public List<IJTurnBasedCombatUnit> GetActionUnits()
        {
            return actionList;
        }

        public void ResetActionUnits()
        {
            var lst = GetAll();
            lst.BinarySort(new Compare());
            actionList.Clear();
            actionList.AddRange(lst);
        }

        /// <summary>
        /// 全都完成了？排序不能行动的
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsAllComplete()
        {
            foreach (IJTurnBasedCombatUnit unit in actionList)
            {
                if (unit.CanAction())
                    return false;

            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IJTurnBasedCombatUnit PopActionUnit()
        {
            return actionList.PopFirst();
        }

        /// <summary>
        /// 设置unit列表，并重置action列表
        /// </summary>
        /// <param name="units"></param>
        public void SetUnits(List<IJTurnBasedCombatUnit> units)
        {
            AddRange(units);

            ResetActionUnits();
        }

        
        class Compare : IComparer<IJTurnBasedCombatUnit>
        {
            int IComparer<IJTurnBasedCombatUnit>.Compare(IJTurnBasedCombatUnit x, IJTurnBasedCombatUnit y)
            {

                if(x.GetActionPoint() > y.GetActionPoint())
                    return -1;

                if (x.GetActionPoint() < y.GetActionPoint())
                    return 1;

                return 0; //不稳定排序，可能交换位置的
            }
        }
    }
}
