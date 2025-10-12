using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : HealthBar
{
    [SerializeField] private Slider _slider;

    public override void HandleUpdateBar(float currentHealth, float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = currentHealth;
    }
}