using System;
using UnityEngine;

namespace Score
{
    public class CoinCollector : MonoBehaviour
    {
        private int _totalAmount;
        
        public event Action<int> CoinChanged;

        private void Awake()
        {
            _totalAmount = 0;
            CoinChanged?.Invoke(_totalAmount);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Coin"))
            {
                int amount = other.GetComponent<Coin>().Value;
                Debug.LogFormat($"AMOUNT: {amount}");
                _totalAmount += amount;

                CoinChanged?.Invoke(_totalAmount);
                Destroy(other.gameObject);
            }
        }
    }
}