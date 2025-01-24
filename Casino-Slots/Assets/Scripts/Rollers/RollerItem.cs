using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Rollers
{
    public class RollerItem : MonoBehaviour
    {
        public RollerItemType Type { get; private set; }

        [SerializeField]
        private Image _image;

        private Roller _roller;
        private float _moveSpeed;
        private float _startMoveSpeed;
        private float _bottomLimit;

        public void Initialize(Roller roller, RollerItemType type, Sprite sprite, float moveSpeed, float bottomLimit)
        {
            _roller = roller;
            Type = type;
            _image.sprite = sprite;
            _startMoveSpeed = moveSpeed;
            _bottomLimit = bottomLimit;
        }

        public void RequestSpin()
        {
            _moveSpeed = _startMoveSpeed;
        }

        public void RequestStop(Action callback = null)
        {
            DOVirtual.Float(_moveSpeed, 0, 3f, newValue => _moveSpeed = newValue)
                .SetEase(Ease.InSine)
                .OnComplete(() => callback?.Invoke());
        }

        public void Spin()
        {
            if (_moveSpeed == 0)
            {
                return;
            }
            
            transform.localPosition -= _moveSpeed * Time.deltaTime * Vector3.up;
            if (transform.localPosition.y < _bottomLimit)
            {
                transform.localPosition = _roller.GetLastItemLocalPosition() + _roller.GetSpacingBetweenItems();
                _roller.MoveFirstItemToTheBack();
            }
        }
    }
}