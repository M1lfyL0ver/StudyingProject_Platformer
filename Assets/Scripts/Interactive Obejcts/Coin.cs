using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour 
{
    [SerializeField] private float _spawnForce = 2f;

    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f) * _spawnForce, _spawnForce);

        _rigidbody.AddForce(direction, ForceMode2D.Impulse);
    }
}