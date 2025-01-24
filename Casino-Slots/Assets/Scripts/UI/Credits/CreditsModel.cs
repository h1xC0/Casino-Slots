using R3;
using UI.MVC;

namespace UI.Credits
{
    public class CreditsModel : Model<ICreditsView>, ICreditsModel
    {
        public ReactiveProperty<long> Credits { get; set; } = new(1000);
        public ReactiveProperty<long> Bet { get; set; } = new(100);

        public int BetSize => 100;

        public CreditsModel(ICreditsView view) 
            : base(view)
        {
            OnModelChanged();
        }
        
        public void IncreaseBet()
        {
            SetAmount(BetSize);
            OnModelChanged();
        }

        public void DecreaseBet()
        {
            SetAmount(-BetSize);
            OnModelChanged();
        }

        public void MaxBet()
        {
            Bet.Value = Credits.Value;
            OnModelChanged();
        }
        
        private void SetAmount(long value)
        {
            Bet.Value += value;
        }

        public void OnModelChanged(bool animate = false)
        {
            View.OnDisplay(Credits.Value, Bet.Value, BetSize, animate);
            
            if (Credits.Value < BetSize)
                Bet.Value = Credits.Value;
            else
            {
                if(Bet.Value < BetSize)
                    Bet.Value = BetSize;
            }
        }
    }
}