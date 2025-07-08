using JFramework;

namespace JFramework.Game
{
    public interface IJCombatActionComponent : IJCombatLifeCycle
    {
        void SetOwner(IJCombatAction owner);

        IJCombatAction GetOwner();
    }
}
