using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatAcionInfo
    {
        string Uid { get; set; }

        List<IJCombatTrigger> Triggers { get; set; }

        List<IJCombatExecutor> Executors { get; set; }
    }
}
