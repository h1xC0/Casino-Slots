using UI.MVC;

namespace UI.Reels
{
    public class ReelsModel : Model<IReelsView>, IReelsModel
    {
        public ReelsModel(IReelsView view) : base(view)
        {
            
        }
    }

    public interface IReelsModel : IModel<IReelsView>
    {
        
    }
}