using Bullet;
using Hero;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private GameObject _body;
        [SerializeField] private GameObject _skin;
        [SerializeField] private GameObject _bulletSpawn;

        [Header("Movement Vars")]
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _timeToRevert = 3f;

        private Rigidbody2D _rb;
        private Animator _anim;
        private HeroShooter _heroShooter;
        private Vector2 _scale, _position;
        
        private Vector2 _bulletSpawnScale, _bulletSpawnPosition;
        private BulletSpawn _bulletSpawnScript;

        public const int IdleState = 0, WalkState = 1, StopState = 2, ActionState = 3, RevertState = 4;

        private int _currentState;
        private float _currentTimeToRevert;
        private bool _isShooterNotNull;

        public int CurrentState {
            get => _currentState;
            set => _currentState = value;
        }

        private void Awake()
        {
            _rb = _body.GetComponent<Rigidbody2D>();
            _anim = _skin.GetComponent<Animator>();
            _heroShooter = GetComponent<HeroShooter>();
            
            _scale = _body.transform.localScale;
            _position = _body.transform.position;

            _bulletSpawnScript = _bulletSpawn.GetComponent<BulletSpawn>();
        }
        private void Start()
        {
            _isShooterNotNull = _heroShooter != null;
            _currentTimeToRevert = 0;
            _currentState = WalkState;
            Movement();
        }

        private void Update()
        {
            CheckState();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CheckState()
        {
            switch (CurrentState)
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
                _heroShooter.Shoot();
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
            _body.transform.localScale = _scale;
            _position.x *= -1;

            _bulletSpawnScript.MovingForward = _scale.x > 0;
        }
    }
}