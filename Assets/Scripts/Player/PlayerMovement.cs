using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Vars")]
        [SerializeField]
        private float _speed = 3;

        [SerializeField] private float _jumpForce = 5;

        [Header("Settings")]
        [SerializeField] private float _jumpOffset = 0.15f;
        [SerializeField] private AnimationCurve _animCurve;
        [SerializeField] private GameObject _groundCollider;
        [SerializeField] private GameObject _controlCollider;
        [SerializeField] private LayerMask _groundLayerMask;

        private Rigidbody2D _rb;
        private Animator _anim;
        private bool _isGrounded;
        private float _groundColliderRadius;
        private float _collisionRadius;

        private bool _isBackward;

        private void Awake()
        {
            _isGrounded = true;
            _isBackward = false;

            _anim = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _groundColliderRadius = _groundCollider.GetComponent<CircleCollider2D>().radius;
            _collisionRadius = _groundColliderRadius + _jumpOffset;
        }

        private void Start()
        {
            CheckGrounded();
        }

        private void FixedUpdate()
        {
            CheckGrounded();

            _anim.SetBool("isJump", !_isGrounded);
        }

        private void CheckGrounded()
        {
            Vector2 posGround = _groundCollider.transform.position;
            bool overGrounded = Physics2D.OverlapCircle(posGround, _collisionRadius, _groundLayerMask); // На земле с включенным запасом - _jumpOffset
            bool onGrounded = Physics2D.OverlapCircle(posGround, _groundColliderRadius, _groundLayerMask); // Строго на земле, что бы в углах не включалась анимация

            Vector2 posControl = _controlCollider.transform.position;
            bool overControl = Physics2D.OverlapCircle(posControl, _collisionRadius, _groundLayerMask); // Контрольная точка касается того же словя, скорее всего вертикальная стена

            _isGrounded = onGrounded || (overGrounded && !overControl);

            //Debug.LogFormat($"Is Grounded: {_isGrounded} | {_collisionRadius}");
        }

        public void Move(float direction, bool isJump)
        {
            _isBackward = direction < 0;

            if (isJump)
            {
                Jump();
            }

            if (Mathf.Abs(direction) > 0.01f)
            {
                FlipPosition();
                Movement(direction);
            }
        }

        private void Jump()
        {
            if (_isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }
        }

        private void Movement(float direction)
        {
            _rb.velocity = new Vector2(_animCurve.Evaluate(direction), _rb.velocity.y);
        }

        private void FlipPosition()
        {
            Vector3 localScale = transform.localScale;

            if (
                (!_isBackward && localScale.x < 0)
                || (_isBackward && localScale.x > 0)
            )
            {
                localScale.x *= -1;
            }

            transform.localScale = localScale;
        }
    }
}
