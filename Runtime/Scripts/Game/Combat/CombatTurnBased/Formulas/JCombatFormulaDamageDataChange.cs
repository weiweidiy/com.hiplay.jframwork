namespace JFramework.Game
{
    public class JCombatFormulaDamageDataChange : JCombatFormulaBase
    {
        public JCombatFormulaDamageDataChange(float[] args) : base(args)
        {
        }
        public override float CalcHitValue(IJAttributeableUnit target)
        {
            return GetArg(0);
        }

        protected override int GetValidArgsCount()
        {
            return 1;
        }
    }
}
