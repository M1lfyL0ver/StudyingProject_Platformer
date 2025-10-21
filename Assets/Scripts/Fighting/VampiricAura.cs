using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VampiricAuraVisualizer))]
public class VampiricAura : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _duration = 4f;
    [SerializeField] private float _cooldown = 6f;
    [SerializeField] private float _period = 0.5f;

    private Health _player;

    private bool _isActive = false;
    private bool _isOnCooldown = false;

    public event Action<float> AuraDurationChanged;
    public event Action<float> AuraCooldownChanged;
    public event Action<bool, float> AuraStatusSwitched;

    private void Awake()
    {
        _player = GetComponent<Health>();
        AuraStatusSwitched?.Invoke(false, _radius);
    }

    public void TrySetActive()
    {
        if (_isActive == false && _isOnCooldown == false)
        {
            _isActive = true;
            AuraStatusSwitched?.Invoke(true, _radius);
            StartCoroutine(VampiricCoroutine());
            AuraDurationChanged?.Invoke(_duration);
        }
    }

    private IEnumerator VampiricCoroutine()
    {
        float timer = 0f;
        WaitForSeconds period = new WaitForSeconds(_period);

        while (timer < _duration)
        {
            timer += _period;
            ApplyVampirism();
            yield return period;
        }

        AuraStatusSwitched?.Invoke(false, _radius);
        _isActive = false;
        StartCoroutine(CooldownCoroutine());
        AuraCooldownChanged?.Invoke(_cooldown);
    }

    private IEnumerator CooldownCoroutine()
    {
        _isOnCooldown = true;

        yield return new WaitForSeconds(_cooldown);

        _isOnCooldown = false;
    }

    private void ApplyVampirism()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, _radius);

        Health nearestEnemy = FindNearestEnemy(hitColliders);

        if (nearestEnemy != null)
        {
            nearestEnemy.TakeDamage(_damage);
            _player.HealHitpoints(_damage);
        }
    }

    private Health FindNearestEnemy(Collider2D[] colliders)
    {
        Health nearestEnemy = null;
        Health enemyHealth = null;
        float closestSqrDistance = float.MaxValue;
        float radiusSquared = _radius * _radius;

        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out _) && collider.gameObject.TryGetComponent<Health>(out enemyHealth))
            {
                if (enemyHealth != null)
                {
                    Vector2 directionToEnemy = collider.transform.position - transform.position;
                    float sqrDistance = directionToEnemy.sqrMagnitude;

                    if (sqrDistance <= radiusSquared && sqrDistance < closestSqrDistance)
                    {
                        closestSqrDistance = sqrDistance;
                        nearestEnemy = enemyHealth;
                    }
                }
            }
        }

        return nearestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}