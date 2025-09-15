using System;
using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHitpoints = 100;
    [SerializeField] private int _minHitpoints = 0;

    private int _hitpoints;

    public Action DamageDealed;
    public Action PlayerIsDead;

    private void OnEnable()
    {
        _hitpoints = _maxHitpoints;
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _hitpoints = Mathf.Max(_minHitpoints, _hitpoints - damage);
            DamageDealed?.Invoke();
            HandleDeath();
        }
    }

    public void HealHitpoints(int heal)
    {
        if (heal > 0)
        {
            _hitpoints = Mathf.Min(_maxHitpoints, _hitpoints + heal);
        }
    }

    private void HandleDeath()
    {
        if (_hitpoints == _minHitpoints)
        {
            PlayerIsDead?.Invoke();
        }
    }
}
