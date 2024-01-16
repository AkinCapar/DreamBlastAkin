using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float additionalSpeed;
        [SerializeField] private Rigidbody2D _rigidbody;
        public BubbleColors _bubbleColor;
        private IMemoryPool _pool;


        public void OnSpawned(BubbleData bubbleData, Transform spawnPos, int spawnNo, IMemoryPool pool)
        {
            transform.parent = spawnPos.parent.parent;
            transform.position = spawnPos.position + new Vector3(Random.Range(-.5f, .5f), spawnNo * onSpawnOffsetY, 0);
            bubbleBodyImage.sprite = bubbleData.bubbleBodySprite;
            bubbleFaceImage.sprite = bubbleData.bubbleFaceSprite;
            _bubbleColor = bubbleData.bubbleColor;
            _pool = pool;
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