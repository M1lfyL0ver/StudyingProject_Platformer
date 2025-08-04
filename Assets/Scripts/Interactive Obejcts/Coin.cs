using UnityEngine;

public class Coin : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMover>(out _))
        {
            Destroy(gameObject);
        }
    }
}