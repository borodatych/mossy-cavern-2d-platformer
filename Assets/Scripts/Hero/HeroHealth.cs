using System;
using System.Collections;
using System.Collections.Generic;
using Die;
using UnityEngine;

namespace Hero
{
    public class HeroHealth : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private bool _nonRemovable;

        private bool _isAlive;
        private Component[] _dieObjs;

        public event Action<float> HealthChanged; 

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _isAlive = true;

            _dieObjs = GetComponents(typeof(IDie));

            if (_root == null)
            {
                _root = gameObject;
            }
        }

        public void TakeDamage(float damage, bool instant)
        {
            if (instant)
            {
                _currentHealth = -1;
            }
            else
            {
                _currentHealth -= damage;
            }

            Flash();
            HealthEvent();
            CheckIsAlive();
        }

        private void CheckIsAlive()
        {
            _isAlive = _currentHealth > 0;
            if (!_isAlive)
            {
                Die();
            }
        }
        private void HealthEvent()
        {
            float currentHealthAsPercentage = _currentHealth / _maxHealth;
            HealthChanged?.Invoke(currentHealthAsPercentage);
        }

        private void Die()
        {
            foreach (var component in _dieObjs)
            {
                var die = (IDie)component;
                die.AfterDeath();
            }

            if (!_nonRemovable)
            {
                Destroy(_root.gameObject);
            }
            else
            {
                _currentHealth = _maxHealth;
            }
        }

        private static IEnumerator FlashSprites(IReadOnlyList<SpriteRenderer> sprites, int numTimes, float delay, bool disable = false)
        {
            for (int loop = 0; loop < numTimes; loop++)
            {            
                for (int i = 0; i < sprites.Count; i++)
                {                
                    if (disable)
                    {
                        sprites[i].enabled = false;
                    }
                    else
                    {
                        sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0.5f);
                    }
                }
 
                yield return new WaitForSeconds(delay);
 
                for (int i = 0; i < sprites.Count; i++)
                {
                    if (disable)
                    {
                        sprites[i].enabled = true;
                    }
                    else
                    {
                        sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 1);
                    }
                }
 
                yield return new WaitForSeconds(delay);
            }
        }
        private void Flash()
        {
            if (_root)
            {
                SpriteRenderer[] sprites = _root.GetComponentsInChildren<SpriteRenderer>();
                StartCoroutine(FlashSprites(sprites, 5, 0.05f));
            }
        }
    }
}
