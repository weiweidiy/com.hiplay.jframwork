using JFramework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JFrame.Game
{
    /// <summary>
    /// 判断战斗是否结束
    /// </summary>
    public class JCombatQuery : DictionaryContainer<IJCombatTeam>, IJCombatQuery
    {
        IJCombatFrameRecorder frameRecorder;

        IJCombatTeam winner;

        public JCombatQuery(List<IJCombatTeam> teams, Func<IJCombatTeam, string> keySelector, IJCombatFrameRecorder frameRecorder) : base(keySelector)
        {
            AddRange(teams);

            this.frameRecorder = frameRecorder;
        }


        public IJCombatTeam GetTeam(string teamUid)
        {
            throw new NotImplementedException();
        }

        public List<IJCombatTeam> GetTeams()
        {
            throw new NotImplementedException();
        }

        public IJCombatUnit GetUnit(string unitUid)
        {
            throw new NotImplementedException();
        }

        public List<IJCombatUnit> GetUnits()
        {
            throw new NotImplementedException();
        }

        public List<IJCombatUnit> GetUnits(IJCombatTeam team)
        {
            throw new NotImplementedException();
        }

        public List<IJCombatUnit> GetUnits(Func<IJCombatUnit, bool> func)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取胜利者队伍
        /// </summary>
        /// <returns></returns>
        public IJCombatTeam GetWinner()
        {
            return winner;
        }

        /// <summary>
        /// 战斗是否结束：剩余1对活着的，回合达到上限结束
        /// </summary>
        /// <returns></returns>
        public virtual bool IsCombatOver()
        {
            winner = null;

            if (frameRecorder.IsMaxFrame())
                return true;

            var teams = GetAll();
            var aliveTeamCount = teams.Count;
            foreach (var team in teams)
            {
                var allDead = team.IsAllDead();
                if (!allDead)
                    winner = team;
                else
                    aliveTeamCount -= 1;
            }

            if (aliveTeamCount != 1)
                winner = null;

            return aliveTeamCount <= 1;
        }

        public bool IsMaxFrame() => frameRecorder.IsMaxFrame();

        public int GetCurFrame() => frameRecorder.GetCurFrame();

        public int GetMaxFrame() => frameRecorder.GetMaxFrame();


    }
}
