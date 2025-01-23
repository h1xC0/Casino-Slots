using UI.MVC;

namespace UI.Credits
{
    public class CreditsController : Controller<ICreditsView, CreditsModel>
    {
        public CreditsController(ICreditsView viewContract, CreditsModel modelContract) 
            : base(viewContract, modelContract)
        {
        }

        public override void Open()
        {
            base.Open();
            
            View.OnDisplay(Model.Credits.Value, Model.Bet.Value, Model.BetSize);
            
            View.TransferCreditsEvent += OnTransferCredits;
            View.BetView.IncreaseEvent += OnIncrease;
            View.BetView.DecreaseEvent += OnDecrease;
        }

        public override void Close()
        {
            View.TransferCreditsEvent -= OnTransferCredits;
            View.BetView.IncreaseEvent -= OnIncrease;
            View.BetView.DecreaseEvent -= OnDecrease;
            
            base.Close();
        }

        private void OnIncrease()
        {
            Model.IncreaseBet();
        }

        private void OnDecrease()
        {
            Model.DecreaseBet();
        }

        private void OnTransferCredits(long credits)
        {
            Model.Credits.Value += credits;
            Model.OnModelChanged(true);
        }
    }
}