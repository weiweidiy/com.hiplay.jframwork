using System;

namespace JFramework.Game
{
    /// <summary>
    /// 有属性还可以释放
    /// </summary>
    public interface IJCombatCasterUnit : IJCombatUnit, IJCombatCaster
    {
        event Action<IJCombatCasterUnit, IJCombatAction> onCast;
    }
}
