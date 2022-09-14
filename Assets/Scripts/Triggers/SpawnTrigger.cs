using System;
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
            // Debug.LogFormat($"Name: {other.name} : Tag: {other.tag}");
            if (other.CompareTag("Player"))
            {
                // Debug.LogFormat($"IsActive: {_isActive}");
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