using System;
using ResourceProvider;
using UnityEngine;

namespace UI.MVC
{
    public interface IView : IResource, IDisposable
    {
        RectTransform RectTransform { get; }
        void Initialize();
        void OnDisplay();
    } 
}