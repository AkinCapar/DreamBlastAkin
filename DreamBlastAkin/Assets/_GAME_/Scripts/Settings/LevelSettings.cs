using System.Collections;
using System.Collections.Generic;
using DreamBlast.Data;
using DreamBlast.Utilities;
using UnityEngine;

namespace DreamBlast.Settings
{
    [CreateAssetMenu(fileName = nameof(LevelSettings), menuName = Constants.MenuNames.SETTINGS + nameof(LevelSettings))]
    public class LevelSettings : ScriptableObject
    {
        public List<LevelData> levels;
    }
}
