namespace JFramework.Game
{
    public abstract class JCombatActionComponent : BaseRunable, IJCombatActionComponent
    {
        IJCombatAction owner;

        protected IJCombatQuery query;

        public IJCombatAction GetOwner() => owner;

        public virtual void SetOwner(IJCombatAction owner) => this.owner = owner;

        public virtual void SetQuery(IJCombatQuery query)
        {
            this.query = query;
        }
    }
}
