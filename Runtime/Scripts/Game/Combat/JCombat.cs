using System;
using System.Threading.Tasks;

namespace JFrame.Game
{

    public abstract class JCombat : IJCombat
    {
        protected IJCombatQuery jCombatQuery;

        IJCombatEventRecorder eventRecorder;

        IJCombatResult jCombatResult;//


        public JCombat(IJCombatQuery jCombatQuery, IJCombatEventRecorder eventRecorder, IJCombatResult jCombatResult)
        {
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



        protected abstract void Update();

        protected virtual void Start()
        {
            var units = jCombatQuery.GetUnits();
            if(units != null)
            {
                foreach (var unit in units)
                {
                    unit.Start(jCombatQuery);
                }
            }

        }


        protected virtual void Stop()
        {
            var units = jCombatQuery.GetUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.Stop();
                }
            }

        }


    }
}
