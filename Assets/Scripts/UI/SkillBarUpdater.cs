using UnityEngine;
using UnityEngine.UI;

public class SkillBarUpdater : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private VampiricAura _aura;

    private void OnEnable()
    {
        _aura.TimerChanged += OnTimerChanged;
        _bar.value = 0f;
        _bar.maxValue = 1f;
    }

    private void OnDisable()
    {
        _aura.TimerChanged -= OnTimerChanged;
    }

    private void OnTimerChanged(float currentTime, float maxTime)
    {
        if (maxTime > 0f)
        {
            _bar.maxValue = maxTime;
            _bar.value = currentTime;
        }
        else
        {
            _bar.value = 0f;
        }
    }
}