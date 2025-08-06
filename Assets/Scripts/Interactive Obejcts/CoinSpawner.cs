using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _timeDelaySpawn = 2f;

    private Coroutine _spawnCoroutine;

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

    private IEnumerator SpawnCoin()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_timeDelaySpawn);

            Instantiate(_coinPrefab, transform.position, transform.rotation);
        }
    }
}