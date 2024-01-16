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
        private List<BubbleView> _bublesToBeBlasted;
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
            _bublesToBeBlasted = new List<BubbleView>();
        }

        public void AddBubble(BubbleView bubble)
        {
            _remainingBubbles.Add(bubble);
        }

        public void CheckBubble(BubbleView bubbleView, List<Collider2D> contactColliders)
        {
            for (int i = 0; i < contactColliders.Count; i++)
            {
                if (contactColliders[i].GetComponent<BubbleView>() != null)
                {
                    BubbleView contactBubble = contactColliders[i].GetComponent<BubbleView>();

                    if (bubbleView._bubbleColor == contactBubble._bubbleColor && !_bublesToBeBlasted.Contains(contactBubble))
                    {
                        _bublesToBeBlasted.Add(contactBubble);
                    }
                }
            }
        }

        private void BlastBubbles()
        {
            foreach (BubbleView bubble in _bublesToBeBlasted)
            {
                _bublesToBeBlasted.Remove(bubble);
                bubble.BlastBubble();
            }
        }
    }
}