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
    public class JCombatTurnBasedEvent : IUnique
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
        /// 行为效果, string=效果类型（伤害，加血等）, int=值
        /// </summary>
        public Dictionary<string, List<ActionEffectInfo>> ActionEffect { get; set; }

    }

    public struct ActionEffectInfo
    {
        public string TargetUid { get; set; }
        public int Value { get; set; }
        public int TargetHp { get; set; } // 目标当前血量
        public int TargetMaxHp { get; set; } // 目标最大血量

        public int CasterHp { get; set; } // 施法者当前血量
        public int CasterMaxHp { get; set; } // 施法者最大血量
    }
}
