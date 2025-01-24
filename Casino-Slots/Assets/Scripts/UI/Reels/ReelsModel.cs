using UI.MVC;
using UI.WindowManager;

namespace UI.Reels
{
    public class ReelsModel : Model<IReelsView>, IReelsModel
    {
        private readonly IWindowManager _windowManager;
        
        public ReelsModel(IReelsView view, IWindowManager windowManager) : base(view)
        {
            _windowManager = windowManager;
        }
    }

    public interface IReelsModel : IModel<IReelsView>
    {
        
    }
}