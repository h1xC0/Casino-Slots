using UI.MVC;

namespace UI.WindowManager
{
    public class WindowOpenInfo : IWindowOpenInfo
    {
        public int Hash { get; }
        public int SortingIndex { get; private set;}
        public IController<IView, IModel<IView>> Controller { get; } 

        public WindowOpenInfo(IController<IView, IModel<IView>> controller, int sortingIndex)
        {
            Hash = controller.GetHashCode();
            Controller = controller;
            SortingIndex = sortingIndex;
        }

        public void SetSortingIndex(int index)
        {
            Controller.View.RectTransform.SetSiblingIndex(index);
            SortingIndex = index;
        }
    }    
}