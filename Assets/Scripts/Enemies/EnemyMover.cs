using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyPathFinder))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;

    private Rigidbody2D _rigidbody;
    private Fliper _fliper;
    private Vector2 _nextWaypoint = Vector2.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _fliper = gameObject.AddComponent<Fliper>();
    }

    private void FixedUpdate()
    {
        Move(_nextWaypoint);
    }

    public void SetNextWaypoint(Vector2 waypointDirection)
    {
        if(waypointDirection != null)
        {
            _nextWaypoint = waypointDirection;
        }
    }

    private void Move(Vector2 nextWaypointDirection)
    {
        _rigidbody.linearVelocity = new Vector2(nextWaypointDirection.x * _moveSpeed, 0);

        _fliper.Flip(nextWaypointDirection);
    }
}