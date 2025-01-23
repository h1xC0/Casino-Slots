using UI.MVC;
using UnityEngine;

namespace UI.WindowManager
{
    public interface IWindowFactory
    {
        TController CreateController<TController>(IView view, IModel<IView> model)
            where TController : class, IController<IView, IModel<IView>>;

        TModel CreateModelView<TModel, TView>(TView view, Transform parent = null) 
            where TModel : class, IModel<IView>
            where TView : Component, IView;

        TView CreateView<TView>(Transform parent = null) 
            where TView : Component, IView;
    }
}