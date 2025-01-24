using Slots.Game.Audio;
using Slots.Game.Events;
using Slots.Game.Libraries;
using Slots.Game.Rollers;
using UI.MVC;

namespace UI.Reels
{
    public class ReelsController : Controller<IReelsView, IReelsModel>
    {
        private readonly IAudioService _audioService;
        private readonly IEventTriggerService _eventTriggerService;
        private readonly RollerFactory _rollerFactory;
        private readonly RollerSequencesLibrary _rollerSequencesLibrary;
        private readonly SpriteLibrary _spriteLibrary;

        public ReelsController(IReelsView viewContract,
            IReelsModel modelContract,
            IAudioService audioService,
            IEventTriggerService eventTriggerService,
            RollerFactory rollerFactory,
            RollerSequencesLibrary rollerSequencesLibrary,
            SpriteLibrary spriteLibrary) 
            : base(viewContract, modelContract)
        {
            _audioService = audioService;
            _eventTriggerService = eventTriggerService;
            _rollerFactory = rollerFactory;
            _rollerSequencesLibrary = rollerSequencesLibrary;
            _spriteLibrary = spriteLibrary;
        }

        public override void Open()
        {
            base.Open();
            View.Initialize(_audioService, _eventTriggerService);
            View.RollerManager.Initialize(_audioService, _eventTriggerService, _rollerFactory, _rollerSequencesLibrary, _spriteLibrary);
        }
    }
}