using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private GameObject _body;
        [SerializeField] private GameObject _skin;

        [Header("Movement Vars")]
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _timeToRevert = 3f;
        [SerializeField] private float _jumpForce = 5;
        [SerializeField] private float _jumpOffset = 0.15f;

        private GameObject _collider;
        private Rigidbody2D _rb;
        private Animator _anim;
        private Vector2 _scale;

        public const float IdleState = 0;
        public const float WalkState = 1;
        public const float StopState = 2;

        private static float _currentState;
        private float _currentTimeToRevert;
        
        public static float CurrentState {
            get => _currentState;
            set => _currentState = value;
            
        }

        private void Awake()
        {
            _rb = _body.GetComponent<Rigidbody2D>();
            _anim = _skin.GetComponent<Animator>();
            _scale = _skin.transform.localScale;
            _collider = GameObject.Find("Body/Collider");
        }
        private void Start()
        {
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
                        FlipAxis();
                        Movement();
                        _currentTimeToRevert = 0;
                        _currentState = WalkState;
                    }
                    break;
                case WalkState:
                    // Movement();
                    break;
                case StopState:
                    Stopping();
                    _currentState = IdleState;
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
            _anim.CrossFade("walk", 0f, 0, 1f);
        }
        private void Stopping()
        {
            _rb.velocity = Vector2.zero;
            _anim.SetFloat("Velocity", _rb.velocity.magnitude);
            _anim.CrossFade("idle", 0f, 0, 1f);
        }
        private void FlipAxis()
        {
            _scale.x *= -1;
            _skin.transform.localScale = _scale;
            _collider.transform.localScale = _scale;
        }
    }
}