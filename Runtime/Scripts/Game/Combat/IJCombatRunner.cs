using System.Threading.Tasks;

namespace JFramework.Game
{
    public interface IJCombatRunner
    {
        void SetCombat(IJCombat combat);

        Task<IJCombatResult> RunCombat();
    }
}
