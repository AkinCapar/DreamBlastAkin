using System.Collections;
using System.Collections.Generic;
using DreamBlast.Settings;
using DreamBlast.Views;
using DreamBlast.Data;
using Zenject;

namespace DreamBlast.Controllers
{
    public class LevelController
    {
        private GameplayScreenView _gameplayScreenView;
        
        #region Injection

        private readonly PrefabSettings _prefabSettings;
        private readonly LevelSettings _levelSettings;
        private readonly LevelUIView.Factory _levelUIViewFactory;
        private readonly GameplayScreenView.Factory _gameplayScreenViewFactory;
        private LevelModel _levelModel;
        private SignalBus _signalBus;
        private BubblesSpawnController _bubblesSpawnController;

        public LevelController(PrefabSettings prefabSettings
            , LevelSettings levelSettings
            , LevelUIView.Factory levelUIViewFactory
            , GameplayScreenView.Factory gameplayScreenViewFactory
            , SignalBus signalBus
            , LevelModel levelModel
            , BubblesSpawnController bubblesSpawnController)
        {
            _prefabSettings = prefabSettings;
            _levelSettings = levelSettings;
            _levelUIViewFactory = levelUIViewFactory;
            _gameplayScreenViewFactory = gameplayScreenViewFactory;
            _signalBus = signalBus;
            _levelModel = levelModel;
            _bubblesSpawnController = bubblesSpawnController;
        }

        #endregion
        public void Initialize()
        {
            _signalBus.Subscribe<SwitchedToGameplayScreenSignal>(OnSwitchedToGameplayScreen);
        }

        private void OnSwitchedToGameplayScreen()
        {
            LevelData currentLevelData = _levelSettings.levels[_levelModel.CurrentLevel()];
            _gameplayScreenView = _gameplayScreenViewFactory.Create(currentLevelData.LevelPrefab);
            _gameplayScreenView.Initialize();
            _bubblesSpawnController.SpawnBubbles(currentLevelData.bubblesCount);
        }

        public void Dispose()
        {            
            _signalBus.Unsubscribe<SwitchedToGameplayScreenSignal>(OnSwitchedToGameplayScreen);
        }
    }
}
