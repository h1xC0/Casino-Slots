using System.Collections;
using Slots.Game.Audio;
using Slots.Game.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reels
{
    public class SpinButton 
    {
        private IAudioService _audioService;
        private IEventTriggerService _eventTriggerService;

        private Button _spinButton;

        private float enabledAlpha = 1f;
        private float disabledAlpha = 0.8f;

        public SpinButton(IAudioService audioService, IEventTriggerService eventTriggerService)
        {
            _audioService = audioService;
            _eventTriggerService = eventTriggerService;
        }

        public void TriggerStartSpinEvent(MonoBehaviour monoBehaviour)
        {
            _audioService.Play("Press Button");
            monoBehaviour.StartCoroutine(SendStartSpinEventAfterAudioFinishedPlaying());
        }

        private IEnumerator SendStartSpinEventAfterAudioFinishedPlaying()
        {
            yield return new WaitWhile(() => _audioService.IsPlaying("Press Button"));
            _eventTriggerService.Trigger("Start Spin");
        }

        public void SetButtonInteraction(Button button, CanvasGroup canvasGroup, bool makeInteractable)
        {
            button.interactable = makeInteractable;
            canvasGroup.alpha = makeInteractable ? enabledAlpha : disabledAlpha;
        }
    }
}