using System.Collections;
using System.Collections.Generic;
using DreamBlast.Settings;
using DreamBlast.Views;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace DreamBlast.Controllers
{
    public class BubblesSpawnController
    {
        private SignalBus _signalBus;
        private BubblesSettings _bubblesSettings;
        private BubbleView.Factory _bubbleViewFactory;

        public BubblesSpawnController(SignalBus signalBus
            , BubblesSettings bubblesSettings
            , BubbleView.Factory bubbleViewFactory)
        {
            _signalBus = signalBus;
            _bubblesSettings = bubblesSettings;
            _bubbleViewFactory = bubbleViewFactory;
        }

        public void Initialize()
        {
        }

        /*public async UniTaskVoid SpawnBubbles(int bubbleAmount, List<Transform> spawnPositions)
        {
            Debug.Log(bubbleAmount + " bubbles are spawned");
            var bubbleTypes = _bubblesSettings.bubbleTypes;
            
            for (int i = 0; i < bubbleAmount; i++)
            {
                BubbleView view = _bubbleViewFactory.Create(
                    bubbleTypes[Random.Range(0, bubbleTypes.Count)],
                    spawnPositions[Random.Range(0, spawnPositions.Count)]);
                
                await UniTask.Delay(50);
            }
        }*/
        public void SpawnBubbles(int bubbleAmount, List<Transform> spawnPositions)
        {
            Debug.Log(bubbleAmount + " bubbles are spawned");
            var bubbleTypes = _bubblesSettings.bubbleTypes;
            
            for (int i = 0; i < bubbleAmount; i++)
            {
                BubbleView view = _bubbleViewFactory.Create(
                    bubbleTypes[Random.Range(0, bubbleTypes.Count)],
                    spawnPositions[Random.Range(0, spawnPositions.Count)], i);
            }
        }
    }
}