using System;
using DreamBlast.Utilities;
using Zenject;

namespace DreamBlast.Controllers
{
    public class GameController : IInitializable, ITickable, IDisposable
    {
        private GameStates _gameState = GameStates.WaitingToStart;

        private ScreenController _screenController;
        private LevelController _levelController;
        private BubblesSpawnController _bubblesSpawnController;
        private BubblesController _bubblesController;

        public GameController(ScreenController screenController
            , LevelController levelController
            , BubblesSpawnController bubblesSpawnController
            , BubblesController bubblesController)
        {
            _screenController = screenController;
            _levelController = levelController;
            _bubblesSpawnController = bubblesSpawnController;
            _bubblesController = bubblesController;
        }

        public void Initialize()
        {
            
        }

        public void Tick()
        {
            switch (_gameState)
            {
                case GameStates.WaitingToStart:
                {
                    UpdateStarting();
                    break;
                }
                case GameStates.Playing:
                {
                    UpdatePlaying();
                    break;
                }
            }
        }

        private void UpdatePlaying()
        {
        }

        private void UpdateStarting()
        {
            if (_gameState != GameStates.WaitingToStart)
            {
                return;
            }

            StartGame();
        }

        private void StartGame()
        {
            if(_gameState != GameStates.WaitingToStart) { return;}
            
            _levelController.Initialize();
            _bubblesController.Initialize();
            _bubblesSpawnController.Initialize();
            _screenController.Initialize();
            _gameState = GameStates.Playing;
        }

        public GameStates GameState
        {
            get { return _gameState; }
        }

        public void Dispose()
        {
            _levelController.Dispose();
            _bubblesController.Dispose();
        }
    }
}
