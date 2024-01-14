using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayScreenView : MonoBehaviour, IInitializable, IDisposable
{
    public void Initialize()
    {
    }

    public void Dispose()
    {
    }
    
    
    public class Factory : PlaceholderFactory<UnityEngine.Object, GameplayScreenView> {}
}
