using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _healthComponent;

    public event Action<float, float> UpdateBar;

    private IHealth _health;

    private void Awake()
    {
        if (_healthComponent is IHealth)
        {
            _health = _healthComponent.GetComponent<IHealth>();
        }
        else
        {
            _health = null;
            Debug.Log($"{_healthComponent.GetType().Name} does not implement IHealth");
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        _health.HealthChanged += HandleUpdateBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= HandleUpdateBar;
    }

    private void Initialize()
    {
        HandleUpdateBar(_health.CurrentHealth, _health.MaxHealth);
    }
    
    private void HandleUpdateBar(float currentHealth, float maxHealth)
    {
        UpdateBar?.Invoke(currentHealth, maxHealth);
    }
}