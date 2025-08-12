namespace JFramework.Game
{
    public abstract class IJCombatFilterBase : JCombatActionComponent, IJCombatFilter
    {
        public IJCombatFilterBase(float[] args) : base(args)
        {
        }

        protected override int GetValidArgsCount()
        {
            return 0;
        }

        public abstract bool Filter(IJCombatExecutorExecuteArgs executeArgs, IJCombatCasterTargetableUnit target);
    }
}
