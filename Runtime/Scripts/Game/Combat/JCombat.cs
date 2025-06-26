using System;
using System.Threading.Tasks;

namespace JFrame.Game
{
    public abstract class JCombat : IJCombat , IJCombatDataSource
    {
        IJCombatDataSource dataSource;

        IJCombatFrameRecorder frameRecorder;

        IJCombatJudger jCombatJudger;

        IJCombatEventRecorder eventRecorder;

        IJCombatResult jCombatResult;

        public JCombat(IJCombatDataSource dataSource, IJCombatFrameRecorder frameRecorder, IJCombatJudger jCombatJudger, IJCombatEventRecorder eventRecorder, IJCombatResult jCombatResult)
        {
            this.dataSource = dataSource ?? throw new ArgumentNullException("dataSource is null");
            this.frameRecorder = frameRecorder ?? throw new ArgumentNullException("frameRecorder is null");
            this.jCombatJudger = jCombatJudger ?? throw new ArgumentNullException("jCombatJudger is null");
            this.eventRecorder = eventRecorder ?? throw new ArgumentNullException("eventRecorder is null");
            this.jCombatResult = jCombatResult ?? throw new ArgumentNullException("jCombatResult is null");
        }



        /// <summary>
        /// 计算战斗结果并返回
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IJCombatResult> GetResult()
        {
            IJCombatResult r = await Task.Run(() =>
            {
                //开始战斗
                Start();

                //更新战斗 如果战斗没有决出胜负，则继续战斗
                while (!jCombatJudger.IsCombatOver())
                {
                    Update(frameRecorder);

                    frameRecorder.NextFrame();
                }

                //获取胜利者
                var winner = jCombatJudger.GetWinner();

                //设置结果
                jCombatResult.SetCombatEvents(eventRecorder.GetAllCombatEvents());
                jCombatResult.SetCombatWinner(winner);
                //report.deltaTime = frame.DeltaTime;
                //report.attacker = attackers;
                //report.defence = defencers;
                //report.damageStatistics = Reporter.DamageStatistics;

                Stop();

                return jCombatResult;
            });

            //Debug.Log("战斗结束 " + frame.FrameCount);
            //await UniTask.Delay(5000);
            return r;
        }

        protected abstract IJCombatResult CreateReport();
        protected virtual void Start()
        {
            frameRecorder.ResetFrame();
        }
        protected abstract void Update(IJCombatFrameRecorder frameRecorder);
        protected virtual void Stop() { }

        public IJCombatTeam GetTeam(int teamId) => dataSource.GetTeam(teamId);
    }
}
