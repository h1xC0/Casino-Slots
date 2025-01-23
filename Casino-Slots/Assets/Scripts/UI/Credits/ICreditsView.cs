using System;
using UI.MVC;

namespace UI.Credits
{
    public interface ICreditsView : IView
    {
        event Action<long> TransferCreditsEvent;
        IBetView BetView { get; }
        void OnDisplay(long credits, long bet, int betSize, bool animate = false);
    }
}