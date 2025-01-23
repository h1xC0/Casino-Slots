using UI.MVC;

namespace UI.WindowManager
{
    public interface IWindowOpenInfo
    {
        int Hash { get; }
        int SortingIndex { get; }
        IController<IView, IModel<IView>> Controller { get; }

        void SetSortingIndex(int index);
    }
}