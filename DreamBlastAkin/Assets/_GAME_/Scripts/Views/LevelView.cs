using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace DreamBlast.Views
{
    public class LevelView : MonoBehaviour, IInitializable, IDisposable
    {
        
        
        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }
        
        public class Factory : PlaceholderFactory<Object, LevelView>
        {
        }
    }
}
