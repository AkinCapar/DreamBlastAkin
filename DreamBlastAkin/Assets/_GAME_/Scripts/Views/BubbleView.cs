using System.Collections.Generic;
using DG.Tweening;
using DreamBlast.Controllers;
using DreamBlast.Data;
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
        private List<Collider2D> _contactingColliders;
        private BubbleColors _bubbleColor;
        private IMemoryPool _pool;
        private bool _blasted;

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
            _blasted = false;
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
            if (!_blasted)
            { 
                _bubblesController.CheckBubble(this);
            }
        }

        public List<Collider2D> GetContactColliders()
        {
            return _contactingColliders;
        }

        public BubbleColors GetBubbleColor()
        {
            return _bubbleColor;
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
            _blasted = true;
            transform.DOScale(Vector3.zero, .25f).OnComplete(OnBlastTweenComplete);
        }

        private void OnBlastTweenComplete()
        {
            Despawn();
        }

        public void Despawn()
        {
            _pool.Despawn(this);
        }
        public void OnDespawned()
        {
            transform.DOScale(Vector3.one, .1f);
        }

        public class Factory : PlaceholderFactory<BubbleData, Transform, int, BubbleView>
        {
        }

        public class Pool : MonoPoolableMemoryPool<BubbleData, Transform, int, IMemoryPool, BubbleView>
        {
        }
    }
}