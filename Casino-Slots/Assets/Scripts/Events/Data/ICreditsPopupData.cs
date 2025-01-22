namespace Slots.Game.Events
{
    public interface ICreditsPopupData : IGameEventData
    {
        int CreditsAmount { get; }
    }
}