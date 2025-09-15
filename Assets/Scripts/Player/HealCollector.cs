using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealCollector : MonoBehaviour
{
    public Action<int> HealPickedUp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<HealPotion>(out HealPotion potion))
        {
            HealPickedUp?.Invoke(potion.Heal);
        }
    }
}