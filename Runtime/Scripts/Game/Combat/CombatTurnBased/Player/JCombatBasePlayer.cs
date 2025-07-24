using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace JFramework.Game
{
    public abstract class JCombatBasePlayer<T> : BaseRunable, IJCombatTurnBasedPlayer<T> where T : IJCombatUnitData
    {
        protected JCombatTurnBasedReportData<T> reportData;

        float scale = 1f;

        protected IObjectPool pool;

        public JCombatBasePlayer() : this(null) { }

        public JCombatBasePlayer(IObjectPool objPool) => this.pool = objPool;

        public virtual void Play(JCombatTurnBasedReportData<T> reportData) 
        {
            this.reportData = reportData;

            string winner = reportData.winnerTeamUid;
            var events = reportData.events;

            OnStartPlay(events);
        }

        public void Play()
        {
            Play(reportData);
        }

        protected abstract void OnStartPlay(List<JCombatTurnBasedEvent> events);

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
            Play(reportData);
        }

        public void SetScale(float scale)=> this.scale = scale;
        public float GetScale() => scale;


        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            var reportData = extraData.Data as JCombatTurnBasedReportData<T>;

            if (reportData == null)
                throw new ArgumentException("无效的 JCombatReportData ");

            Play(reportData);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }


    }
}
