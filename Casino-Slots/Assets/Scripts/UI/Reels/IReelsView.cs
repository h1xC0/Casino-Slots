using Rollers;
using Slots.Game.Audio;
using Slots.Game.Events;
using UI.MVC;

namespace UI.Reels
{
    public interface IReelsView : IView
    {
        IRollerManager RollerManager { get; }
        void Initialize(IAudioService audioService, IEventTriggerService eventTriggerService);
    }
}