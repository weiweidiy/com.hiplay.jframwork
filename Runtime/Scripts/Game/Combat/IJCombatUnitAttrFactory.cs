using JFramework;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatUnitAttrFactory
    {
        List<IUnique> Create();
    }
}
