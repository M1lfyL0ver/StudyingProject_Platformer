using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VampiricAuraVisualizer))]
public class VampiricAura : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _duration = 4f;
    [SerializeField] private float _cooldown = 6f;
    [SerializeField] private float _period = 0.5f;

    private Health _player;
    private VampiricAuraVisualizer _visualizer;

    private bool _isActive = false;
    private bool _isOnCooldown = false;

    public event Action<float> AuraDurationChanged;
    public event Action<float> AuraCooldownChanged;

    private void Awake()
    {
        _player = GetComponent<Health>();
        _visualizer = GetComponent<VampiricAuraVisualizer>();
        _visualizer.SetInactive();
    }

    public void TrySetActive()
    {
        if (_isActive == false && _isOnCooldown == false)
        {
            _isActive = true;
            _visualizer.SetActive(_radius);
            StartCoroutine(VampiricCoroutine());
            AuraDurationChanged?.Invoke(_duration);
        }
    }

    private IEnumerator VampiricCoroutine()
    {
        float timer = 0f;
        WaitForSeconds period = new WaitForSeconds(_period);

        while (timer < _duration)
        {
            timer += _period;
            ApplyVampirism();
            yield return period;
        }

        _visualizer.SetInactive();
        _isActive = false;
        StartCoroutine(CooldownCoroutine());
        AuraCooldownChanged?.Invoke(_cooldown);
    }

    private IEnumerator CooldownCoroutine()
    {
        _isOnCooldown = true;

        yield return new WaitForSeconds(_cooldown);

        _isOnCooldown = false;
    }

    private void ApplyVampirism()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, _radius);
        Health enemy = null;

        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out _))
            {
                enemy = collider.GetComponent<Health>();
                enemy.TakeDamage(_damage);
                _player.HealHitpoints(_damage);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}