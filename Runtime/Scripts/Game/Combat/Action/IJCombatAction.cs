using JFramework;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatAction : IUnique, IRunable, IJCombatCaster, IJCombatCastable
    {
        void SetQuery(IJCombatQuery jCombatQuery);

        List<IJCombatTrigger> GetTriggers();

        IJCombatAcionInfo GetActionInfo();
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
