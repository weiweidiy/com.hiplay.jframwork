using JFramework.Game;

namespace JFramework.Game
{
    public class JCombatExtraData : IJCombatExtraData
    {
        string sourceUnitUid;
        string actionUid;
        public JCombatExtraData(string sourceUnitUid, string actionUid)
        {
            this.actionUid = actionUid;
            this.sourceUnitUid = sourceUnitUid;
        }
        public string GetActionSourceUid()
        {
            return this.actionUid;
        }

        public string GetUnitSourceUid()
        {
            return this.sourceUnitUid;
        }
    }
}
