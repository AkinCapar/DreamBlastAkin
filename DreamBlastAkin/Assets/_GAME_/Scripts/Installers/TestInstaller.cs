using System.Collections;
using System.Collections.Generic;
using DreamBlast.Controllers;
using DreamBlast.Data;
using DreamBlast.Settings;
using DreamBlast.Views;
using UnityEngine;
using Zenject;

public class TestInstaller : Installer<TestInstaller>
{
    private PrefabSettings _prefabSettings;

    public override void InstallBindings()
    {
        _prefabSettings = Container.Resolve<PrefabSettings>();

        //MODELS
        Container.Bind<LevelModel>().AsSingle();

        //CONTROLLERS
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        Container.Bind<ScreenController>().AsSingle();
        Container.Bind<LevelController>().AsSingle();
        Container.BindInterfacesAndSelfTo<BubblesController>().AsSingle();
        Container.Bind<BubblesSpawnController>().AsSingle();

        //FACTORIES
        Container.BindFactory<Object, GameplayScreenView, GameplayScreenView.Factory>()
            .FromFactory<PrefabFactory<GameplayScreenView>>();
        Container.BindFactory<Object, LevelUIView, LevelUIView.Factory>().FromFactory<PrefabFactory<LevelUIView>>();

        InstallBubbles();
    }

    private void InstallBubbles()
    {
        if (_prefabSettings.bubbleView != null)
        {
            Container.BindFactory<BubbleData, Transform, int, BubbleView, BubbleView.Factory>()
                .FromPoolableMemoryPool<BubbleData, Transform, int, BubbleView, BubbleView.Pool>(poolBinder =>
                    poolBinder
                        .WithInitialSize(100)
                        .FromComponentInNewPrefab(_prefabSettings.bubbleView)
                        .UnderTransformGroup("bubbles"));
        }
    }
}