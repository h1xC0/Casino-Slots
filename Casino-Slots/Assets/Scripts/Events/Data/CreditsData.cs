namespace Events.Data
{
    public class CreditsData : ICreditsData
    {
        public int CreditsAmount { get; private set; }

        public CreditsData(int creditsAmount)
        {
            CreditsAmount = creditsAmount;
        }
    }
}