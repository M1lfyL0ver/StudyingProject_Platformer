using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler), typeof(Rigidbody2D), typeof(BoxCollider2D))]
[RequireComponent(typeof(GroundDetector), typeof(PlayerAnimationSwitcher))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _playerMovementSpeed = 5f;

    private PlayerInputHandler _playerInputHandler;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private GroundDetector _groundDetector;
    private PlayerAnimationSwitcher _playerAnimationSwitcher;
    private Vector2 _moveVelocity = Vector2.zero;

    public event Action<Vector2> DirectionChanged;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _groundDetector = GetComponent<GroundDetector>();
        _playerAnimationSwitcher = GetComponent<PlayerAnimationSwitcher>();
    }

    private void OnEnable()
    {
        _playerInputHandler.LeftRightPressed += ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed += Jump;
    }

    private void OnDisable()
    {
        _playerInputHandler.LeftRightPressed -= ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed -= Jump;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveVelocity.x, _rigidbody.linearVelocity.y);
    }

    private void Jump(Vector2 direction)
    {
        if (direction.y > 0 && _groundDetector.IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ChangeMovementSpeed(Vector2 direction)
    {
        Quaternion right = Quaternion.Euler(0f, 0f, 0f);
        Quaternion left = Quaternion.Euler(0f, 180f, 0f);

        _playerAnimationSwitcher.SetRunAnimation(direction);
        _moveVelocity = direction.normalized * _playerMovementSpeed;

        if(direction.x < 0)
        {
            transform.rotation = left;
        }
        if (direction.x > 0)
        {
            transform.rotation = right;
        }
    }
}