using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    public Action<Coin> OnCollectorCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CoinCollector>(out _))
        {
            OnCollectorCollided?.Invoke(this);
        }
    }
}