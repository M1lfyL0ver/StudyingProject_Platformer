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
        _health.IsDead += SetGameObjectDisabled;
    }

    private void OnDisable()
    {
        _health.IsDead -= SetGameObjectDisabled;
    }

    private void SetGameObjectDisabled()
    {
        _health.gameObject.SetActive(false);
    }
}