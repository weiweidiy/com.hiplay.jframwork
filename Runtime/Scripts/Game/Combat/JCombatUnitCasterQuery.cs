namespace JFrame.Game
{
    public class JCombatUnitCasterQuery : IJcombatUnitCasterQuery
    {
        IJCombatUnit caster;
        public JCombatUnitCasterQuery(IJCombatUnit caster)
        {
            this.caster = caster;
        }
        public string GetUnitUid()
        {
            return caster.Uid;
        }
    }
}
