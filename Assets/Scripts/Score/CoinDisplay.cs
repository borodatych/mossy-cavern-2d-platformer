using TMPro;
using UnityEngine;

namespace Score
{
    public class CoinDisplay : MonoBehaviour
    {
        [SerializeField] private CoinCollector _collector;
        [SerializeField] private TextMeshProUGUI _text;
        
        private void Awake()
        {
            _collector.CoinChanged += OnCoinChange;
        }

        private void OnDestroy()
        {
            _collector.CoinChanged -= OnCoinChange;
        }

        private void OnCoinChange(int value)
        {
            _text.SetText($"{value}");
        }
    }
}