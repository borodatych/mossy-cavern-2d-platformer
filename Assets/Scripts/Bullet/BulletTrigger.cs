using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Collider2D))]
    public class BulletTrigger : MonoBehaviour
    {
        [SerializeField] private bool _explosive = false;
        [SerializeField] private bool _waveDamage = false;
        private Animator _anim;
        private Collider2D _collider;
        private List<Collider2D> _ignoreColliders;
        
        public List<Collider2D> IgnoreColliders {
            get => _ignoreColliders;
            set => _ignoreColliders = value;
        }
        
        private void Awake()
        {
            DOTween.Init();
            _anim = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            _ignoreColliders = new List<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsChildCollider(collision) && !((IList) GlobalVars.BulletIgnoreTags).Contains(collision.tag))
            {
                if (_explosive)
                {
                    /*/ TODO: Подумать как через DOTWeen можно это провернуть
                    var sequence = DOTween.Sequence();
                    sequence.Append(transform.DOScale(new Vector2(1f, 1f), 0f));
                    sequence.InsertCallback(1, () => _anim.SetBool("detonate", true));
                    sequence.OnComplete(() => {
                        Destroy(gameObject);
                    });
                    //*/

                    //*/
                    if (_waveDamage) _collider.enabled = false; // Взрыв не триггерит === false
                    transform.DOScale(new Vector2(1f, 1f), 0.2f);
                    if (_anim) _anim.SetBool("detonate", true); // 0.4f
                    Destroy(gameObject, 0.5f);
                    //*/
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        
        private bool IsChildCollider(Collider2D collision)
        {
            foreach (var t in _ignoreColliders)
            {
                if (collision.CompareTag(t.tag)) return true;
            }

            return false;
        }
    }
}