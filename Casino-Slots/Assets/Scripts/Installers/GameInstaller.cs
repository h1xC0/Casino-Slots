using Patterns;
using ResourceProvider;
using Rewards;
using Rollers;
using Services.ResourceProvider;
using Slots.Game.Audio;
using Slots.Game.Events;
using UI.Credits;
using UI.Reels;
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
            var windowManager = Container.Resolve<IWindowManager>();

            windowManager.Open<CreditsController>();
            windowManager.Open<ReelsController>();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesTo<ResourceProviderService>()
                .FromNew()
                .AsSingle();
            
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
            
            Container.Bind<IGridToLineConverter>()
                .To<GridToLineConverter>()
                .AsSingle();

            Container.Bind<ILinePatternChecker>()
                .To<LinePatternChecker>()
                .AsSingle();

            Container.BindInterfacesTo<PayTableRewardsRetriever>()
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
            
            Container
                .Bind<ReelsMapper>()
                .ToSelf()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}