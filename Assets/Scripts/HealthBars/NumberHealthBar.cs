using TMPro;
using UnityEngine;

public class NumberHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private MonoBehaviour _healthComponent;
    
    private IHealth _health;

    private void Awake()
    {
        if(_healthComponent is IHealth)
        {
            _health = _healthComponent.GetComponent<IHealth>();
            Initialize();
        }
        else
        {
            _health = null;
            Debug.Log($"{_healthComponent.GetType().Name} does not implement IHealth");
        }
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateText;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateText;
    }

    private void Initialize()
    {
        UpdateText(_health.CurrentHealth, _health.MaxHealth);
    }

    private void UpdateText(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth}/{maxHealth}";
    }
}