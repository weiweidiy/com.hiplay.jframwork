using System.Threading.Tasks;

namespace JFramework.Game
{
    /// <summary>
    /// 基于后台线程单帧运行的runner
    /// </summary>
    public class JCombatTurnBasedRunner : IJCombatTurnBasedReporter, IRunner
    {
        /// <summary>
        /// 可运行战斗对象
        /// </summary>
        IRunable jCombat;

        /// <summary>
        /// 战斗战报接口
        /// </summary>
        IJCombatTurnBasedReport jCombatReport;

        IJCombatTurnBasedEventRecorder eventRecorder;

        IJCombatQuery jCombatQuery;

        public JCombatTurnBasedRunner(IRunable combat, IJCombatQuery jCombatQuery, IJCombatTurnBasedEventRecorder eventRecorder, IJCombatTurnBasedReport jCombatResult) { 
            this.eventRecorder = eventRecorder;
            this.jCombatQuery = jCombatQuery;
            this.jCombatReport = jCombatResult;
            this.jCombat = combat;
        }

        /// <summary>
        /// 设置可执行对象
        /// </summary>
        /// <param name="combat"></param>
        public void SetRunable(IRunable combat)
        {
            jCombat = combat;
        }

        /// <summary>
        /// 获取运行结果
        /// </summary>
        /// <returns></returns>
        public IJCombatTurnBasedReport GetReport()
        {
            return jCombatReport;
        }

        /// <summary>
        /// 运行：这里是多线程方式运行一次
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            IJCombatTurnBasedReport r = await Task.Run(() =>
            {
                //开始战斗
                jCombat.Start(null);

                jCombat.Update(null);

                jCombat.Stop();

                //获取胜利者
                var winner = jCombatQuery.GetWinner();

                //设置结果
                jCombatReport.SetCombatEvents(eventRecorder.GetAllCombatEvents());
                jCombatReport.SetCombatWinner(winner);

                return jCombatReport;
            });
        }
    }
}
