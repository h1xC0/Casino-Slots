using System.Collections;
using System.Collections.Generic;
using Events.Data;
using Patterns;
using Slots.Game.Audio;
using Slots.Game.Events;
using UnityEngine;
using Zenject;

namespace Rewards
{
    public class SpinResultRewardsRetriever : MonoBehaviour
    {
        private IAudioService _audioService;
        private IEventTriggerService _eventTriggerService;
        private IGridToLineConverter _gridToLineConverter;
        private ILinePatternChecker _linePatternChecker;
        private IPayTableRewardsRetriever _payTableRewardsRetriever;

        private LineType[] _lineTypes;

        [Inject]
        private void Initialize(IAudioService audioService, 
            IEventTriggerService eventTriggerService,
            IGridToLineConverter gridToLineConverter,
            ILinePatternChecker linePatternChecker,
            IPayTableRewardsRetriever payTableRewardsRetriever)
        {
            _audioService = audioService;
            _eventTriggerService = eventTriggerService;
            _gridToLineConverter = gridToLineConverter;
            _linePatternChecker = linePatternChecker;
            _payTableRewardsRetriever = payTableRewardsRetriever;
            
            _lineTypes = new LineType[(int)LineType.Size];
            for (int i = 0; i < (int)LineType.Size; ++i)
            {
                _lineTypes[i] = (LineType)i;
            }
        }

        public void CheckSpinResult(IGameEventData gameEventData)
        {
            var grid = (gameEventData as SpinResultData).SpinResultGrid;
            StartCoroutine(RetrieveRewards(grid));
        }

        private IEnumerator RetrieveRewards(IGrid grid, float delayBetweenRewardsInSeconds = 5f)
        {
            for (int i = 0; i < _lineTypes.Length; ++i)
            {
                _gridToLineConverter.GetLineValuesFromGrid(_lineTypes[i], grid, out List<int> valuesInLine);
                var lineResult = _linePatternChecker.GetResultFromLine(valuesInLine);
                int lineCredits = _payTableRewardsRetriever.RetrieveReward(lineResult as LineResult);
                if (lineCredits > 0)
                {
                    _eventTriggerService.Trigger("Show Line", new LinePopupData(i));
                    _eventTriggerService.Trigger("Show Credits", new CreditsData(lineCredits));
                    _audioService.Play("Win Credits");
                    yield return new WaitForSeconds(delayBetweenRewardsInSeconds);
                }
            }
            _eventTriggerService.Trigger("Can Spin Again");
        }
    }
}