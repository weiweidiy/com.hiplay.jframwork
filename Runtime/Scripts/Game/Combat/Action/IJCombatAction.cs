using JFramework;

namespace JFramework.Game
{
    public interface IJCombatAction : IUnique, IRunable, IJCombatCaster, IJCombatCastable
    {
        void SetQuery(IJCombatQuery jCombatQuery);
    }

    public interface IJCombatCastable
    {
        /// <summary>
        /// 设置归属
        /// </summary>
        /// <param name="casterQuery"></param>
        void SetCaster(IJCombatCaster caster);

        /// <summary>
        /// 获取释放者
        /// </summary>
        /// <returns></returns>
        string GetCaster();
    }
}
