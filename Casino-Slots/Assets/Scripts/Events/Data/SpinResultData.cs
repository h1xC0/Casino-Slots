using Patterns;

namespace Events.Data
{
    public class SpinResultData : IGameEventData
    {
        public readonly IGrid SpinResultGrid;

        public SpinResultData(IGrid grid)
        {
            SpinResultGrid = grid;
        }
    }
}