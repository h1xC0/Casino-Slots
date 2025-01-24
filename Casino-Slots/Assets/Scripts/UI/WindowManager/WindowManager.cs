using System;
using System.Collections.Generic;
using System.Linq;
using UI.MVC;
using UnityEngine;

namespace UI.WindowManager
{
    public class WindowManager : IWindowManager, IWindowRegistration
    {
        public event Action<IController<IView, IModel<IView>>> RegisterEvent;

        private Transform _windowsParent;
        private Dictionary<Type, IWindowOpenInfo> _activeWindows;
        private readonly IWindowFactory _windowFactory;

        private int _lastSortingIndex;
        private readonly Dictionary<Type, Action<Transform, Action<IView, IModel<IView>>>> _viewModelCreators;


        public WindowManager(IWindowFactory windowFactory, Transform parent)
        {
            _activeWindows = new Dictionary<Type, IWindowOpenInfo>();
            _viewModelCreators = new Dictionary<Type, Action<Transform, Action<IView, IModel<IView>>>>();
            _windowFactory = windowFactory;
            _windowsParent = parent;
        } 

        public TController Open<TController>() 
            where TController : class, IController<IView, IModel<IView>>
        {
            var viewModel = CreateViewModel<TController>(_windowsParent);
            var controller = _windowFactory.CreateController<TController>(viewModel.view, viewModel.model);
            _lastSortingIndex++;

            var openInfo = new WindowOpenInfo(controller, _lastSortingIndex);

            openInfo.Controller.Open();
            _activeWindows.Add(openInfo.Controller.GetType(), openInfo);
            Debug.Log($"A controller was created with hash <color=green>{openInfo.Hash}</color>");
            return (TController)openInfo.Controller;
        }

        public bool IsOpened<TPresenter>() 
            where TPresenter : class, IController<IView, IModel<IView>> => 
            _activeWindows
                .Any(windowOpenPair => windowOpenPair.Key == typeof(TPresenter));

        public void Register<TController>(Action<Transform, Action<IView, IModel<IView>>> createMethod)
            where TController : class, IController<IView, IModel<IView>>
        {
            if (IsOpened<TController>())
                return;
            
            var type = typeof(TController);
            _viewModelCreators[type] = createMethod;
        }

        public void SetSortingIndex<TController>(TController controller, int index) where TController : IController<IView, IModel<IView>>
        {
            foreach (var windowInfo in _activeWindows)
            {
                if(windowInfo.Value.SortingIndex == index)
                {
                    _activeWindows.TryGetValue(controller.GetType(), out IWindowOpenInfo currentWindowInfo);
                    if(currentWindowInfo == null) break;
                    var auxIndex = controller.View.RectTransform.GetSiblingIndex();
                    windowInfo.Value.SetSortingIndex(auxIndex);
                    currentWindowInfo.SetSortingIndex(index);

                    break;
                }
            }
        }

        public IWindowOpenInfo GetWindow<TController>() 
            where TController : class, IController<IView, IModel<IView>>
        {
            _activeWindows.TryGetValue(typeof(TController), out IWindowOpenInfo currentWindowInfo);

            return currentWindowInfo;
        }

        public void Close<TController>(TController controller)
        {
            if(_activeWindows.TryGetValue(controller.GetType(), out IWindowOpenInfo openInfo) == false)
            {
                Debug.Log($"<color=yellow>Didn't {controller.GetType().Name} controller");
                return;
            }

            openInfo.Controller.Close();
            _activeWindows.Remove(openInfo.Controller.GetType());
            _lastSortingIndex--;
        }

        private (IView view, IModel<IView> model) CreateViewModel<TController>(Transform parent)
            where TController : class, IController<IView, IModel<IView>>
        {
            IView windowView = null;
            IModel<IView> windowModel = null;
            _viewModelCreators[typeof(TController)].Invoke(parent, (view, model) =>
            {
                if (view == null)
                {
                    Debug.LogError($"Couldn't create View: {typeof(TController)}");
                    return;
                }

                if (model == null)
                {
                    Debug.LogError($"Couldn't create Model: {typeof(TController)}");
                }

                windowView = view;
                windowModel = model;
            });

            return (windowView, windowModel);
        }
    }
}
