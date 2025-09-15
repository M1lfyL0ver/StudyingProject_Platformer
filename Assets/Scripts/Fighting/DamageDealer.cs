using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageInterval = 1f;

    private float _lastDamageTime = 0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent<Health>(out Health healthChanger))
        {
            TryDealDamage(healthChanger);
        }
    }

    private void TryDealDamage(Health target)
    {
        if (Time.time - _lastDamageTime >= _damageInterval)
        {
            target.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }
}