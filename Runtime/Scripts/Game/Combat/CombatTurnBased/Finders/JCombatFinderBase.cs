namespace JFramework.Game
{
    public abstract class JCombatFinderBase : JCombatActionComponent, IJCombatTargetsFinder
    {
        protected IJCombatExecutorExecuteArgs executeArgs = new ExecutorExecuteArgs();
        public JCombatFinderBase(float[] args) : base(args)
        {

        }

        protected override int GetValidArgsCount()
        {
            return 0; // 不需要参数
        }

        public abstract IJCombatExecutorExecuteArgs GetTargetsData();
    }
}
