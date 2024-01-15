using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamBlast.Utilities;
using DreamBlast.Settings;
using DreamBlast.Views;

namespace DreamBlast.Controllers
{
    public class ScreenController
    {
        private ScreenStates _currentState;
        private GameplayScreenView _gameplayScreenView;
        
        #region Injection

        private readonly PrefabSettings _prefabSettings;
        private readonly GameplayScreenView.Factory _gameplayScreenViewFactory;

        public ScreenController(PrefabSettings prefabSettings
            , GameplayScreenView.Factory gameplayScreenViewFactory)
        {
            _prefabSettings = prefabSettings;
            _gameplayScreenViewFactory = gameplayScreenViewFactory;
        }

        #endregion
        public void Initialize()
        {
            CreateState(ScreenStates.GameplayState);
        }
        
        public void ChangeState(ScreenStates state)
        {
            CreateState(state);
        }

        private void CreateState(ScreenStates state)
        {
            ClearScreens();
            _currentState = state;

            switch (_currentState)
            {
                case ScreenStates.GameplayState:
                    CreateGameplayScreen();
                    break;
                
            }
        }

        private void CreateGameplayScreen()
        {
            _gameplayScreenView = _gameplayScreenViewFactory.Create(_prefabSettings.GameplayScreenView);
            _gameplayScreenView.Initialize();
        }

        private void ClearScreens()
        {
            
        }
    }
}
