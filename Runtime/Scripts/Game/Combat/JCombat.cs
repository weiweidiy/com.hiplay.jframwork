using System;
using System.Threading.Tasks;

namespace JFrame.Game
{

    public abstract class JCombat : IJCombat
    {
       // IJCombatDataSource dataSource;

        //IJCombatFrameRecorder frameRecorder;

        protected IJCombatQuery jCombatQuery;

        IJCombatEventRecorder eventRecorder;

        IJCombatResult jCombatResult;//



        public JCombat(/*IJCombatFrameRecorder frameRecorder,*/ IJCombatQuery jCombatQuery, IJCombatEventRecorder eventRecorder, IJCombatResult jCombatResult)
        {
            //his.frameRecorder = frameRecorder ?? throw new ArgumentNullException("frameRecorder is null");
            this.jCombatQuery = jCombatQuery ?? throw new ArgumentNullException("jCombatJudger is null");
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

                Update();

                //获取胜利者
                var winner = jCombatQuery.GetWinner();

                //设置结果
                jCombatResult.SetCombatEvents(eventRecorder.GetAllCombatEvents());
                jCombatResult.SetCombatWinner(winner);

                Stop();

                return jCombatResult;
            });

            return r;
        }

        protected virtual void Start()
        {
        }

        protected abstract void Update();

        //protected abstract void Update(IJCombatFrameRecorder frameRecorder);

        protected virtual void Stop() { }


    }
}
