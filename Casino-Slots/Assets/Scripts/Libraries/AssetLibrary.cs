using UnityEngine;

namespace Slots.Game.Libraries
{
    public class AssetLibrary<T> : ScriptableObject
    {
        public T[] Assets => _assets;

        [SerializeField]
        private T[] _assets;
    }
}