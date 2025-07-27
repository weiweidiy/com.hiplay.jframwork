namespace JFramework.Game
{
    public interface IJCombatFormula : IJCombatActionComponent
    {
        /// <summary>
        /// 计算数值
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        float CalcHitValue(IJAttributeableUnit target);

        bool IsHit(IJAttributeableUnit target);
    }
}
