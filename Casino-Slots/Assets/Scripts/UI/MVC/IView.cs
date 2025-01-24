using System;
using ResourceProvider;
using Services.ResourceProvider;
using UnityEngine;

namespace UI.MVC
{
    public interface IView : IResource, IDisposable
    {
        event Action InitializeEvent;
        RectTransform RectTransform { get; }
        void Initialize();
        void OnDisplay();
    } 
}