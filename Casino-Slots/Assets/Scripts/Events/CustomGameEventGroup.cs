using System;

namespace Slots.Game.Events
{
    [Serializable]
    public class CustomGameEventGroup
    {
        public GameEvent gameEvent;
        public UnityCustomGameDataEvent onTriggerEvent;
    }
}