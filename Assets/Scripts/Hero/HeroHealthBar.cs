using UnityEngine;
using UnityEngine.UI;

namespace Hero
{
    public class HeroHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _filling;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private Transform _attached;
        [SerializeField] private float _offsetY = 0.35f;

        private void Awake()
        {
            _health.HealthChanged += OnHealthChange;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChange;
        }

        private void OnHealthChange(float value)
        {
            _filling.fillAmount = value;
        }

        private void LateUpdate()
        {
            Vector3 position = new Vector3(_attached.position.x, _attached.position.y + _offsetY, 0f);
            transform.position = Vector3.MoveTowards(transform.position, position, 10 * Time.deltaTime);
        }
    }
}