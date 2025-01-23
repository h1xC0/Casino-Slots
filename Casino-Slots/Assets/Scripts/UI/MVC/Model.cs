namespace UI.MVC
{
    public class Model<TView> : IModel<TView>
        where TView : class, IView
    {
        public TView View { get; }

        public Model(TView view)
        {
            View = view;
        }

        public virtual void OnModelChanged()
        {
            View.OnDisplay();
        }

        public void Dispose()
        {
            View.Dispose();
        }
    }
}