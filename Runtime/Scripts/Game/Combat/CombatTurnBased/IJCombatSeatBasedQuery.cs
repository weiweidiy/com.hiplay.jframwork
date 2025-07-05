using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 基于站位的查询接口
    /// </summary>
    public interface IJCombatSeatBasedQuery
    {
        int GetSeat(string unitUid);
    }
}
