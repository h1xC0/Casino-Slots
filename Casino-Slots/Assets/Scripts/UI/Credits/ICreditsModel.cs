using R3;
using UI.MVC;

namespace UI.Credits
{
    public interface ICreditsModel : IModel<ICreditsView>
    {
        ReactiveProperty<long> Credits { get; set; }
        ReactiveProperty<long> Bet { get; set; }
        int BetSize { get; }

        void IncreaseBet();
        void DecreaseBet();
        void MaxBet();
        void OnModelChanged(bool animate = false);
    }
}