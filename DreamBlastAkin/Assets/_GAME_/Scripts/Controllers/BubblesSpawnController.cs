using System.Collections;
using System.Collections.Generic;
using DreamBlast.Settings;
using DreamBlast.Views;
using UnityEngine;
using Zenject;

namespace DreamBlast.Controllers
{
    public class BubblesSpawnController
    {
        private SignalBus _signalBus;
        private BubblesSettings _bubblesSettings;
        private BubbleView.Factory _bubbleViewFactory;
        private BubblesController _bubblesController;

        public BubblesSpawnController(SignalBus signalBus
            , BubblesSettings bubblesSettings
            , BubbleView.Factory bubbleViewFactory
            , BubblesController bubblesController)
        {
            _signalBus = signalBus;
            _bubblesSettings = bubblesSettings;
            _bubbleViewFactory = bubbleViewFactory;
            _bubblesController = bubblesController;
        }

        public void Initialize()
        {
        }
        public void SpawnBubbles(int bubbleAmount, List<Transform> spawnPositions, int colorCount)
        {
            var bubbleTypes = _bubblesSettings.bubbleTypes;
            
            for (int i = 0; i < bubbleAmount; i++)
            {
                BubbleView view = _bubbleViewFactory.Create(
                    bubbleTypes[Random.Range(0, colorCount)],
                    spawnPositions[Random.Range(0, spawnPositions.Count)], i);
                _bubblesController.AddRemainingBubble(view);
            }
        }
    }
}