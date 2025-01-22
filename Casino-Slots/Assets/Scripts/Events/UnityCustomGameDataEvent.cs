using System;
using UnityEngine.Events;

namespace Slots.Game.Events
{
    [Serializable]
    public class UnityCustomGameDataEvent : UnityEvent<IGameEventData> { }
}
