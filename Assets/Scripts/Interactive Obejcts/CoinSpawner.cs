using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _timeDelaySpawn = 2f;
    [SerializeField] private int _initialPoolSize = 10;

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
            coin.CoinOnCollectorCollided += ReturnCoinToPool;
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
            coin.CoinOnCollectorCollided += ReturnCoinToPool;
        }

        coin.transform.position = transform.position;
        coin.transform.rotation = transform.rotation;
        coin.gameObject.SetActive(true);
    }

    private void ReturnCoinToPool(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }
}