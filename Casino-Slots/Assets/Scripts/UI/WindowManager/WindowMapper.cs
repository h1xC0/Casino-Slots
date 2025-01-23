using UI.MVC;
using UnityEngine;

namespace UI.WindowManager
{
    public class WindowMapper<TController, TView, TModel>
        where TController : class, IController<IView, IModel<IView>>
        where TView : Component, IView
        where TModel : class, IModel<IView>
    {
        private readonly IWindowRegistration _windowRegistration;
        private readonly IWindowFactory _windowFactory;

        public WindowMapper(IWindowRegistration windowRegistration, IWindowFactory windowFactory)
        {
            _windowRegistration = windowRegistration;
            _windowFactory = windowFactory;
            Initialize();
        }

        private void Initialize()
        {
            _windowRegistration.Register<TController>((transform, action) => 
            {
                var view = _windowFactory.CreateView<TView>(transform);
                action.Invoke(view,_windowFactory.CreateModelView<TModel, TView>(view, transform));
            });
        }   
    }
}