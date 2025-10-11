using TMPro;
using UnityEngine;

[RequireComponent(typeof(HealthBar))]
public class NumberHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private HealthBar _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<HealthBar>();
    }

    private void OnEnable()
    {
        _healthBar.UpdateBar += UpdateText;
    }

    private void OnDisable()
    {
        _healthBar.UpdateBar -= UpdateText;
    }

    private void UpdateText(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth}/{maxHealth}";
    }
}