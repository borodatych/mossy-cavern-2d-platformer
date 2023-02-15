using DG.Tweening;
using UnityEngine;

namespace Anim
{
    public class WaterPoolAnim : MonoBehaviour, IAnim
    {
        [SerializeField] private float _delta = 10f;
        [SerializeField] private float _duration = 10f;

        private Sequence _sequence;

        public void Play()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DOMoveY(transform.position.y - _delta, _duration));
            
            _sequence.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}