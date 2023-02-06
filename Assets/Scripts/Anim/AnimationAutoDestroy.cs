using UnityEngine;

namespace Anim
{
    [RequireComponent(typeof(Animator))]
    public class AnimationAutoDestroy : MonoBehaviour
    {
        [SerializeField] private float _delay = 0f;
 
        private void Start ()
        {
            float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            Destroy (gameObject, animTime + _delay); 
        }
    }
}