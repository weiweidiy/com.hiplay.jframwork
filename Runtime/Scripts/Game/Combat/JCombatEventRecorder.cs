using JFramework;
using System.Collections.Generic;

namespace JFrame.Game
{
    public abstract class JCombatEventRecorder : IJCombatEventRecorder
    {
        protected List<IJCombatEvent> events = new List<IJCombatEvent>();

        protected EventManager eventManager;

        public JCombatEventRecorder(EventManager eventManager) { 
            
            this.eventManager = eventManager;   
        }

        public List<IJCombatEvent> GetAllCombatEvents() => events;
    }
}
