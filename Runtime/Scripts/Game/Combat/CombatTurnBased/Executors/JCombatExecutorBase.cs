using System;
using System.Collections.Generic;
using static JFramework.Game.JCombatExecutorDamageDataChange;

namespace JFramework.Game
{


    public abstract class JCombatExecutorBase : JCombatActionComponent, IJCombatExecutor
    {
        protected IJCombatTargetsFinder finder;

        protected IJCombatFormula formulua;

        protected JCombatTurnBasedEvent objEvent;

        public IJCombatFilter filter;

        Dictionary<string, IJCobmatExecuteArgsHistroy> executeArgsHistroy = new Dictionary<string, IJCobmatExecuteArgsHistroy>();

        public JCombatExecutorBase(IJCombatFilter filter, IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args = null) : base(args)
        {
            this.finder = finder;
            this.formulua = formulua;
            this.filter = filter;
        }


        public IJCombatExecutorExecuteArgs Execute(IJCombatExecutorExecuteArgs executeArgs)
        {
            executeArgsHistroy.Clear();

            if (executeArgs == null)
            {
                return executeArgs;
            }

            var targets = executeArgs.TargetUnits;
            if (targets == null || targets.Count == 0)
            {
                return executeArgs;
            }

            foreach (var target in targets)
            {
                if (target == null)
                {
                    throw new ArgumentNullException(nameof(target), "Target unit cannot be null.");
                }

                var needExecutor = true;
                if (filter != null)
                {
                    needExecutor = filter.Filter(executeArgs, target);
                }

                if (needExecutor)
                {
                    executeArgsHistroy.Add(target.Uid, DoExecute(executeArgs, target));
                }
            }

            executeArgs.ExecuteArgsHistroy = executeArgsHistroy;

            return executeArgs;
        }

        protected abstract IJCobmatExecuteArgsHistroy DoExecute(IJCombatExecutorExecuteArgs executeArgs, IJCombatCasterTargetableUnit target);

        public override void SetOwner(IJCombatAction owner)
        {
            base.SetOwner(owner);
            if (finder != null)
                finder.SetOwner(owner);

            if (formulua != null)
            {
                formulua.SetOwner(owner);
            }

            if (filter != null)
                filter.SetOwner(owner);
        }

        public override void SetQuery(IJCombatQuery query)
        {
            base.SetQuery(query);

            if (finder != null)
                finder.SetQuery(query);

            if (formulua != null)
            {
                formulua.SetQuery(query);
            }

            if (filter != null)
                filter.SetQuery(query);
        }

        public void AddCombatEvent(JCombatTurnBasedEvent combatEvent)
        {
            objEvent = combatEvent;
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            if (finder != null)
                finder.Start(extraData);

            if (formulua != null)
            {
                formulua.Start(extraData);
            }

            if (filter != null)
                filter.Start(extraData);
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (finder != null)
                finder.Stop();

            if (formulua != null)
            {
                formulua.Stop();
            }

            if (filter != null)
                filter.Stop();
        }
    }
}
