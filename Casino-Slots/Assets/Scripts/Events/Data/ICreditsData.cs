namespace Events.Data
{
    public interface ICreditsData : IGameEventData
    {
        int CreditsAmount { get; }
    }
}