using System.Collections;
using System.Collections.Generic;
using DreamBlast.Settings;
using DreamBlast.Utilities;
using DreamBlast.Views;
using UnityEngine;

namespace DreamBlast.Controllers
{
    public class BubblesController
    {
        private List<BubbleView> _remainingBubbles;
        private List<BubbleView> _bubblesToBeBlasted;
        private LevelSettings _levelSettings;
        private LevelModel _levelModel;

        public BubblesController(LevelSettings levelSettings
            , LevelModel levelModel)
        {
            _levelSettings = levelSettings;
            _levelModel = levelModel;
        }

        public void Initialize()
        {
            _remainingBubbles = new List<BubbleView>();
            _bubblesToBeBlasted = new List<BubbleView>();
        }

        public void AddRemainingBubble(BubbleView bubble)
        {
            _remainingBubbles.Add(bubble);
        }

        public void CheckBubble(BubbleView startBubble)
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

            if (_bubblesToBeBlasted.Count > _levelSettings.levels[_levelModel.CurrentLevel()].minBlastableContactAmount)
            {
                foreach (BubbleView bubble in _bubblesToBeBlasted)
                {
                    bubble.BlastBubble();
                    _remainingBubbles.Remove(bubble);
                }
            }
            
            Debug.Log("remaining bubbles: " + _remainingBubbles.Count);
            _bubblesToBeBlasted.Clear();
        }
    }
}