using System;
using System.Collections.Generic;

namespace UI.MVC
{
    public class Controller<TView, TModel> : IController<TView, TModel>
        where TView : IView 
        where TModel : IModel<IView>
    {
        public TModel Model { get; protected set; }
        public TView View { get; protected set; }

        private List<IDisposable> _disposables;

        public Controller(TView viewContract, TModel modelContract)
        {
            _disposables = new List<IDisposable>();
            Model = modelContract;
            View = viewContract;
        }

        public virtual void Open()
        {   
            View.Initialize();
        }

        public virtual void Close()
        {
            AddDisposable(Model);
            Dispose();
        }

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}