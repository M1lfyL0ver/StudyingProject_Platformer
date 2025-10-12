using UnityEngine;
using UnityEngine.UI;

public class HealthBarSpawner : MonoBehaviour
{
    [SerializeField] Transform _targets;
    [SerializeField] Slider _healthBar;

    private void Start()
    {
        foreach (Transform target in _targets)
        {
            
        }
    }
}