using System.Collections;
using System.Collections.Generic;
using Events.Data;
using Patterns;
using Slots.Game.Audio;
using Slots.Game.Events;
using Slots.Game.Libraries;
using UnityEngine;
using Grid = Patterns.Grid;

namespace Rollers
{
    public interface IRollerManager
    {
        void Initialize(IAudioService audioService,
            IEventTriggerService eventTriggerService,
            RollerFactory rollerFactory,
            RollerSequencesLibrary rollerSequencesLibrary,
            SpriteLibrary spriteAssets);

        void StartSpin();
        void ResetGrid();
    }
    
    public class RollerManager : MonoBehaviour, IRollerManager
    {
        private IAudioService _audioService;
        private IEventTriggerService _eventTriggerService;
        private RollerFactory _rollerFactory;
        private RollerSequencesLibrary _rollerSequencesLibrary;
        private SpriteLibrary _spriteAssets;

        private IGrid _gridOfStoppedRollerItemsOnScreen;
        private Roller[] _rollers;

        private const float _startingRollerXPosition = -461f;
        private const float _spacingBetweenRollers = 230f;
        private const float _delayBetweenRollersInSeconds = 0.1f;

        public const int NumberOfRowsInGrid = 3;
        public const int NumberOfColumnsInGrid = 5;
        
        public void Initialize(IAudioService audioService,
            IEventTriggerService eventTriggerService,
            RollerFactory rollerFactory,
            RollerSequencesLibrary rollerSequencesLibrary,
            SpriteLibrary spriteAssets)
        {
            _audioService = audioService;
            _eventTriggerService = eventTriggerService;
            _rollerFactory = rollerFactory;
            _rollerSequencesLibrary = rollerSequencesLibrary;
            _spriteAssets = spriteAssets;
            
            _rollers = new Roller[NumberOfColumnsInGrid];
            _gridOfStoppedRollerItemsOnScreen = new Grid(NumberOfRowsInGrid, NumberOfColumnsInGrid);
            InstantiateAndAddRollersToList();
        }

        public void StartSpin()
        {
            StartCoroutine(SpinRollers());
        }

        public void ResetGrid()
        {
            _gridOfStoppedRollerItemsOnScreen.ResetGridValues();
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
                _gridOfStoppedRollerItemsOnScreen.SetColumnValues(i, itemsOnScreen);
            }
            _audioService.Stop("Spin Roller");
            _eventTriggerService.Trigger("Check Spin Result", new SpinResultData(_gridOfStoppedRollerItemsOnScreen));
        }
    }
}