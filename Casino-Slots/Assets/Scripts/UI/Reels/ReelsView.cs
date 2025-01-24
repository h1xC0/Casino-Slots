using Rollers;
using Slots.Game.Audio;
using Slots.Game.Events;
using Slots.Game.Rollers;
using UI.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Reels
{
    public class ReelsView : View, IReelsView
    {
        public IRollerManager RollerManager => _rollerManager;
        [SerializeField] private RollerManager _rollerManager;
        [SerializeField] private Button _spinButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        private SpinButton _spin;

        public void Initialize(IAudioService audioService, IEventTriggerService eventTriggerService)
        {
            _spin = new SpinButton(audioService, eventTriggerService);
            _spinButton.onClick.AddListener(SetButtonDisabled);
            _spinButton.onClick.AddListener(() => _spin.TriggerStartSpinEvent(this));
        }

        public void SetButtonInteractable(bool flag)
        {
            _spin.SetButtonInteraction(_spinButton, _canvasGroup, flag);
        }

        private void SetButtonDisabled()
        {
            _spinButton.interactable = false;
        }

        public override void Dispose()
        {
            base.Dispose();
            _spinButton.onClick.RemoveListener(SetButtonDisabled);
            _spinButton.onClick.RemoveListener(() => _spin.TriggerStartSpinEvent(this));
        }
    }
}