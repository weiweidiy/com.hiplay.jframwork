using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 基于战位的查找器
    /// </summary>
    public class JCombatDefaultFinder : JCombatActionComponent, IJCombatTargetsFinder
    {
        public List<IJCombatUnit> GetTargets(IJCombatQuery query)
        {
            var result = new List<IJCombatUnit>();

            var myUnitUid = GetOwner().GetCaster();

            //query.

            return result;
        }
    }
}
