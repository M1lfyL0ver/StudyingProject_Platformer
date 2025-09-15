using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    public Action<Coin, Collision2D> OnCollectorCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollectorCollided?.Invoke(this, collision);
    }
}