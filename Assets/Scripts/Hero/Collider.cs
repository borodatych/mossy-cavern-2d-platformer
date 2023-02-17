using UnityEngine;

namespace Hero
{
    public class Collider: MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        
        public GameObject Root {
            get => _root;
            set => _root = value;
        }
    }
}