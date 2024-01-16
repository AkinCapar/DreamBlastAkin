using UnityEngine;
using Zenject;
using DreamBlast.Controllers;
using DreamBlast.Data;
using DreamBlast.Views;
using DreamBlast.Settings;

namespace DreamBlast.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _bubbleContainer;
        
        #region Injection

        private PrefabSettings _prefabSettings;

        [Inject]
        private void Construct(PrefabSettings prefabSettings)
        {
            _prefabSettings = prefabSettings;
        }

        #endregion
        public override void InstallBindings()
        {
            GameSignalsInstaller.Install(Container);
            
            
            //MODELS
            Container.Bind<LevelModel>().AsSingle();
            //Container.Bind<LevelUIView>().AsSingle();
            
            //CONTROLLERS
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.Bind<ScreenController>().AsSingle();
            Container.Bind<LevelController>().AsSingle();
            Container.Bind<BubblesSpawnController>().AsSingle();
            
            //FACTORIES
            Container.BindFactory<Object, GameplayScreenView, GameplayScreenView.Factory>().FromFactory<PrefabFactory<GameplayScreenView>>();
            Container.BindFactory<Object, LevelUIView, LevelUIView.Factory>().FromFactory<PrefabFactory<LevelUIView>>();
            
            InstallBubbles();
        }
        
        private void InstallBubbles()
        {
            Container.BindFactory<BubbleData, Transform, int, BubbleView, BubbleView.Factory>()
                .FromPoolableMemoryPool<BubbleData, Transform, int, BubbleView, BubbleView.Pool>(poolBinder => poolBinder
                    .WithInitialSize(100)
                    .FromComponentInNewPrefab(_prefabSettings.bubbleView)
                    .UnderTransform(_bubbleContainer));
        }
    }
}
