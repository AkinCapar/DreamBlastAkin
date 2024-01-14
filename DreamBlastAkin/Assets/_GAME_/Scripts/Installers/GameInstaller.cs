using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DreamBlast.Controllers;

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
            
            Container.BindFactory<Object, GameplayScreenView, GameplayScreenView.Factory>().FromFactory<PrefabFactory<GameplayScreenView>>();
            GameSignalsInstaller.Install(Container);
        }
    }
}
