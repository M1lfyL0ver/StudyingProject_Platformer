using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmoothSliderBar : HealthBar
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 2f;

    private float _targetValue;
    private Coroutine _currentAnimation;

    public override void HandleUpdateBar(float currentHealth, float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _targetValue = currentHealth;

        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
        }

        _currentAnimation = StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateSlider()
    {
        while (Mathf.Approximately(_slider.value, _targetValue) == false)
        {
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                _targetValue,
                _smoothSpeed * Time.deltaTime * _slider.maxValue
            );

            yield return null;
        }

        _currentAnimation = null;
    }

    private void OnDisable()
    {
        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
            _currentAnimation = null;
        }
    }
}