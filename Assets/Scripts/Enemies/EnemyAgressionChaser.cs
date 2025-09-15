using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMover), typeof(EnemyPathFinder))]
public class EnemyAgressionChaser : MonoBehaviour
{
    [SerializeField] private float _chaseRange = 5f;
    [SerializeField] private float _stopChaseRange = 7f;

    private Player _player;
    private EnemyMover _mover;
    private bool _isChasing = false;

    public event Action<bool> ChaseStateChanged;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        FindPlayer();
    }

    private void Update()
    {
        if (_player == null) return;

        HandleChaseState();
        HandleChaseMovement();
    }

    private void OnDrawGizmosSelected()
    {
        DrawDetectionZones();
        DrawChaseLine();
    }

    private void HandleChaseState()
    {
        float distanceToPlayer = GetSqrDistanceToPlayer();
        bool shouldChase = ShouldChasePlayer(distanceToPlayer);

        if (shouldChase && (_isChasing == false))
        {
            StartChasing();
        }
        else if ((shouldChase == false) && _isChasing)
        {
            StopChasing();
        }
    }

    private void HandleChaseMovement()
    {
        if (_isChasing)
        {
            UpdateChaseDirection();
        }
    }

    private float GetSqrDistanceToPlayer()
    {
        return (_player.transform.position - transform.position).sqrMagnitude;
    }

    private bool ShouldChasePlayer(float distanceToPlayer)
    {
        if (_isChasing)
        {
            return distanceToPlayer <= _stopChaseRange * _stopChaseRange;
        }
        else
        {
            return distanceToPlayer <= _chaseRange * _chaseRange;
        }
    }

    private void FindPlayer()
    {
        _player = FindFirstObjectByType<Player>();
    }

    private void StartChasing()
    {
        _isChasing = true;
        ChaseStateChanged?.Invoke(true);
    }

    private void StopChasing()
    {
        _isChasing = false;
        ChaseStateChanged?.Invoke(false);
    }

    private void UpdateChaseDirection()
    {
        Vector2 chaseDirection = (_player.transform.position - transform.position).normalized;
        _mover.SetNextWaypoint(chaseDirection);
    }

    private void DrawDetectionZones()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stopChaseRange);
    }

    private void DrawChaseLine()
    {
        if (_isChasing && _player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _player.transform.position);
        }
    }
}