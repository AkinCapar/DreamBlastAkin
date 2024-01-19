using Zenject;
using System.Collections;
using System.Collections.Generic;
using DreamBlast.Controllers;
using DreamBlast.Installer;
using DreamBlast.Settings;
using DreamBlast.Views;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelCompletionTest : ZenjectIntegrationTestFixture
{
    private LevelModel _levelModel;
    private GameController _gameController;
    private BubblesController _bubblesController;
    private List<BubbleView> _remainingBubbles;
    
    [UnityTest]
    public IEnumerator Test()
    {
        PreInstall();
        Bind();
        PostInstall();
        Initialize();

        int currentLevel = _levelModel.CurrentLevel();
        
        for (int i = 0; i < _remainingBubbles.Count; i++)
        {
            _remainingBubbles[i].BlastBubble();
        }
        
        _bubblesController.CheckForLevelCompletion();
        
        yield return new WaitForSeconds(3f);
        
        Assert.True(_levelModel.CurrentLevel() - currentLevel == 1);
        
    }
    
    private void Bind()
    {
        BindSettings();
        GameSignalsInstaller.Install(Container);
        TestInstaller.Install(Container);
    }

    private void Initialize()
    {
        _gameController = Container.Resolve<GameController>();
        _gameController.Tick();
        _bubblesController = Container.Resolve<BubblesController>();
        _levelModel = Container.Resolve<LevelModel>();
        _remainingBubbles = _bubblesController.GetRemainingBubbles();
    }

    private void BindSettings()
    {
        LevelSettings levelSettings =
            AssetDatabase.LoadAssetAtPath<LevelSettings>("Assets/Resources/TestSettings/LevelSettings 1.asset");
        Container.BindInterfacesAndSelfTo(levelSettings.GetType()).FromInstance(levelSettings);
        BubblesSettings bubblesSettings =
            AssetDatabase.LoadAssetAtPath<BubblesSettings>("Assets/Resources/TestSettings/BubblesSettings 1.asset");
        Container.BindInterfacesAndSelfTo(bubblesSettings.GetType()).FromInstance(bubblesSettings);
        PrefabSettings prefabSettings =
            AssetDatabase.LoadAssetAtPath<PrefabSettings>("Assets/Resources/TestSettings/PrefabSettings 1.asset");
        Container.BindInterfacesAndSelfTo(prefabSettings.GetType()).FromInstance(prefabSettings);
    }
    
}