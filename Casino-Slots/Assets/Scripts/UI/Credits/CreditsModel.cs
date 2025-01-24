using R3;
using UI.MVC;

namespace UI.Credits
{
    public class CreditsModel : Model<ICreditsView>, ICreditsModel
    {
        public long Credits { get; private set; } = 1000;
        public long Bet { get; private set; } = 10;

        public int BetSize => 10;

        public CreditsModel(ICreditsView view) 
            : base(view)
        {
            OnModelChanged();
        }

        public void EarnCredits(long credits = 0)
        {
            SetCredits(credits);
            OnModelChanged(true);
        }

        public void SpendCredits()
        {
            SetCredits(-Bet);
            OnModelChanged();
        }
        
        public void IncreaseBet()
        {
            SetBet(BetSize);
            OnModelChanged();
        }

        public void DecreaseBet()
        {
            SetBet(-BetSize);
            OnModelChanged();
        }

        public void MaxBet()
        {
            Bet = Credits;
            OnModelChanged();
        }
        
        private void SetBet(long value)
        {
            Bet += value;
        }

        private void SetCredits(long value)
        {
            Credits += value;
        }

        public void OnModelChanged(bool animate = false)
        {
            View.OnDisplay(Credits, Bet, BetSize, animate);
            
            if (Credits < BetSize)
                Bet = Credits;
            else
            {
                if(Bet < BetSize)
                    Bet = BetSize;
            }
        }
    }
}