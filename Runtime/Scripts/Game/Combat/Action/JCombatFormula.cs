namespace JFramework.Game
{
    public abstract class JCombatFormula : JCombatActionComponent, IJCombatFormula
    {
        public virtual bool IsHit(IJAttributeableUnit target)
        {
            // 默认实现，子类可以覆盖
            return true;
        }

        public abstract int CalcHitValue(IJAttributeableUnit target);
    }
}
