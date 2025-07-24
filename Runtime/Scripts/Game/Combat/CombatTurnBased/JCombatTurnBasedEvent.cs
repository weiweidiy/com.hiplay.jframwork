using System.Collections.Generic;

namespace JFramework.Game
{

    public enum CombatEventType
    {
        Damage,
    }
    /// <summary>
    /// 战斗事件
    /// </summary>
    public struct JCombatTurnBasedEvent : IUnique
    {
        public string Uid { get; set; }

        /// <summary>
        /// 索引，用来排序事件
        /// </summary>
        public int SortIndex { get; set; }
        /// <summary>
        /// 当前逻辑帧
        /// </summary>
        public int CurFrame { get; set; }

        /// <summary>
        /// 释放者uid: 可能是unit也可能是team , buffer等 , to do: 必须是ICombatCaster接口（可行动的接口）
        /// </summary>
        public string CasterUid { get; set; }

        /// <summary>
        /// 技能uid
        /// </summary>
        public string CastActionUid { get; set; }

        /// <summary>
        /// 行为效果, string=目标uid, int=值
        /// </summary>
        public Dictionary<string, List<ActionEffectInfo>> ActionEffect { get; set; }

    }

    public struct ActionEffectInfo
    {
        public string TargetUid { get; set; }
        public int Value { get; set; }
    }
}
