using Anim;
using UnityEngine;

namespace Die
{
    public class AnimAfterDie : MonoBehaviour, IDie
    {
        [SerializeField] private GameObject[] _animObjects;
        public void AfterDeath()
        {
            foreach(var objet in _animObjects)
            {
                var anims = objet.GetComponents(typeof(IAnim));

                foreach (var component in anims)
                {
                    var anim = (IAnim)component;
                    anim.Play();
                }
            }
        }
    }
}
