using System.Collections;
using System.Collections.Generic;
using DreamBlast.Utilities;
using DreamBlast.Views;
using UnityEngine;

namespace DreamBlast.Settings
{
    [CreateAssetMenu(fileName = nameof(PrefabSettings), menuName = Constants.MenuNames.SETTINGS + nameof(PrefabSettings))]
    public class PrefabSettings : ScriptableObject
    {
        public GameplayScreenView GameplayScreenView;
    }
}
