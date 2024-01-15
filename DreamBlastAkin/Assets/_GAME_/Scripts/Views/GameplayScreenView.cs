using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace DreamBlast.Views
{
    public class GameplayScreenView : MonoBehaviour, IInitializable, IDisposable
    {
        public void Initialize()
        {
            Debug.Log("gameplay screen view is initialized");
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
