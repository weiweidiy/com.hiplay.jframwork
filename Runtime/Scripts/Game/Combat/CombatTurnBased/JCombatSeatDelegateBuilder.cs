using System;
using System.Collections.Generic;
using System.Linq;

namespace JFramework.Game
{
    //public class JCombatSeatDelegateBuilder<T> : IJCombatSeatDelegateBuilder where T : JCombatUnitInfo
    //{
    //    List<JCombatFormationInfo<T>> formationInfos = new List<JCombatFormationInfo<T>>();
    //    public JCombatSeatDelegateBuilder(List<JCombatFormationInfo<T>> formationInfos)
    //    {
    //        this.formationInfos.AddRange(formationInfos);
    //    }
    //    public Func<string, int> Build()
    //    {
    //        return (unitUid) => // to do: 需要所有的战斗单位（包括NPC）
    //        {
    //            //从atkFormations中获取对应的阵型点位
    //            if (formationInfos == null || formationInfos.Count == 0)
    //            {
    //                throw new Exception("没有可用的阵型");
    //            }
    //            var formation = formationInfos.FirstOrDefault(f => f.UnitInfo.Uid == unitUid);
    //            return formation?.Point ?? -1; // 如果没有找到对应的阵型点位，返回-1
    //        };
    //    }
    //}
}
