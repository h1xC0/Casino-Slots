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
        
        [SerializeField] private Button _reduceButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private TMP_Text _betTMP;

        public override void Initialize()
        {
            _reduceButton.onClick.AddListener(OnDecrease);
            _increaseButton.onClick.AddListener(OnIncrease);

            _reduceButton.onClick.AddListener(OnDisplay);
            _increaseButton.onClick.AddListener(OnDisplay);
        }

        public void OnDisplay(long credits, long bet, int betSize)
        {
            _betTMP.text = ToMoneyFormat(bet);
            
            
            
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

        // private void IncreaseAmount()
        // {
        //     SetAmount(BetSize);
        //     _reduceButton.interactable = true;
        //
        //     if (_creditsAmount < _betAmount + BetSize)
        //         _increaseButton.interactable = false;
        // }
        //
        // private void DecreaseAmount()
        // {
        //     SetAmount(-BetSize);
        //     
        //     if (_creditsAmount > _betAmount)
        //         _increaseButton.interactable = true;
        //
        //     if (_betAmount - BetSize == 0)
        //         _reduceButton.interactable = false;
        // }

        // private void SetAmount(float value)
        // {
        //     _betAmount += value;
        //     _betTMP.text = ToMoneyFormat(_betAmount);
        // }

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