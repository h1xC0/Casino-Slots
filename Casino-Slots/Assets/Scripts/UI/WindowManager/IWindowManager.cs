using UI.MVC;
using UnityEngine;

namespace UI.WindowManager
{
    public interface IWindowManager
    {
        void Open<TController>()
            where TController : class, IController<IView, IModel<IView>>;
            
            // where TView : Component, IView
            // where TModel : class, IModel<TView>;

        bool IsOpened<TPresenter>() 
            where TPresenter : class, IController<IView, IModel<IView>>;
            
        void SetSortingIndex<TController>(TController controller, int index) where TController : IController<IView, IModel<IView>>;
        void Close<TController>(TController controller);
    }
}
