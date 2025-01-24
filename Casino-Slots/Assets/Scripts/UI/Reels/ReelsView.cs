using System;
using Rollers;
using Slots.Game.Audio;
using Slots.Game.Events;
using UI.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reels
{
    public class ReelsView : View, IReelsView
    {
        public event Action ReelsSpinEvent;
        public IRollerManager RollerManager => _rollerManager;
        [SerializeField] private RollerManager _rollerManager;
        [SerializeField] private Button _spinButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        private SpinButton _spin;

        public void Initialize(IAudioService audioService, IEventTriggerService eventTriggerService)
        {
            _spin = new SpinButton(audioService, eventTriggerService);
            _spinButton.onClick.AddListener(ReelsSpin);
            _spinButton.onClick.AddListener(() => _spin.TriggerStartSpinEvent(this));
        }

        public void SetButtonInteractable(bool flag)
        {
            _spin.SetButtonInteraction(_spinButton, _canvasGroup, flag);
        }

        public void SetButtonActive(bool flag)
        {
            _spinButton.gameObject.SetActive(flag);
        }

        private void ReelsSpin()
        {
            ReelsSpinEvent?.Invoke();
        }

        public override void Dispose()
        {
            base.Dispose();
            _spinButton.onClick.RemoveListener(ReelsSpin);
            _spinButton.onClick.RemoveListener(() => _spin.TriggerStartSpinEvent(this));
        }
    }
}