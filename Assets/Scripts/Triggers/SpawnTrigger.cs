using Die;
using UnityEngine;

namespace Triggers
{
    public class SpawnTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _ps;
        [SerializeField] private bool _isActive;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!_isActive)
                {
                    other.GetComponent<SpawnAfterDie>().Respawn = gameObject;
                    _ps.Play();

                    _isActive = true;
                }
            }
        }
    }
}