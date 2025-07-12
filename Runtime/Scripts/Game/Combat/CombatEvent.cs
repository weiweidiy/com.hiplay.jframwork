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
    public struct CombatEvent : IUnique
    {
        public string Uid { get; set; }

        /// <summary>
        /// 当前逻辑帧
        /// </summary>
        public int CurFrame;

        /// <summary>
        /// 释放者uid: 可能是unit也可能是team , buffer等 , to do: 必须是ICombatCaster接口（可行动的接口）
        /// </summary>
        public string CasterUid;

        /// <summary>
        /// 技能uid
        /// </summary>
        public string CastActionUid;

        /// <summary>
        /// 行为效果, string=目标uid, int=值
        /// </summary>
        public Dictionary<CombatEventType, List<KeyValuePair<string, int>>> ActionEffect;

    }
}
