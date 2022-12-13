using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _speed;

    // ReSharper disable Unity.PerformanceAnalysis
    public void Shoot()
    {
        GameObject currentBullet = Instantiate(_bullet, _spawn.position, Quaternion.identity);
        Rigidbody2D rbCurrentBullet = currentBullet.GetComponent<Rigidbody2D>();

        if (rbCurrentBullet != null)
        {
            int dir = _spawn.localScale.x < 0 ? -1 : 1;
            rbCurrentBullet.velocity = new Vector2(_speed * dir, rbCurrentBullet.velocity.y);
        }
    }
}
