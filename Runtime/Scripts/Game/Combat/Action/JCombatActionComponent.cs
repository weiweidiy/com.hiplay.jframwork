namespace JFrame.Game
{
    public abstract class JCombatActionComponent : IJCombatActionComponent
    {
        IJCombatAction owner;
        public IJCombatAction GetOwner() => owner;

        public virtual void SetOwner(IJCombatAction owner) => this.owner = owner;   
    }
}
