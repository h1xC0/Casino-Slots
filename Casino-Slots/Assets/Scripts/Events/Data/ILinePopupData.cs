namespace Slots.Game.Events
{
    public interface ILinePopupData : IGameEventData
    {
        int LineIndex { get; }
    }
}