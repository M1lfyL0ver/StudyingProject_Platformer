using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _reachThreshold = 0.1f;

    private Rigidbody2D _rigidbody;
    private int _currentWaypointIndex = 0;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PatrolBetweenWaypoints();
    }

    private void PatrolBetweenWaypoints()
    {
        if (_waypoints.Length == 0)
        {
            return;
        }

        Vector2 targetPosition = _waypoints[_currentWaypointIndex].position;
        Vector2 direction = (targetPosition - _rigidbody.position).normalized;
        Vector2 movement = direction * _moveSpeed * Time.fixedDeltaTime;

        _rigidbody.linearVelocity = new Vector2(direction.x * _moveSpeed, 0);

        if (IsCloseEnough(transform.position, targetPosition, _reachThreshold))
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }

        if (direction.x > 0 && !_isFacingRight || direction.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsCloseEnough(Vector2 start, Vector2 end, float threshold)
    {
        return Vector2.SqrMagnitude(start - end) <= threshold;
    }
}