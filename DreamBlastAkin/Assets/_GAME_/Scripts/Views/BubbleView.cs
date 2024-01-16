using System;
using System.Collections;
using System.Collections.Generic;
using DreamBlast.Controllers;
using DreamBlast.Data;
using DreamBlast.Settings;
using DreamBlast.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace DreamBlast.Views
{
    public class BubbleView : MonoBehaviour, IPoolable<BubbleData, Transform, int, IMemoryPool>
    {
        [SerializeField] private Image bubbleBodyImage;
        [SerializeField] private Image bubbleFaceImage;
        [SerializeField] private float onSpawnOffsetY;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int maxContactAmount;
        private List<Collider2D> _contactingColliders;
        public BubbleColors _bubbleColor;
        private IMemoryPool _pool;

        #region Injection

        private BubblesController _bubblesController;

        [Inject]
        public void Construct(BubblesController bubblesController)
        {
            _bubblesController = bubblesController;
        }

        #endregion

        public void OnSpawned(BubbleData bubbleData, Transform spawnPos, int spawnNo, IMemoryPool pool)
        {
            transform.parent = spawnPos.parent.parent;
            transform.position = spawnPos.position + new Vector3(Random.Range(-.5f, .5f), spawnNo * onSpawnOffsetY, 0);
            bubbleBodyImage.sprite = bubbleData.bubbleBodySprite;
            bubbleFaceImage.sprite = bubbleData.bubbleFaceSprite;
            _bubbleColor = bubbleData.bubbleColor;
            _pool = pool;
            _contactingColliders = new List<Collider2D>();
        }

        public void OnPointerUp()
        {
            BubbleCheck();
        }

        public void BubbleCheck()
        {
            if (_contactingColliders.Count < 1)
            {
                return;
            }

            _bubblesController.CheckBubble(this, _contactingColliders);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_contactingColliders.Contains(other))
            {
                _contactingColliders.Add(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_contactingColliders.Contains(other))
            {
                _contactingColliders.Remove(other);
            }
        }

        public void BlastBubble()
        {
            _pool.Despawn(this);
        }

        public Rigidbody2D GetRigidBody2D()
        {
            return _rigidbody;
        }

        public void OnDespawned()
        {
        }

        public class Factory : PlaceholderFactory<BubbleData, Transform, int, BubbleView>
        {
        }

        public class Pool : MonoPoolableMemoryPool<BubbleData, Transform, int, IMemoryPool, BubbleView>
        {
        }
    }
}