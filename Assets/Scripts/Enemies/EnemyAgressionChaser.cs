using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMover))]
public class EnemyAgressionChaser : MonoBehaviour
{
    [SerializeField] private float _chaseRange = 5f;
    [SerializeField] private float _stopChaseRange = 7f;

    private Transform _currentTarget;
    private EnemyMover _mover;
    private bool _isChasing = false;

    public event Action<bool> ChaseStateChanged;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        FindTarget();
        HandleChaseState();
        HandleChaseMovement();
    }

    private void FindTarget()
    {
        if (_currentTarget != null)
        {
            float distance = Vector2.Distance(transform.position, _currentTarget.position);

            if (distance > _stopChaseRange)
            {
                _currentTarget = null;
            }

            return;
        }

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _chaseRange);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<Player>(out _))
            {
                _currentTarget = target.transform;
                break;
            }
        }
    }

    private void HandleChaseState()
    {
        bool shouldChase = _currentTarget != null;

        if (shouldChase && _isChasing == false)
        {
            _isChasing = true;
            ChaseStateChanged?.Invoke(true);
        }
        else if (shouldChase == false && _isChasing)
        {
            _isChasing = false;
            ChaseStateChanged?.Invoke(false);
        }
    }

    private void HandleChaseMovement()
    {
        if (_isChasing && _currentTarget != null)
        {
            Vector2 direction = (_currentTarget.position - transform.position).normalized;
            _mover.SetNextWaypoint(direction);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stopChaseRange);

        if (_isChasing && _currentTarget != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _currentTarget.position);
        }
    }
}