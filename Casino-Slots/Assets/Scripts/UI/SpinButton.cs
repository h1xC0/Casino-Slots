using Slots.Game.Audio;
using Slots.Game.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Slots.Game.UI
{
    [RequireComponent(typeof(Button))]
    public class SpinButton : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [Inject] private IAudioService _audioService;
        [Inject] private IEventTriggerService _eventTriggerService;

        private Button _spinButton;

        private float enabledAlpha = 1f;
        private float disabledAlpha = 0.8f;

        private void Awake()
        {
            _spinButton = GetComponent<Button>();
        }

        public void TriggerStartSpinEvent()
        {
            _audioService.Play("Press Button");
            StartCoroutine(SendStartSpinEventAfterAudioFinishedPlaying());
        }

        private IEnumerator SendStartSpinEventAfterAudioFinishedPlaying()
        {
            yield return new WaitWhile(() => _audioService.IsPlaying("Press Button"));
            _eventTriggerService.Trigger("Start Spin");
        }

        public void SetButtonInteraction(bool makeInteractable)
        {
            _spinButton.interactable = makeInteractable;
            _canvasGroup.alpha = makeInteractable ? enabledAlpha : disabledAlpha;
        }
    }
}