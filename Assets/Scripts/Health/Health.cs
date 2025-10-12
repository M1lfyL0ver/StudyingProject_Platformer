using System;
using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHitpoints = 100;
    [SerializeField] private int _minHitpoints = 0;

    private int _hitpoints;

    public event Action IsDead;

    public event Action<float, float> HitpointsChanged;

    public float CurrentHitpoints => _hitpoints;

    public float MaxHitpoints => _maxHitpoints;

    public float MinHitpoints => _minHitpoints;

    private void OnEnable()
    {
        _hitpoints = _maxHitpoints;
        HitpointsChanged?.Invoke(_hitpoints, _maxHitpoints);
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _hitpoints = Mathf.Max(_minHitpoints, _hitpoints - damage);
            HitpointsChanged?.Invoke(_hitpoints, _maxHitpoints);
            HandleDeath();
        }
    }

    public void HealHitpoints(int heal)
    {
        if (heal > 0)
        {
            _hitpoints = Mathf.Min(_maxHitpoints, _hitpoints + heal);
            HitpointsChanged?.Invoke(_hitpoints, _maxHitpoints);
        }
    }

    private void HandleDeath()
    {
        if (_hitpoints == _minHitpoints)
        {
            IsDead?.Invoke();
        }
    }
}