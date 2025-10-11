using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBar))]
public class SmoothSliderBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 2f;

    private HealthBar _healthBar;
    private float _targetValue;
    private bool _isAnimating;

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
        _targetValue = currentHealth;
        _isAnimating = true;
    }

    private void Update()
    {
        if (_isAnimating)
        {
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                _targetValue,
                _smoothSpeed * Time.deltaTime * _slider.maxValue
            );

            if (Mathf.Approximately(_slider.value, _targetValue))
            {
                _isAnimating = false;
            }
        }
    }
}