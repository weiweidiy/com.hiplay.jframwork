using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace JFramework.Game
{
    public abstract class JCombatBasePlayer<T> : BaseRunable, IJCombatTurnBasedPlayer<T> where T : IJCombatUnitData
    {
        protected JCombatTurnBasedReportData<T> reportData;

        float scale = 1f;

        protected IObjectPool pool;

        protected IJCombatAnimationPlayer animationPlayer;

        public JCombatBasePlayer( JCombatTurnBasedReportData<T> reportData, IJCombatAnimationPlayer animationPlayer, IObjectPool objPool = null)
        {
            this.pool = objPool;
            this.reportData = reportData;
            this.animationPlayer = animationPlayer;
        }

        /// <summary>
        /// 初始化战报数据
        /// </summary>
        /// <param name="reportData"></param>
        /// <returns></returns>
        public void LoadReportData(JCombatTurnBasedReportData<T> reportData) 
        {
            this.reportData = reportData;
        }

        /// <summary>
        /// 播放战报
        /// </summary>
        /// <returns></returns>
        public async Task Play()
        {
            string winner = reportData.winnerTeamUid;
            var events = reportData.events;
            var teams = reportData.FormationData;
            //根据战报数据中的队伍信息，初始化游戏对象
            await animationPlayer.Initialize(reportData);
            //用event中的SortIndex字段做升序排序
            events.Sort((x, y) => x.SortIndex.CompareTo(y.SortIndex));
            await OnStartPlayActionEvents(events);
        }

        protected abstract Task OnStartPlayActionEvents(List<JCombatTurnBasedEvent> events);

        protected virtual RunableExtraData GetRunableData()
        {
            if (pool == null)
                return new RunableExtraData();

            return pool.Rent<RunableExtraData>();
        }

        protected virtual JCombatTurnBasedEventRunner GetEventRunner()
        {
            if(pool == null)
                return new JCombatTurnBasedEventRunner();

            return pool.Rent<JCombatTurnBasedEventRunner>();
        }

        protected virtual void ReleaseRunner(JCombatTurnBasedEventRunner runner, RunableExtraData extraData)
        {
            if (pool != null)
            {
                runner.Dispose();
                pool.Return(runner);
                pool.Return(extraData);
            }
 
            else
                runner.Dispose();        
        }

        

        public void RePlay()
        {
            LoadReportData(reportData);
        }

        public void SetScale(float scale)=> this.scale = scale;
        public float GetScale() => scale;


        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            var reportData = extraData.Data as JCombatTurnBasedReportData<T>;

            if (reportData == null)
                throw new ArgumentException("无效的 JCombatReportData ");

            LoadReportData(reportData);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }


    }
}
