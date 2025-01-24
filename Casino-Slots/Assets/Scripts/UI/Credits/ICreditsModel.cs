using R3;
using UI.MVC;

namespace UI.Credits
{
    public interface ICreditsModel : IModel<ICreditsView>
    {
        long Credits { get; }
        long Bet { get; }
        int BetSize { get; }

        void EarnCredits(long credits);
        void SpendCredits();
        void IncreaseBet();
        void DecreaseBet();
        void MaxBet();
        void OnModelChanged(bool animate = false);
    }
}