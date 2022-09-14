using UnityEngine;

namespace Die
{
    public class SpawnAfterDie : MonoBehaviour, IDie
    {
        [SerializeField] private GameObject _respawn;

        public GameObject Respawn
        {
            // get => _respawn;
            set => _respawn = value;
        }

        public void AfterDeath()
        {
            transform.position = _respawn.transform.position;
        }
    }
}