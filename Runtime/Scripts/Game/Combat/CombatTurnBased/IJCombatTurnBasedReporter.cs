using System.Threading.Tasks;

namespace JFramework.Game
{
    public interface IJCombatTurnBasedReporter 
    {
        IJCombatTurnBasedReport GetReport();
    }
}
