using UnityEngine;

namespace UI.MVC
{
    public class View : MonoBehaviour, IView
    {
        public RectTransform RectTransform => (RectTransform)transform;
        
        public virtual void Initialize()
        {

        }

        public virtual void OnDisplay()
        {

        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}