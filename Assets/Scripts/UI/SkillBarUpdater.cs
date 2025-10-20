using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillBarUpdater : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private VampiricAura _aura;
    [SerializeField] private float _animationSpeed = 1f;

    private Coroutine _currentAnimation;

    private void OnEnable()
    {
        _aura.AuraDurationChanged += DrawDuration;
        _aura.AuraCooldownChanged += DrawCooldown;
    }

    private void OnDisable()
    {
        _aura.AuraDurationChanged -= DrawDuration;
        _aura.AuraCooldownChanged -= DrawCooldown;

        if (_currentAnimation != null)
        {
            StopCoroutine(_currentAnimation);
            _currentAnimation = null;
        }
    }

    private void DrawCooldown(float maxValue)
    {
        _bar.maxValue = maxValue;

        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);

        _currentAnimation = StartCoroutine(AnimateSlider(0f, maxValue));
    }

    private void DrawDuration(float maxValue)
    {
        _bar.maxValue = maxValue;

        if (_currentAnimation != null)
            StopCoroutine(_currentAnimation);

        _currentAnimation = StartCoroutine(AnimateSlider(maxValue, 0f));
    }

    private IEnumerator AnimateSlider(float startValue, float endValue)
    {
        _bar.value = startValue;
        float elapsedTime = 0f;
        float duration = Mathf.Abs(endValue - startValue) / _animationSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            _bar.value = Mathf.Lerp(startValue, endValue, progress);
            yield return null;
        }

        _bar.value = endValue;
        _currentAnimation = null;
    }
}