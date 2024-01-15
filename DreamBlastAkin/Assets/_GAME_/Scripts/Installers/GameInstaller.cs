using UnityEngine;
using Zenject;
using DreamBlast.Controllers;
using DreamBlast.Views;

namespace DreamBlast.Installer
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //CONTROLLERS
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.Bind<ScreenController>().AsSingle();
            Container.Bind<LevelController>().AsSingle();
            
            //MODELS
            Container.Bind<LevelModel>().AsSingle();
            Container.Bind<LevelView>().AsSingle();
            
            //FACTORIES
            Container.BindFactory<Object, GameplayScreenView, GameplayScreenView.Factory>().FromFactory<PrefabFactory<GameplayScreenView>>();
            Container.BindFactory<Object, LevelView, LevelView.Factory>().FromFactory<PrefabFactory<LevelView>>();
            
            
            GameSignalsInstaller.Install(Container);
        }
    }
}
