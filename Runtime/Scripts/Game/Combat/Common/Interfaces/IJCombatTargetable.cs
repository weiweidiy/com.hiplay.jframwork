using System;

namespace JFramework.Game
{
    /// <summary>
    /// 可作为目标的接口
    /// </summary>
    public interface IJCombatTargetable
    {
        event Action<IJCombatTargetable, IJCombatDamageData> onBeforeHurt;
        event Action<IJCombatTargetable, IJCombatDamageData> onAfterHurt;
        /// <summary>
        /// 收到伤害
        /// </summary>
        /// <param name="damageData"></param>
        int OnHurt(IJCombatDamageData damageData);

        int GetCurHp();

        int GetMaxHp();

        void NotifyBeforeHurt(IJCombatDamageData data);
        void NotifyAfterHurt(IJCombatDamageData data);
    }
}
