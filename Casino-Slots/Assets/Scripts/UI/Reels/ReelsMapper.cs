using UI.WindowManager;

namespace UI.Reels
{
    public class ReelsMapper : WindowMapper<ReelsController, ReelsView, ReelsModel>
    {
        public ReelsMapper(IWindowRegistration windowRegistration, IWindowFactory windowFactory) : base(windowRegistration, windowFactory)
        {
        }
    }
}