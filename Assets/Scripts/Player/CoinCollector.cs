using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinCollector : MonoBehaviour
{
    private int _coinsCount;

    public Action<int> CoinsCountChanged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out _))
        {
            Destroy(collision.gameObject);
            _coinsCount++;
            CoinsCountChanged?.Invoke(_coinsCount);
        }
    }
}