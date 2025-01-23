using System;

namespace UI.MVC
{
    public interface IModel<out TView> : IDisposable
    {
        TView View { get;}
        void OnModelChanged();
    }
}