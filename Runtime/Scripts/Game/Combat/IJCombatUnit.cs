using JFramework;

namespace JFramework.Game
{

    /// <summary>
    /// 可战斗单位接口
    /// </summary>
    public interface IJCombatUnit : IUnique , IJCombatLifeCycle
    {
        /// <summary>
        /// 是否已死亡
        /// </summary>
        /// <returns></returns>
        bool IsDead();

        /// <summary>
        /// 获取属性对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uid"></param>
        /// <returns></returns>
        IUnique GetAttribute(string uid);

        /// <summary>
        /// 收到伤害
        /// </summary>
        /// <param name="damageData"></param>
        int OnDamage(IJCombatDamageData damageData);

    }
}
