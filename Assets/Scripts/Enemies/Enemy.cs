using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(EnemyPathFinder))]
public class Enemy : MonoBehaviour
{
    private EnemyPathFinder _pathFinder;
    private EnemyMover _mover;

    private void Awake()
    {
        _pathFinder = GetComponent<EnemyPathFinder>();
        _mover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _pathFinder.WaypointReached += _mover.SetNextWaypoint;
    }

    private void OnDisable()
    {
        _pathFinder.WaypointReached -= _mover.SetNextWaypoint;
    }
}