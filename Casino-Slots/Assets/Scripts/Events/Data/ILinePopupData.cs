namespace Events.Data
{
    public interface ILinePopupData : IGameEventData
    {
        int LineIndex { get; }
    }
}