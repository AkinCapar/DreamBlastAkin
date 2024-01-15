using System.Collections;
using System.Collections.Generic;
using DreamBlast.Settings;
using UnityEngine;
using Zenject;

namespace DreamBlast.Controllers
{
    public class BubblesSpawnController
    {
        private SignalBus _signalBus;
        private BubblesSettings _bubblesSettings;
        
        public BubblesSpawnController(SignalBus signalBus
            , BubblesSettings bubblesSettings)
        {
            _signalBus = signalBus;
            _bubblesSettings = bubblesSettings;
        }
        public void Initialize()
        {
            
        }

        public void SpawnBubbles(int bubbleAmount)
        {
            Debug.Log(bubbleAmount + " bubbles are spawned");
        }
    }
}
