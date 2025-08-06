using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _playerMovementSpeed = 5f;

    private Rigidbody2D _rigidbody;
    private Fliper _fliper;
    private Vector2 _moveVelocity = Vector2.zero;

    public event Action<Vector2> DirectionChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _fliper = gameObject.AddComponent<Fliper>();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_moveVelocity.x, _rigidbody.linearVelocity.y);
    }

    public void Jump(Vector2 direction, bool isGrounded)
    {
        if (direction.y > 0 && isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    public void ChangeMovementSpeed(Vector2 direction)
    {
        _moveVelocity = direction.normalized * _playerMovementSpeed;

        _fliper.Flip(direction);
    }
}