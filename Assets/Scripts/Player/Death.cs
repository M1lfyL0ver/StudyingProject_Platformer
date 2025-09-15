using UnityEngine;

public class Death : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.PlayerIsDead += SetPlayerDisabled;
    }

    private void OnDisable()
    {
        _health.PlayerIsDead -= SetPlayerDisabled;
    }

    private void SetPlayerDisabled()
    {
        _health.gameObject.SetActive(false);
    }
}