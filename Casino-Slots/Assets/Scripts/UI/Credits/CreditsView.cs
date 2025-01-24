using System;
using DG.Tweening;
using Events.Data;
using Slots.Game.Events;
using TMPro;
using UI.MVC;
using UnityEngine;

namespace UI.Credits
{
    public class CreditsView : View, ICreditsView
    {
        public event Action<long> TransferCreditsEvent;
        public IBetView BetView => _betView;
        [SerializeField] private BetView _betView;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _creditsTransform;

        private const float _creditsWaitDelay = 0.65f;
        private const float _creditCountingDelay = 1f;

        private Vector3 _originalScale;
        private float _credits;

        public override void Initialize()
        {
            base.Initialize();
            _text.text = ToMoneyFormat(_credits);
            _originalScale = _creditsTransform.localScale;
            _betView.Initialize();
        }

        public void OnDisplay(long credits, long bet, int betSize, bool animate = false)
        {
            ShowAnimationCredits(credits, animate);
            _betView.OnDisplay(credits, bet, betSize);
        }

        public void ShowCredits(IGameEventData gameEventData)
        {
            if (gameEventData is not ICreditsData creditsPopupData)
                return;
            
            TransferCreditsEvent?.Invoke(creditsPopupData.CreditsAmount);
            Debug.Assert(creditsPopupData != null);
        }

        private void ShowAnimationCredits(float modelCredits, bool animate = false)
        {
            if(animate == false)
            {
                _credits = modelCredits;
                _text.text = ToMoneyFormat(_credits);
                return;
            }

            DOVirtual.Float(_credits, modelCredits, _creditCountingDelay, newValue => 
            {
                _credits = newValue;
                _text.text = ToMoneyFormat(_credits);
            });
            AnimateCredits();
        }

        private Sequence AnimateCredits()
        {
            var sequence = DOTween.Sequence();
            var scaleMultiplier = .2f;

            sequence.Append(_creditsTransform.DOScale(_creditsTransform.localScale * (1 + scaleMultiplier), _creditsWaitDelay));

            sequence.SetEase(Ease.OutQuad);
            sequence.SetLoops(15, LoopType.Yoyo);
            sequence.OnComplete(() => _creditsTransform.DOScale(_originalScale, 0.5f));

            return sequence;
        }

        private string ToMoneyFormat(float money)
        {
            return String.Format("{0:C}", money);
        }

    }
}