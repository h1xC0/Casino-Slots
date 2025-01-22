using Slots.Game.Events;
using UnityEngine;

namespace Slots.Game.Libraries
{
    [CreateAssetMenu(fileName = "New Game Event Library", menuName = "Libraries/Game Event Library")]
    public class GameEventLibrary : AssetLibrary<GameEvent> { }
}