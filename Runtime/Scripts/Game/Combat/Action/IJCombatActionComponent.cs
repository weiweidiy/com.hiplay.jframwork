namespace JFrame.Game
{
    public interface IJCombatActionComponent
    {
        void SetOwner(IJCombatAction owner);

        IJCombatAction GetOwner();
    }
}
