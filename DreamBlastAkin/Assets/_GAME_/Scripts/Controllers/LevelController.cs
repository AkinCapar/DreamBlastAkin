using System.Collections;
using System.Collections.Generic;
using DreamBlast.Settings;
using DreamBlast.Views;
using UnityEngine;

namespace DreamBlast.Controllers
{
    public class LevelController
    {
        
        #region Injection

        private readonly PrefabSettings _prefabSettings;
        private readonly LevelView.Factory _levelViewFactory;

        public LevelController(PrefabSettings prefabSettings
            , LevelView.Factory gameplayScreenViewFactory)
        {
            _prefabSettings = prefabSettings;
            _levelViewFactory = gameplayScreenViewFactory;
        }

        #endregion
        public void Initialize()
        {
            
        }
    }
}
