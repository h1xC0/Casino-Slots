using System;
using TMPro;
using UI.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Credits
{
    public class BetView : View, IBetView
    {
        public event Action IncreaseEvent;
        public event Action DecreaseEvent;
        public event Action MaxBetEvent;
        
        [SerializeField] private Button _reduceButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private Button _maxBetButton;
        [SerializeField] private TMP_Text _betTMP;

        public override void Initialize()
        {
            _reduceButton.onClick.AddListener(OnDecrease);
            _increaseButton.onClick.AddListener(OnIncrease);
            _maxBetButton.onClick.AddListener(OnMaxBet);
            
            _reduceButton.onClick.AddListener(OnDisplay);
            _increaseButton.onClick.AddListener(OnDisplay);
            _maxBetButton.onClick.AddListener(OnDisplay);
        }

        public void OnDisplay(long credits, long bet, int betSize)
        {
            _betTMP.text = ToMoneyFormat(bet);
            _maxBetButton.interactable = bet != credits;
            
            if(bet > betSize)
                _reduceButton.interactable = true;
            
            if (credits > bet)
                _increaseButton.interactable = true;

            if (bet - betSize == 0)
                _reduceButton.interactable = false;
            
            if (credits < bet + betSize)
                _increaseButton.interactable = false;
        }

        private void OnIncrease()
        {
            IncreaseEvent?.Invoke();
        }

        private void OnDecrease()
        {
            DecreaseEvent?.Invoke();
        }

        private void OnMaxBet()
        {
            MaxBetEvent?.Invoke();
        }

        public void SetInteraction(bool flag)
        {
            _reduceButton.gameObject.SetActive(flag);
            _increaseButton.gameObject.SetActive(flag);
        }

        
        private string ToMoneyFormat(float money)
        {
            return String.Format("{0:C}", money);
        }

        public override void Dispose()
        {
            _reduceButton.onClick.RemoveListener(OnDisplay);
            _increaseButton.onClick.RemoveListener(OnDisplay);
            
            base.Dispose();
        }
    }
}