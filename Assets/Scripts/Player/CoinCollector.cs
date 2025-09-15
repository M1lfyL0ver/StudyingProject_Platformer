using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinCollector : MonoBehaviour
{
    public Action CoinPickedUp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            CoinPickedUp?.Invoke();
            coin.Collect();
        }
    }
}