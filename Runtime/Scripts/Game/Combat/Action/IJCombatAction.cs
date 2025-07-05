using JFramework;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatAction : IUnique
    {
        /// <summary>
        /// action 开始激活了，一般用于监听事件
        /// </summary>
        void Start(IJCombatQuery query);
        
        /// <summary>
        /// action 无效了，一般用于移除监听事件
        /// </summary>
        void Stop();

        /// <summary>
        /// 执行一次动作（触发，攻击）
        /// </summary>
        /// <param name="query"></param>
        void Act();

        /// <summary>
        /// 设置归属
        /// </summary>
        /// <param name="casterQuery"></param>
        void SetCaster(IJcombatUnitCasterQuery casterQuery);

        /// <summary>
        /// 获取释放者
        /// </summary>
        /// <returns></returns>
        string GetCaster();
    }
}
