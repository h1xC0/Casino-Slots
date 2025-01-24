﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Slots.Game.Audio;
using Slots.Game.Libraries;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Rollers
{
    public class Roller : MonoBehaviour
    {
        public bool IsSpinning { get; private set; }

        private List<RollerItem> _items;

        [Inject] private IAudioService _audioService;
        [Inject] private RollerItemFactory _rollerItemFactory;

        private const float _minSpinTimeInSeconds = .5f;
        private const float _maxSpinTimeInSeconds = 1f;
        private const float _centerItemDuration = .5f;

        private const float _itemSpinSpeed = 2000f;
        private const float _startingItemYPosition = -143.31f;
        private const float _spacingBetweenItems = 212f;
        private const float _itemBottomLimit = -355f;

        private bool _centerItemsOnScreen;

        public void Initialize(RollerSequenceLibrary itemSequence, SpriteLibrary spriteAssets)
        {
            _items = new List<RollerItem>();
            InstantiateAndAddRollerItemsToList(itemSequence, spriteAssets);
        }

        private void Update()
        {
            if (!IsSpinning)
            {
                return;
            }
            
            SpinItems();
        }

        public void StartSpin()
        {
            IsSpinning = true;
            RequestSpinState(true);
        }

        public void StartSpinCountdown()
        {
            float currentSpinTmeInSeconds = Random.Range(_minSpinTimeInSeconds, _maxSpinTimeInSeconds);
            StartCoroutine(StopSpinAfterDelay(currentSpinTmeInSeconds));
        }

        public void MoveFirstItemToTheBack()
        {
            var firstItem = _items[0];
            _items.Add(firstItem);
            _items.RemoveAt(0);
        }

        public Vector3 GetSpacingBetweenItems()
        {
            return Vector3.up * _spacingBetweenItems;
        }

        public Vector3 GetLastItemLocalPosition()
        {
            return _items[^1].transform.localPosition;
        }

        public void GetRollerItemsOnScreen(out List<int> itemsOnScreen)
        {
            itemsOnScreen = new List<int>();
            for (int i = RollerManager.NumberOfRowsInGrid - 1; i >= 0; --i)
            {
                itemsOnScreen.Add((int)_items[i].Type);
            }
        }

        private void InstantiateAndAddRollerItemsToList(RollerSequenceLibrary itemSequence, SpriteLibrary spriteLoader)
        {
            for (int i = 0; i < itemSequence.Assets.Length; ++i)
            {
                var item = _rollerItemFactory.Create();
                item.transform.SetParent(transform);
                
                var itemLocalPosition = Vector3.up * _startingItemYPosition + (i * GetSpacingBetweenItems());
                item.transform.localPosition = itemLocalPosition;
                item.transform.localScale = Vector3.one;
                
                var itemType = itemSequence.Assets[i];
                var itemSprite = spriteLoader.Assets[(int)itemType];
                
                item.Initialize(this, itemType, itemSprite, _itemSpinSpeed, _itemBottomLimit);
                _items.Add(item);
            }
        }

        private void SpinItems()
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                _items[i].Spin();
            }
        }

        private IEnumerator StopSpinAfterDelay(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            RequestStopSpin();
            CenterItemsOnScreenIfNecessary();
            // RequestSpinState(false, CenterItemsOnScreenIfNecessary);
        }

        private void RequestStopSpin()
        {
            IsSpinning = false;
            _centerItemsOnScreen = true;
            _audioService.Play("Stop Roller");
        }

        private void CenterItemsOnScreenIfNecessary()
        {
            if (_centerItemsOnScreen)
            {
                Vector3 localPosition = Vector3.zero;
                for (int i = 0; i < _items.Count; ++i)
                {
                    localPosition = Vector3.up * _startingItemYPosition + (i * GetSpacingBetweenItems());
                    _items[i].transform.DOLocalMove(localPosition, _centerItemDuration).SetEase(Ease.OutCubic);
                }
                if (_items[^1] && Mathf.Abs(_items[^1].transform.localPosition.y - localPosition.y) < 0.01f)
                {
                    _centerItemsOnScreen = false;
                }
            }
        }

        private void RequestSpinState(bool flag, Action callback = null)
        {
            foreach (var rollerItem in _items)
            {
                if(flag)
                    rollerItem.RequestSpin();
                else
                    rollerItem.RequestStop(callback);
            }
        }
    }
}