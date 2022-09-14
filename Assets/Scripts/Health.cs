using System.Collections;
using Die;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _nonRemovable;

    private bool _isAlive;
    private Component[] _dieObjs;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _isAlive = true;

        _dieObjs = GetComponents(typeof(IDie));
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

    private void Die()
    {
        foreach (IDie die in _dieObjs)
        {
            die.AfterDeath();
        }

        if (!_nonRemovable)
        {
            Destroy(gameObject);
        }
        else
        {
            _currentHealth = _maxHealth;
        }
    }

    /**
     * Coroutine to create a flash effect on all sprite renderers passed in to the function.
     *
     * @param sprites   a sprite renderer array
     * @param numTimes  how many times to flash
     * @param delay     how long in between each flash
     * @param disable   if you want to disable the renderer instead of change alpha
     */
    IEnumerator FlashSprites(SpriteRenderer[] sprites, int numTimes, float delay, bool disable = false) {
        // number of times to loop
        for (int loop = 0; loop < numTimes; loop++) {            
            // cycle through all sprites
            for (int i = 0; i < sprites.Length; i++) {                
                if (disable) {
                    // for disabling
                    sprites[i].enabled = false;
                } else {
                    // for changing the alpha
                    sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0.5f);
                }
            }
 
            // delay specified amount
            yield return new WaitForSeconds(delay);
 
            // cycle through all sprites
            for (int i = 0; i < sprites.Length; i++) {
                if (disable) {
                    // for disabling
                    sprites[i].enabled = true;
                } else {
                    // for changing the alpha
                    sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 1);
                }
            }
 
            // delay specified amount
            yield return new WaitForSeconds(delay);
        }
    }
    private void Flash()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(FlashSprites(sprites, 5, 0.05f));
    }
}
