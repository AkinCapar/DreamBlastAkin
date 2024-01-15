using System.Collections;
using System.Collections.Generic;
using DreamBlast.Data;
using DreamBlast.Utilities;
using UnityEngine;

namespace DreamBlast.Settings
{
    [CreateAssetMenu(fileName = nameof(BubblesSettings), menuName = Constants.MenuNames.SETTINGS + nameof(BubblesSettings))]
    public class BubblesSettings : ScriptableObject
    {
        public List<BubbleData> bubbleTypes;
    }
}
