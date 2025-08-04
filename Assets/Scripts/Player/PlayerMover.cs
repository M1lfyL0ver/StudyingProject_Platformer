using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler), typeof(Rigidbody2D), typeof(BoxCollider2D))]
[RequireComponent(typeof(GroundDetector))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _playerMovementSpeed = 5f;

    private PlayerInputHandler _playerInputHandler;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private GroundDetector _groundDetector;
    private Vector2 _moveVelocity = Vector2.zero;

    public event Action<Vector2> DirectionChanged;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _groundDetector = GetComponent<GroundDetector>();
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
        DirectionChanged?.Invoke(direction);
        _moveVelocity = direction.normalized * _playerMovementSpeed;

        if(direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        if (direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}