using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private GameObject _body;
        [SerializeField] private GameObject _skin;
        [SerializeField] private GameObject _action;

        [Header("Movement Vars")]
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _timeToRevert = 3f;
        [SerializeField] private float _jumpForce = 5;
        [SerializeField] private float _jumpOffset = 0.15f;

        private GameObject _collider;
        private Rigidbody2D _rb;
        private Animator _anim;
        private Shooter _shooter;
        private Vector2 _scale;

        public const float IdleState = 0;
        public const float WalkState = 1;
        public const float StopState = 2;
        public const float ActionState = 3;
        public const float RevertState = 4;

        private static float _currentState;
        private float _currentTimeToRevert;
        private bool _isShooterNotNull;

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
            _shooter = GetComponent<Shooter>();
        }
        private void Start()
        {
            _isShooterNotNull = _shooter != null;
            _currentTimeToRevert = 0;
            _currentState = WalkState;
            Movement();
        }

        private void Update()
        {
            CheckState();
        }
        private void FixedUpdate()
        {
            //CheckState();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CheckState()
        {
            switch (_currentState)
            {
                case IdleState:
                    _currentTimeToRevert += Time.deltaTime;
                    if (_currentTimeToRevert >= _timeToRevert)
                    {
                        _currentTimeToRevert = 0;
                        _currentState = ActionState;
                    }
                    break;
                case WalkState:
                    // Movement();
                    break;
                case StopState:
                    Stopping();
                    _currentState = IdleState;
                    break;
                case ActionState:
                    Action();
                    _currentState = RevertState;
                    break;
                case RevertState:
                    FlipAxis();
                    Movement();
                    _currentState = WalkState;
                    break;
            }
        }

        private void Action()
        {
            if (_isShooterNotNull)
            {
                _shooter.Shoot();
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
            _action.transform.localScale = _scale;
            _collider.transform.localScale = _scale;
        }
    }
}