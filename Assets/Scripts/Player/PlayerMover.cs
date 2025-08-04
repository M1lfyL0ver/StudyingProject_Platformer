using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _playerMovementSpeed = 5f;

    public Action<Vector2> directionChanged;

    private PlayerInputHandler _playerInputHandler;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _moveVelocity = Vector2.zero;


    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (direction.y > 0 && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(_boxCollider.size.x, 0.15f);
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (_boxCollider.size.y / 2 + 0.1f);

        return Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, 0.1f);
    }

    private void ChangeMovementSpeed(Vector2 direction)
    {
        directionChanged?.Invoke(direction);
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