using JFrame.Game;

namespace JFramework
{
    public interface IJCombatLifeCycle
    {
        void OnStart(IJCombatQuery query);

        void OnStop();
    }
}

