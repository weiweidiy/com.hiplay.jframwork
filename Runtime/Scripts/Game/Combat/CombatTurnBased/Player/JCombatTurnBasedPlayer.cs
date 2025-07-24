using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedPlayer<T> : JCombatBasePlayer<T> where T : IJCombatUnitData
    {
        public JCombatTurnBasedPlayer() : base() { }
        public JCombatTurnBasedPlayer(IObjectPool objPool) : base(objPool) { }

        protected override async void OnStartPlay(List<JCombatTurnBasedEvent> events)
        {
            var que = new Queue<JCombatTurnBasedEvent>(events);

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
