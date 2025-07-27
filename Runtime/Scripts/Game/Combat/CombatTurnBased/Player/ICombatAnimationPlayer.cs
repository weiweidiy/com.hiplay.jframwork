using System.Collections.Generic;
using System.Threading.Tasks;

namespace JFramework.Game
{
    /// <summary>
    /// 具体创建游戏对象的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICombatAnimationPlayer
    {
        Task Initialize<T>(JCombatTurnBasedReportData<T> reportData) where T : IJCombatUnitData;
        Task PlayAcion( string casterUid, string actionUid, Dictionary<string, List<ActionEffectInfo>> effect);

    }
}
