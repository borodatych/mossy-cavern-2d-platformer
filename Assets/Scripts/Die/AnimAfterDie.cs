using Anim;
using UnityEngine;

namespace Die
{
    public class AnimAfterDie : MonoBehaviour, IDie
    {
        [SerializeField] private GameObject[] _animObjects;
        public void AfterDeath()
        {
            foreach(GameObject objet in _animObjects)
            {
                // objet.GetComponent<IAnim>()?.Play();
                Component[] anims = objet.GetComponents(typeof(IAnim));

                foreach (IAnim anim in anims)
                {
                    anim.Play();
                }
            }
        }
    }
}
