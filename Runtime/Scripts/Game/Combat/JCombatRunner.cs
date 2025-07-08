using System.Threading.Tasks;

namespace JFramework.Game
{
    public class JCombatRunner : IJCombatRunner
    {
        IJCombatLifeCycle jCombat;

        IJCombatResult jCombatResult;

        IJCombatEventRecorder eventRecorder;

        IJCombatQuery jCombatQuery;

        public JCombatRunner(IJCombatQuery jCombatQuery, IJCombatEventRecorder eventRecorder, IJCombatResult jCombatResult) { 
            this.eventRecorder = eventRecorder;
            this.jCombatQuery = jCombatQuery;
            this.jCombatResult = jCombatResult;
        }

        public void SetCombat(IJCombat combat)
        {
            jCombat = combat;
        }

        public async Task<IJCombatResult> RunCombat()
        {
            if (jCombat == null)
                throw new System.Exception("CombatRunner 没有设置 Combat 请调用 JCombatRunner.SetCombat(IJCombat combat) 方法");

            IJCombatResult r = await Task.Run(() =>
            {
                //开始战斗
                jCombat.OnStart();

                jCombat.OnUpdate();

                //获取胜利者
                var winner = jCombatQuery.GetWinner();

                //设置结果
                jCombatResult.SetCombatEvents(eventRecorder.GetAllCombatEvents());
                jCombatResult.SetCombatWinner(winner);

                jCombat.OnStop();

                return jCombatResult;
            });

            return r;
        }


    }
}
