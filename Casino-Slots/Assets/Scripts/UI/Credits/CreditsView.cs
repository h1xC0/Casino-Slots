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
        [SerializeField] private TextMeshProUGUI _winningsTMP;
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

            AnimateWinnings(creditsPopupData.CreditsAmount);
            TransferCreditsEvent?.Invoke(creditsPopupData.CreditsAmount);
            Debug.Assert(creditsPopupData != null);
        }

        public void ShowLosings(IGameEventData gameEventData)
        {
            var spinResultData = gameEventData as SpinResultData;

            if(spinResultData?.SpinResultGrid.NumberOfColumns == 0 && spinResultData.SpinResultGrid.NumberOfRows == 0)
                AnimateWinnings(0);
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
            AnimateCredits(_creditsTransform);
        }

        private Sequence AnimateCredits(Transform transform)
        {
            var sequence = DOTween.Sequence();
            var scaleMultiplier = .2f;

            sequence.Append(transform.DOScale(transform.localScale * (1 + scaleMultiplier), _creditsWaitDelay));

            sequence.SetEase(Ease.OutQuad);
            sequence.SetLoops(10, LoopType.Yoyo);
            sequence.OnComplete(() => transform.DOScale(Vector3.one, 0.5f));

            return sequence;
        }
        
        private Sequence AnimateWinnings(long credits)
        {
            var sequence = DOTween.Sequence();
            _winningsTMP.text = credits > 0 ? "You won " + ToMoneyFormat(credits) + " credits" : "<color=red>No win</color>";
            _winningsTMP.enableVertexGradient = credits > 0;
            sequence.SetEase(Ease.OutSine);

            sequence.Append(DOVirtual.Float(_winningsTMP.alpha, 1, 1, newValue => _winningsTMP.alpha = newValue));
            sequence.Join(_winningsTMP.transform.DOScale(Vector3.one, 1));
            sequence.Append(AnimateCredits(_winningsTMP.transform));

            sequence.Append(DOVirtual.Float(_winningsTMP.alpha, 0, 2f, newValue => _winningsTMP.alpha = newValue));
            sequence.Join(_creditsTransform.DOScale(Vector3.zero, 1f));

            return sequence;
        }

        private string ToMoneyFormat(float money)
        {
            return String.Format("{0:C}", money);
        }

    }
}