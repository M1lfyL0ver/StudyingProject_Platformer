using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyPathFinder))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;

    private Rigidbody2D _rigidbody;
    private EnemyPathFinder _enemyPathFinder;
    private Fliper _fliper;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyPathFinder = GetComponent<EnemyPathFinder>();
        _fliper = gameObject.AddComponent<Fliper>();
    }

    private void FixedUpdate()
    {
        Move();
        _enemyPathFinder.ChangeWaypointIfReached();
    }

    private void Move()
    {
        Vector2 direction = _enemyPathFinder.GetNextWaypointDirection();
        Vector2 movement = direction * _moveSpeed * Time.fixedDeltaTime;

        _rigidbody.linearVelocity = new Vector2(direction.x * _moveSpeed, 0);

        _fliper.Flip(direction);
    }
}