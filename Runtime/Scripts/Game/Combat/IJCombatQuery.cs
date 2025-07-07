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

        /// <summary>
        /// 获取单位的敌对队伍
        /// </summary>
        /// <param name="unitUid"></param>
        /// <returns></returns>
        List<IJCombatTeam> GetOppoTeams(string unitUid);


        IJCombatUnit GetUnit(string unitUid);

        List<IJCombatUnit> GetUnits();

        List<IJCombatUnit> GetUnits(Func<IJCombatUnit, bool> func);

        List<IJCombatUnit> GetUnits(string teamUid);
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
