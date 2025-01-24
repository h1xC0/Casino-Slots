using Patterns;

namespace Rewards
{
    public interface IPayTableRewardsRetriever
    {
        int RetrieveReward(LineResult result);
    }
}