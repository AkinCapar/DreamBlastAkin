using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamBlast.Utilities;
using DreamBlast.Settings;
using DreamBlast.Views;
using Zenject;

namespace DreamBlast.Controllers
{
    public class ScreenController
    {
        private ScreenStates _currentState;
        
        #region Injection

        private SignalBus _signalBus;

        public ScreenController(SignalBus signalBus)
        {
            _signalBus = signalBus;
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
                    SwitchToGameplayScreen();
                    break;
                
            }
        }

        private void SwitchToGameplayScreen()
        {
            _signalBus.Fire<SwitchedToGameplayScreenSignal>();
        }

        private void ClearScreens()
        {
            
        }
    }
}
