using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace DreamBlast.Views
{
    public class GameplayScreenView : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private List<Transform> _spawnPositions;
        public void Initialize()
        {
            Debug.Log("gameplay screen view is initialized");
        }

        public List<Transform> SpawnPositions()
        {
            return _spawnPositions;
        }

        public void Dispose()
        {
            Debug.Log("gameplay screen view is disposed");
        }


        public class Factory : PlaceholderFactory<Object, GameplayScreenView>
        {
        }
    }
}
