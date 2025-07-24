﻿using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatUnitData
    {
        string Uid { get; set; }
        int Seat { get; set; }
    }

    public class JCombatUnitData : IJCombatUnitData
    {
        public string Uid { get; set; }
        public int Seat { get; set; }
    }


    public class JCombatTurnBasedReportData<T> where T : IJCombatUnitData
    {
        public Dictionary<string, List<T>> FormationData { get; set; }
        public string winnerTeamUid { get; set; }
        public List<JCombatTurnBasedEvent> events { get; set; }
    }
    //{
    //    public Dictionary<string, List<JCombatUnitData>> FormationData { get; set; }
    //    public string winnerTeamUid { get; set; }
    //    public List<JCombatTurnBasedEvent> events { get; set; }
    //}


    public abstract class JCombatTurnBasedReportBuilder : IJCombatTurnBasedReportBuilder
    {
        List<JCombatTurnBasedEvent> events;

        IJCombatTeam winner;

        List<IJCombatTeam> teams;

        protected IJCombatSeatBasedQuery seatQuery;

        public JCombatTurnBasedReportBuilder(IJCombatSeatBasedQuery seatQuery)
        {
            this.seatQuery = seatQuery ?? throw new ArgumentNullException(nameof(seatQuery));
        }

        public void SetForamtionData(List<IJCombatTeam> teams)=>this.teams = teams;

        public void SetCombatEvents(List<JCombatTurnBasedEvent> events) => this.events = events;

        public void SetCombatWinner(IJCombatTeam team) => this.winner = team;

        public JCombatTurnBasedReportData<T> GetCombatReportData<T>() where T : class, IJCombatUnitData
        {
            var data = new JCombatTurnBasedReportData<T>();

            data.FormationData = GetFormationData<T>();
            data.winnerTeamUid = winner?.Uid ?? null;
            data.events = events;

            return data;
        }

        private Dictionary<string, List<T>> GetFormationData<T>() where T : class, IJCombatUnitData
        {
            var result = new Dictionary<string, List<T>>();
            foreach (var team in teams)
            {
                var units = team.GetAllUnits();
                var unitDataList = new List<T>();
                foreach (var unit in units)
                {
                    var unitData = CreateUnitData<T>(unit);
                    unitDataList.Add(unitData);
                }
                result.Add(team.Uid, unitDataList);
            }
            return result;// 如果没有团队数据，返回空字典
        }

        protected abstract T CreateUnitData<T>(IJCombatUnit unit) where T : class, IJCombatUnitData;

        //{
        //    return new T
        //    {
        //        Uid = unit.Uid,
        //        Seat = seatQuery.GetSeat(unit.Uid)
        //    };
        //}
    }


}
