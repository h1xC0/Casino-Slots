using Rollers;
using Slots.Game.Audio;
using Slots.Game.Events;
using Slots.Game.Libraries;
using UI.Credits;
using UI.MVC;
using UI.WindowManager;

namespace UI.Reels
{
    public class ReelsController : Controller<IReelsView, IReelsModel>
    {
        private readonly IAudioService _audioService;
        private readonly IEventTriggerService _eventTriggerService;
        private readonly RollerFactory _rollerFactory;
        private readonly RollerSequencesLibrary _rollerSequencesLibrary;
        private readonly SpriteLibrary _spriteLibrary;
        private readonly IWindowManager _windowManager;
        private CreditsController _creditsController;

        public ReelsController(IReelsView viewContract,
            IReelsModel modelContract,
            IAudioService audioService,
            IEventTriggerService eventTriggerService,
            RollerFactory rollerFactory,
            RollerSequencesLibrary rollerSequencesLibrary,
            SpriteLibrary spriteLibrary,
            IWindowManager windowManager) 
            : base(viewContract, modelContract)
        {
            _audioService = audioService;
            _eventTriggerService = eventTriggerService;
            _rollerFactory = rollerFactory;
            _rollerSequencesLibrary = rollerSequencesLibrary;
            _spriteLibrary = spriteLibrary;
            _windowManager = windowManager;
        }

        public override void Open()
        {
            base.Open();
            View.Initialize(_audioService, _eventTriggerService);
            View.RollerManager.Initialize(_audioService, _eventTriggerService, _rollerFactory, _rollerSequencesLibrary, _spriteLibrary);

            _creditsController = (CreditsController)_windowManager.GetWindow<CreditsController>().Controller;
            View.ReelsSpinEvent += SpendCredits;
        }

        public override void Close()
        {
            View.ReelsSpinEvent -= SpendCredits;
            base.Close();
        }

        private void SpendCredits()
        {
            _creditsController.SpendCredits();
            if (_creditsController.Model.Credits <= 0)
            {
                View.SetButtonActive(false);
            }
        }
    }
}