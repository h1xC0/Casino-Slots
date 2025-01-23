﻿using ResourceProvider;
using Slots.Game.Audio;
using Slots.Game.Events;
using Slots.Game.Rollers;
using UI.Credits;
using UI.WindowManager;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AudioService _audioServiceInstance;
        [SerializeField] private GameEventTriggerService _triggerServiceInstance;
        [SerializeField] private Roller _rollerPrefab;
        [SerializeField] private RollerItem _rollerItemPrefab;
        [SerializeField] private Transform _windowsParent;

        public override void InstallBindings()
        {
            BindServices();

            Container.BindFactory<Roller, RollerFactory>()
                .FromComponentInNewPrefab(_rollerPrefab);

            Container.BindFactory<RollerItem, RollerItemFactory>()
                .FromComponentInNewPrefab(_rollerItemPrefab);
            
            BindWindowSystem();
            BindWindows();
        }

        public override void Start()
        {
            base.Start();
            Container.Resolve<IWindowManager>().Open<CreditsController>();
        }

        private void BindServices()
        {
            Container
                .Bind<IAudioService>()
                .FromComponentInNewPrefab(_audioServiceInstance)
                .UnderTransform(transform)
                .AsSingle();

            Container
                .Bind<IEventTriggerService>()
                .FromComponentInNewPrefab(_triggerServiceInstance)
                .UnderTransform(transform)
                .AsSingle();

            Container
                .BindInterfacesTo<ResourceProviderService>()
                .FromNew()
                .AsSingle();
        }

        private void BindWindowSystem()
        {
            Container
                .BindInterfacesTo<WindowManager>()
                .FromNew()
                .AsSingle()
                .WithArguments(_windowsParent);

            Container
                .BindInterfacesTo<WindowFactory>()
                .FromNew()
                .AsSingle();
        }

        private void BindWindows()
        {
            Container
                .Bind<CreditsMapper>()
                .ToSelf()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}