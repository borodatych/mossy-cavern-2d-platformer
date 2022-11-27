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

        private const float IDLE_STATE = 0;
        private const float WALK_STATE = 1;
        private const float REVERT_STATE = 2;

        [Header("-= Debug Info =-")]
        [SerializeField] private float _currentState;
        [SerializeField] private float _currentTimeToRevert;
        [SerializeField] private float _magnitude;

        private void Start()
        {
            _currentState = WALK_STATE;
            _currentTimeToRevert = 0;

            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _scale = transform.localScale;
        }

        private void Update()
        {
            switch (_currentState)
            {
                case IDLE_STATE:
                    _currentTimeToRevert += Time.deltaTime;

                    if (_currentTimeToRevert >= _timeToRevert)
                    {
                        _currentTimeToRevert = 0;
                        _currentState = REVERT_STATE;
                    }

                    _magnitude = 0f;

                    break;
                case WALK_STATE:
                    Vector2 _velocity = Vector2.right;
                    if (_scale.x == -1)
                    {
                        _velocity = Vector2.left;
                    }

                    _rb.velocity = _velocity * _speed;
                    _magnitude = _rb.velocity.magnitude;

                    break;
                case REVERT_STATE:
                    _scale.x *= -1;
                    transform.localScale = _scale;

                    _currentState = WALK_STATE;
                    _magnitude = 0f;

                    break;
            }

            _anim.SetFloat("Velocity", _magnitude);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyStopper"))
            {
                _currentState = IDLE_STATE;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("EnemyStopper"))
            {
                _currentState = WALK_STATE;
            }
        }        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("EnemyStopper"))
            {
                // _currentState = IDLE_STATE;
            }
        }
    }
}