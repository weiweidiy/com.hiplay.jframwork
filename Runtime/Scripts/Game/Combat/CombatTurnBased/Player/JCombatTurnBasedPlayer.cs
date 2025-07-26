using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedPlayer<T> : JCombatBasePlayer<T> where T : IJCombatUnitData
    {
        public JCombatTurnBasedPlayer(JCombatTurnBasedReportData<T> reportData, ICombatAnimationPlayer animationPlayer, IObjectPool objPool = null) : base(reportData, animationPlayer,objPool)
        {
        }

        protected override async void OnStartPlay(List<JCombatTurnBasedEvent> events)
        {
            var que = new Queue<JCombatTurnBasedEvent>(events);

            while (que.Count > 0)
            {
                var runner = GetEventRunner();
                runner.AnimationPlayer = animationPlayer;
                var combatEvent = que.Dequeue();
                var runableData = GetRunableData();
                runableData.Data = combatEvent;
                await runner.Start(runableData);

                ReleaseRunner(runner, runableData);
            }
        }
    }
}
