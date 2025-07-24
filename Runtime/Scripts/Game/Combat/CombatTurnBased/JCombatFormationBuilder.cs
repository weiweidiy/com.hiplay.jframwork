using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatFormationBuilder: IJCombatFormationBuilder
    {
        protected IJCombatUnitBuilder unitBuilder;

        List<JCombatFormationInfo> formationInfos = new List<JCombatFormationInfo>();
        public JCombatFormationBuilder(IJCombatUnitBuilder unitBuilder)
        {
            this.unitBuilder = unitBuilder;
        }

        public abstract List<JCombatFormationInfo> Build();
    }


}
