using System;
using System.Collections;
using System.Collections.Generic;
using DreamBlast.Views;
using UnityEngine;

namespace DreamBlast.Data
{
    [Serializable]
    public class LevelData
    {
        public GameplayScreenView LevelPrefab;
        public int colorsCount;
        public int bubblesCount;
    }
}
