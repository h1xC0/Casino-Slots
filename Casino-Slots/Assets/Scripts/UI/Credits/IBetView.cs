using System;

namespace UI.Credits
{
    public interface IBetView
    {
        event Action IncreaseEvent;
        event Action DecreaseEvent;
        event Action MaxBetEvent;
    }
}