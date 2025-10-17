using System;
using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHitpoints = 100;
    [SerializeField] private int _minHitpoints = 0;

    public event Action Dead;

    public event Action<float, float> HitpointsChanged;

    public float CurrentHitpoints { get; private set; }

    public float MaxHitpoints => _maxHitpoints;

    public float MinHitpoints => _minHitpoints;

    private void OnEnable()
    {
        CurrentHitpoints = _maxHitpoints;
        HitpointsChanged?.Invoke(CurrentHitpoints, _maxHitpoints);
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            CurrentHitpoints = Mathf.Max(_minHitpoints, CurrentHitpoints - damage);
            HitpointsChanged?.Invoke(CurrentHitpoints, _maxHitpoints);
            HandleDeath();
        }
    }

    public void HealHitpoints(int heal)
    {
        if (heal > 0)
        {
            CurrentHitpoints = Mathf.Min(_maxHitpoints, CurrentHitpoints + heal);
            HitpointsChanged?.Invoke(CurrentHitpoints, _maxHitpoints);
        }
    }

    private void HandleDeath()
    {
        if (CurrentHitpoints == _minHitpoints)
        {
            Dead?.Invoke();
        }
    }
}