using System;
using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        _health.HitpointsChanged += HandleUpdateBar;
    }

    private void OnDisable()
    {
        _health.HitpointsChanged -= HandleUpdateBar;
    }

    public abstract void HandleUpdateBar(float currentHealth, float maxHealth);

    private void Initialize()
    {
        HandleUpdateBar(_health.CurrentHitpoints, _health.MaxHitpoints);
    }
}