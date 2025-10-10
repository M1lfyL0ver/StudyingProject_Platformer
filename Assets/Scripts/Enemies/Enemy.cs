using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(EnemyPathFinder), typeof(EnemyAgressionChaser))]
[RequireComponent(typeof(Health), typeof(DamageDealer))]
public class Enemy : MonoBehaviour
{
    private EnemyMover _mover;
    private EnemyPathFinder _pathFinder;
    private EnemyAgressionChaser _chaser;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _pathFinder = GetComponent<EnemyPathFinder>();
        _chaser = GetComponent<EnemyAgressionChaser>();
    }

    private void OnEnable()
    {
        _chaser.ChaseStateChanged += HandleChaseStateChanged;
    }

    private void OnDisable()
    {
        _chaser.ChaseStateChanged -= HandleChaseStateChanged;
    }

    private void HandleChaseStateChanged(bool isChasing)
    {
        if (isChasing)
        {
            _pathFinder.enabled = false;
        }
        else
        {
            _pathFinder.enabled = true;
            _mover.SetNextWaypoint(_pathFinder.GetNextWaypointDirection());
        }
    }   
}