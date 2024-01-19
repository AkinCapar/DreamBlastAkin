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
            levelText.text = "LEVEL " + (levelCount + 1);
        }

        public void DestroyGameplayScreenView()
        {
            Destroy(gameObject);
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
