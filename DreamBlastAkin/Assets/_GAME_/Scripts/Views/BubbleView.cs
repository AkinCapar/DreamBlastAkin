using System;
using System.Collections;
using System.Collections.Generic;
using DreamBlast.Data;
using DreamBlast.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DreamBlast.Views
{
    public class BubbleView : MonoBehaviour, IPoolable<BubbleData, IMemoryPool>
    {
        [SerializeField] private Image bubbleBodyImage;
        [SerializeField] private Image bubbleFaceImage;
        public BubbleColors _bubbleColor;
        private IMemoryPool _pool;


        public void OnSpawned(BubbleData bubbleData, IMemoryPool pool)
        {
            bubbleBodyImage.sprite = bubbleData.bubbleBodySprite;
            bubbleFaceImage.sprite = bubbleData.bubbleFaceSprite;
            _bubbleColor = bubbleData.bubbleColor;
            _pool = pool;
        }
        
        public void OnDespawned()
        {
            
        }
        
        public class Factory : PlaceholderFactory<BubbleData, BubbleView>
        {
        }

        public class Pool : MonoPoolableMemoryPool<BubbleData, IMemoryPool, BubbleView>
        {
        }
    }
}
