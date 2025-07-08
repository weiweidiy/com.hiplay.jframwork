using System;
using System.Threading.Tasks;

namespace JFramework.Game
{

    public abstract class JCombat : IJCombat
    {
        protected IJCombatQuery jCombatQuery;

        IJCombatRunner jCombatRunner;
        public JCombat(IJCombatQuery jCombatQuery, IJCombatRunner jCombatRunner)
        {
            this.jCombatQuery = jCombatQuery;
            this.jCombatRunner = jCombatRunner;
            jCombatRunner.SetCombat(this);
        }

        /// <summary>
        /// 计算战斗结果并返回
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IJCombatResult> GetResult()
        {
            return jCombatRunner.RunCombat();
        }




        public virtual void OnStart()
        {
            var units = jCombatQuery.GetUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.OnStart();
                }
            }

        }

        public abstract void OnUpdate();

        public virtual void OnStop()
        {
            var units = jCombatQuery.GetUnits();
            if (units != null)
            {
                foreach (var unit in units)
                {
                    unit.OnStop();
                }
            }

        }


    }
}
