using System.Text;
using System.Threading.Tasks;

namespace JFrame.Game
{
    /// <summary>
    /// 战报式战斗对象
    /// </summary>
    public interface IJCombat
    {
        Task<IJCombatResult> GetResult();
    }
}
