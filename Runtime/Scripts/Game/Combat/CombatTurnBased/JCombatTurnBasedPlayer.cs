using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedPlayer : JCombatPlayer
    {
        public JCombatTurnBasedPlayer() : base() { }
        public JCombatTurnBasedPlayer(IObjectPool objPool) : base(objPool) { }

        protected override async void OnStartPlay(List<CombatEvent> events)
        {
            var que = new Queue<CombatEvent>(events);

            while (que.Count > 0)
            {
                var runner = GetEventRunner();
                var combatEvent = que.Dequeue();
                var runableData = GetRunableData();
                runableData.Data = combatEvent;
                await runner.Start(runableData);

                ReleaseRunner(runner, runableData);
            }
        }
    }
}
