namespace JFramework.Game
{
    public abstract class JCombatActionComponent : IJCombatActionComponent
    {
        IJCombatAction owner;

        protected IJCombatQuery query;

        public JCombatActionComponent(IJCombatQuery query)
        {
            this.query = query;
        }

        public IJCombatAction GetOwner() => owner;

        public virtual void SetOwner(IJCombatAction owner) => this.owner = owner;
        public virtual void OnStart() { }
        public virtual void OnStop() { }

        public virtual void OnUpdate() { }
    }
}
