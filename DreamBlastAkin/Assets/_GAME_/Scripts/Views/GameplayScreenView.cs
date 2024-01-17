using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace DreamBlast.Views
{
    public class GameplayScreenView : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPositions;
        [SerializeField] private TextMeshProUGUI levelText;
        public void Initialize(int levelCount)
        {
            Debug.Log("gameplay screen view is initialized");
            levelText.text = "LEVEL " + levelCount + 1;
        }

        public List<Transform> SpawnPositions()
        {
            return spawnPositions;
        }
        public class Factory : PlaceholderFactory<Object, GameplayScreenView>
        {
        }
    }
}
