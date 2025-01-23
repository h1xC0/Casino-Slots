using System;
using UnityEngine;

namespace UI.MVC
{
    public abstract class View : MonoBehaviour, IView
    {
        public event Action InitializeEvent;
        public RectTransform RectTransform => (RectTransform)transform;
        
        public virtual void Initialize()
        {
            InitializeEvent?.Invoke();
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