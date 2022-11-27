using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _timeToRevert;

        private Rigidbody2D _rb;
        private Animator _anim;
        private Vector2 _scale;

        private const float IdleState = 0;
        private const float WalkState = 1;

        private float _currentState;
        private float _currentTimeToRevert;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _scale = transform.localScale;

            _currentTimeToRevert = 0;
            _currentState = WalkState;
            Movement();
        }

        private void Update()
        {
            CheckState();
        }

        private void CheckState()
        {
            switch (_currentState)
            {
                case IdleState:
                    _currentTimeToRevert += Time.deltaTime;
                    if (_currentTimeToRevert >= _timeToRevert)
                    {
                        TurnAxis();
                        Movement();

                        _currentTimeToRevert = 0;
                        _currentState = WalkState;
                    }
                    break;
                case WalkState:
                    Movement();
                    break;
            }
        }

        private void Movement()
        {
            Vector2 _velocity = Vector2.right;
            if (_scale.x == -1)
            {
                _velocity = Vector2.left;
            }

            _rb.velocity = _velocity * _speed;
            _anim.SetFloat("Velocity", _rb.velocity.magnitude);
        }
        private void StopMoving()
        {
            _rb.velocity = Vector2.zero;
            _anim.SetFloat("Velocity", _rb.velocity.magnitude);
        }
        private void TurnAxis()
        {
            _scale.x *= -1;
            transform.localScale = _scale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyStopper"))
            {
                _currentState = IdleState;
                StopMoving();
            }
        }
    }
}