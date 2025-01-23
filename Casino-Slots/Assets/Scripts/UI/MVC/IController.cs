using System;

namespace UI.MVC
{
    public interface IController<out TView, out TModel> : IDisposable
        where TView : IView
        where TModel : IModel<IView>
    {
        TModel Model { get;}
        TView View { get; }
        void Open();
        void Close();
    }    
}