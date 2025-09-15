using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    public Action<Coin> OnCollectorCollided;

    public void Collect()
    {
        OnCollectorCollided?.Invoke(this);
    }
}