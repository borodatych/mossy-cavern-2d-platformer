using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Animator))]
    public class AnimAutoDestroy : MonoBehaviour
    {
        [SerializeField] private float _delay = 0f;
 
        private void Start ()
        {
            float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            Destroy (gameObject, animTime + _delay); 
        }
    }
}