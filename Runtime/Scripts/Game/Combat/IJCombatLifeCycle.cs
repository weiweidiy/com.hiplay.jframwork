using JFramework.Game;

namespace JFramework
{
    public interface IJCombatLifeCycle
    {
        void OnStart(/*IJCombatQuery query*/);

        void OnUpdate();

        void OnStop();
    }
}

