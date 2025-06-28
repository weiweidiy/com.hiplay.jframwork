using System;
using System.Collections.Generic;

namespace JFrame.Game
{

    /// <summary>
    /// 战斗
    /// </summary>
    public interface IJCombatQuery
    {
        #region 查询状态
        bool IsCombatOver();

        IJCombatTeam GetWinner();
        #endregion

        #region 查询战斗对象
        List<IJCombatTeam> GetTeams();

        IJCombatTeam GetTeam(string teamUid);

        IJCombatUnit GetUnit(string unitUid);

        List<IJCombatUnit> GetUnits();

        List<IJCombatUnit> GetUnits(Func<IJCombatUnit, bool> func);

        List<IJCombatUnit> GetUnits(IJCombatTeam team);
        #endregion

        #region 查询帧
        /// <summary>
        /// 获取当前帧
        /// </summary>
        /// <returns></returns>
        int GetCurFrame();

        /// <summary>
        /// 获取最大帧
        /// </summary>
        /// <returns></returns>
        int GetMaxFrame();

        /// <summary>
        /// 是否达到最大帧
        /// </summary>
        /// <returns></returns>
        bool IsMaxFrame();
        #endregion

    }
}
