using System;
using UI.MVC;
using UnityEngine;

namespace UI.WindowManager
{
    public interface IWindowRegistration
    {
        void Register<TController>(Action<Transform, Action<IView, IModel<IView>>> createMethod)
            where TController : class, IController<IView, IModel<IView>>;

        event Action<IController<IView, IModel<IView>>> RegisterEvent;
    }
}