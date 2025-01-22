using Slots.Game.Audio;
using Slots.Game.Events;
using Slots.Game.Libraries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slots.Game.Rollers
{
    public class RollerManager : MonoBehaviour
    {
        [Inject] private IAudioService _audioService;
        [Inject] private IEventTriggerService _eventTriggerService;
        [Inject] private RollerFactory _rollerFactory;
        [Inject] private RollerSequencesLibrary _rollerSequencesLibrary;
        [Inject] private SpriteLibrary _spriteAssets;

        private Roller[] _rollers;

        private const float _startingRollerXPosition = -461f;
        private const float _spacingBetweenRollers = 230f;
        private const float _delayBetweenRollersInSeconds = 0.25f;

        public const int NumberOfRowsInGrid = 3;
        public const int NumberOfColumnsInGrid = 5;

        private void Start()
        {
            _rollers = new Roller[NumberOfColumnsInGrid];
            InstantiateAndAddRollersToList();
        }

        public void StartSpin()
        {
            StartCoroutine(SpinRollers());
        }

        public void ResetGrid()
        {
        }

        private void InstantiateAndAddRollersToList()
        {
            for (int i = 0; i < _rollers.Length; ++i)
            {
                var roller = _rollerFactory.Create();
                roller.transform.SetParent(transform);
                var rollerLocalPosition = Vector3.right * (_startingRollerXPosition + (i * _spacingBetweenRollers));
                roller.transform.localPosition = rollerLocalPosition;
                roller.transform.localScale = Vector3.one;
                roller.Initialize(_rollerSequencesLibrary.Assets[i], _spriteAssets);
                _rollers[i] = roller;
            }
        }

        private IEnumerator SpinRollers()
        {
            _audioService.Play("Spin Roller", true);
            for (int i = 0; i < _rollers.Length; ++i)
            {
                _rollers[i].StartSpin();
                yield return new WaitForSeconds(_delayBetweenRollersInSeconds);
            }
            for (uint i = 0; i < _rollers.Length; ++i)
            {
                _rollers[i].StartSpinCountdown();
                yield return new WaitWhile(() => _rollers[i].IsSpinning);
                yield return new WaitForSeconds(_delayBetweenRollersInSeconds);
                _rollers[i].GetRollerItemsOnScreen(out List<int> itemsOnScreen);
            }
            _audioService.Stop("Spin Roller");
        }
    }
}