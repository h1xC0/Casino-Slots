using System.Collections.Generic;
using ResourceProvider;
using UI.MVC;
using UnityEngine;
using Zenject;

namespace UI.WindowManager
{
    public class WindowFactory : IWindowFactory
    {
        private readonly DiContainer _container;
        private readonly IResourceProviderService _resourceProviderService;

        public WindowFactory(DiContainer container, IResourceProviderService resourceProviderService)
        {
            _container = container;
            _resourceProviderService = resourceProviderService;
        }

        // public TController CreateController<TController>(Transform parent = null)
        //     where TController : class, IController<IModel<IView>, IView>
        // {
        //     var modelViewTuple = CreateModelView<IModel<IView>, View>(parent);
        //     var model = modelViewTuple.Item1;
        //     var view = modelViewTuple.Item2;
        //     var controller = _container.Instantiate<TController>(new List<object> {model, view});
        //     return controller;
        // }

        
        public TController CreateController<TController>(IView view, IModel<IView> model)
            where TController : class, IController<IView, IModel<IView>>
        {
            return _container.Instantiate<TController>(new object[] { view, model });
        }

        public TModel CreateModelView<TModel, TView>(TView view, Transform parent = null) 
            where TModel : class, IModel<IView>
            where TView : Component, IView
        {
            var model = _container.Instantiate<TModel>(new List<object>{view});
            return model;
        }

        public TView CreateView<TView>(Transform parent = null) 
            where TView : Component, IView
        {
            var resource = _resourceProviderService.LoadResource<TView>(true);
            var view = Object.Instantiate(resource, parent);
            _container.Inject(view);
            return view;
        }
    }
}