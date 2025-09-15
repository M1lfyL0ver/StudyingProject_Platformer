using System;
using UnityEngine;

public class EnemyPathFinder : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _reachThreshold = 0.1f;

    private EnemyMover _mover;
    private Vector2 _targetPosition;
    private int _currentWaypointIndex = 0;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void Start()
    {
        _mover.SetNextWaypoint(SetNextWaypointDirection());
    }

    private void FixedUpdate()
    {
        ChangeWaypointIfReached();
    }

    public Vector2 GetNextWaypointDirection()
    {
        return SetNextWaypointDirection();
    }

    private Vector2 SetNextWaypointDirection()
    {
        if (_waypoints.Length == 0)
        {
            return Vector2.zero;
        }

        _targetPosition = _waypoints[_currentWaypointIndex].position;

        Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;

        return direction;
    }

    private void ChangeWaypointIfReached()
    {
        if (IsCloseEnough(transform.position, _targetPosition, _reachThreshold))
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
            _mover.SetNextWaypoint(SetNextWaypointDirection());
        }
    }

    private bool IsCloseEnough(Vector2 start, Vector2 end, float threshold)
    {
        return Vector2.SqrMagnitude(start - end) <= threshold;
    }
}