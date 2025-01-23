using UI.WindowManager;

namespace UI.Credits
{
    public class CreditsMapper : WindowMapper<CreditsController, CreditsView, CreditsModel>
    {
        public CreditsMapper(IWindowRegistration windowRegistration, IWindowFactory windowFactory) : base(windowRegistration, windowFactory)
        {
        }
    }
}