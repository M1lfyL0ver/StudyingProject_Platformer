using TMPro;
using UnityEngine;

public class NumberHealthBar : HealthBar
{
    [SerializeField] private TextMeshProUGUI _text;

    public override void HandleUpdateBar(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth}/{maxHealth}";
    }
}