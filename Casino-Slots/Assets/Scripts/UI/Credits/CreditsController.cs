using UI.MVC;

namespace UI.Credits
{
    public class CreditsController : Controller<ICreditsView, ICreditsModel>
    {
        public CreditsController(ICreditsView viewContract, ICreditsModel modelContract) 
            : base(viewContract, modelContract)
        {
        }

        public override void Open()
        {
            base.Open();
            
            View.OnDisplay(Model.Credits, Model.Bet, Model.BetSize);
            
            View.TransferCreditsEvent += OnTransferCredits;
            View.BetView.IncreaseEvent += OnIncrease;
            View.BetView.DecreaseEvent += OnDecrease;
            View.BetView.MaxBetEvent += OnMaxBet;
        }

        public override void Close()
        {
            View.TransferCreditsEvent -= OnTransferCredits;
            View.BetView.IncreaseEvent -= OnIncrease;
            View.BetView.DecreaseEvent -= OnDecrease;
            View.BetView.MaxBetEvent -= OnMaxBet;
            
            base.Close();
        }
        
        public void SpendCredits()
        {
            Model.SpendCredits();
        }

        private void OnIncrease()
        {
            Model.IncreaseBet();
        }

        private void OnDecrease()
        {
            Model.DecreaseBet();
        }

        private void OnMaxBet()
        {
            Model.MaxBet();
        }

        private void OnTransferCredits(long credits)
        {
            Model.EarnCredits(credits);
        }
    }
}