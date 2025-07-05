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
            return Get(teamUid);
        }

        public List<IJCombatTeam> GetTeams()
        {
            return GetAll();
        }

        public IJCombatUnit GetUnit(string unitUid)
        {
            var teams = GetTeams();
            foreach(var team in teams)
            {
                var unit = team.GetUnit(unitUid);
                if (unit != null)
                    return unit;

            }
            return null;
        }

        public List<IJCombatUnit> GetUnits()
        {
            var result = new List<IJCombatUnit>();

            foreach(var team in GetTeams())
            {
                result.AddRange(team.GetAllUnit());
            }

            return result;
        }

        public List<IJCombatUnit> GetUnits(string teamUid)
        {
            var team = Get(teamUid);    
            return team.GetAllUnit();   
        }

        public List<IJCombatUnit> GetUnits(Func<IJCombatUnit, bool> func)
        {
            var units = GetUnits();
            var result = new List<IJCombatUnit>();

            foreach (var item in units)
            {
                if(func(item))
                    result.Add(item);
            }

            return result;
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
