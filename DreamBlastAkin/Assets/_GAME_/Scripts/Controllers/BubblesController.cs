using System.Collections;
using System.Collections.Generic;
using DreamBlast.Settings;
using DreamBlast.Utilities;
using DreamBlast.Views;
using UnityEngine;
using Zenject;
using BubbleView = DreamBlast.Views.BubbleView;

namespace DreamBlast.Controllers
{
    public class BubblesController : ITickable
    {
        private List<BubbleView> _remainingBubbles;
        private List<BubbleView> _bubblesToBeBlasted;
        private SignalBus _signalBus;
        private LevelSettings _levelSettings;
        private LevelModel _levelModel;
        private bool _timerEnabled;
        private float _timer;
        private bool _stopLevelCompletionCheck;

        public BubblesController(LevelSettings levelSettings
            , LevelModel levelModel
            , SignalBus signalBus)
        {
            _levelSettings = levelSettings;
            _levelModel = levelModel;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _remainingBubbles = new List<BubbleView>();
            _bubblesToBeBlasted = new List<BubbleView>();
            _signalBus.Subscribe<LevelCompletedSignal>(OnLevelCompletedSignal);
        }

        public void AddRemainingBubble(BubbleView bubble)
        {
            _remainingBubbles.Add(bubble);
        }

        public void CheckBubble(BubbleView startBubble, bool checkForLevelCompletion = false)
        {
            if (startBubble == null)
            {
                return;
            }

            HashSet<BubbleView> visitedBubbles = new HashSet<BubbleView>();
            Queue<BubbleView> bubblesToCheck = new Queue<BubbleView>();

            bubblesToCheck.Enqueue(startBubble);
            visitedBubbles.Add(startBubble);

            while (bubblesToCheck.Count > 0)
            {
                BubbleView currentBubble = bubblesToCheck.Dequeue();

                if (!_bubblesToBeBlasted.Contains(currentBubble))
                {
                    _bubblesToBeBlasted.Add(currentBubble);
                }

                List<Collider2D> contactColliders = currentBubble.GetContactColliders();

                foreach (Collider2D collider in contactColliders)
                {
                    BubbleView contactBubble = collider.GetComponent<BubbleView>();

                    if (contactBubble != null &&
                        currentBubble.GetBubbleColor() == contactBubble.GetBubbleColor() &&
                        !visitedBubbles.Contains(contactBubble))
                    {
                        visitedBubbles.Add(contactBubble);
                        bubblesToCheck.Enqueue(contactBubble);
                    }
                }
            }

            if (_bubblesToBeBlasted.Count >=
                _levelSettings.levels[_levelModel.CurrentLevel()].minBlastableContactAmount)
            {
                if (checkForLevelCompletion)
                {
                    _stopLevelCompletionCheck = true;
                }

                else
                {
                    foreach (BubbleView bubble in _bubblesToBeBlasted)
                    {
                        bubble.BlastBubble();
                        _remainingBubbles.Remove(bubble);
                    }

                    StartTimerForLevelCompletionCheck();
                }
            }

            _bubblesToBeBlasted.Clear();
        }

        private void StartTimerForLevelCompletionCheck()
        {
            _timer = 0;
            _timerEnabled = true;
        }

        private void OnLevelCompletedSignal()
        {
            foreach (BubbleView bubbleView in _remainingBubbles)
            {
                bubbleView.Despawn();
            }

            _remainingBubbles.Clear();
        }

        private void CheckForLevelCompletion()
        {
            for (int i = 0; i < _remainingBubbles.Count; i++)
            {
                CheckBubble(_remainingBubbles[i], true);
                if (_stopLevelCompletionCheck)
                {
                    _stopLevelCompletionCheck = false;
                    return;
                }
            }

            _signalBus.Fire<NoBubblesLeftToBlastSignal>();
            Debug.Log("There is no blastable bubbles contact left");
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<LevelCompletedSignal>(OnLevelCompletedSignal);
        }

        public void Tick()
        {
            if (_timerEnabled)
            {
                _timer += Time.deltaTime;
            }

            if (_timer > 5)
            {
                _timer = 0;
                _timerEnabled = false;
                CheckForLevelCompletion();
            }
        }
    }
}