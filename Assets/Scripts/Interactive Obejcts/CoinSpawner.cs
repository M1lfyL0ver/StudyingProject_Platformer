using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _timeDelaySpawn = 2f;
    [SerializeField] private int _initialPoolSize = 10;
    [SerializeField] private float _spawnForce = 2f;

    private Queue<Coin> _coinPool = new Queue<Coin>();
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        InitializePool();
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoin());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            Coin coin = Instantiate(_coinPrefab, transform.position, transform.rotation);
            coin.gameObject.SetActive(false);
            coin.OnCollectorCollided += TryReturnCoinToPool;
            _coinPool.Enqueue(coin);
        }
    }

    private IEnumerator SpawnCoin()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeDelaySpawn);

        while (enabled)
        {
            yield return wait;
            SpawnCoinFromPool();
        }
    }

    private void SpawnCoinFromPool()
    {
        Coin coin;

        if (_coinPool.Count > 0)
        {
            coin = _coinPool.Dequeue();
        }
        else
        {
            coin = Instantiate(_coinPrefab, transform.position, transform.rotation);
            coin.OnCollectorCollided += TryReturnCoinToPool;
        }

        coin.transform.position = transform.position;
        coin.transform.rotation = transform.rotation;
        coin.gameObject.SetActive(true);

        Rigidbody2D rigidbody = coin.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.Range(-1f, 1f) * _spawnForce, _spawnForce);
        rigidbody.AddForce(direction, ForceMode2D.Impulse);
    }

    private void TryReturnCoinToPool(Coin coin, Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CoinCollector>(out _))
        {
            coin.gameObject.SetActive(false);
        }
    }
}