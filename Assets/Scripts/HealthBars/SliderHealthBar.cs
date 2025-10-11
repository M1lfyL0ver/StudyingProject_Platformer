using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBar))]
public class SliderHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private HealthBar _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<HealthBar>();
    }

    private void OnEnable()
    {
        _healthBar.UpdateBar += UpdateBar;
    }

    private void OnDisable()
    {
        _healthBar.UpdateBar -= UpdateBar;
    }

    private void UpdateBar(float currentHealth, float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = currentHealth;
    }
}