using System;
using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHitpoints = 100;
    [SerializeField] private int _minHitpoints = 0;

    private int _hitpoints;

    public event Action PlayerIsDead;

    public event Action<float, float> HealthChanged;

    public float CurrentHealth => _hitpoints;

    public float MaxHealth => _maxHitpoints;

    public float MinHealth => _minHitpoints;

    private void OnEnable()
    {
        _hitpoints = _maxHitpoints;
        HealthChanged?.Invoke(_hitpoints, _maxHitpoints);
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _hitpoints = Mathf.Max(_minHitpoints, _hitpoints - damage);
            HealthChanged?.Invoke(_hitpoints, _maxHitpoints);
            HandleDeath();
        }
    }

    public void HealHitpoints(int heal)
    {
        if (heal > 0)
        {
            _hitpoints = Mathf.Min(_maxHitpoints, _hitpoints + heal);
            HealthChanged?.Invoke(_hitpoints, _maxHitpoints);
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