using UnityEngine;

public class VampiricAuraVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _visual;

    private VampiricAura _aura;

    private void Awake()
    {
        _aura = GetComponent<VampiricAura>();
    }

    private void OnEnable()
    {
        _aura.AuraStatusSwitched += HandleAuraStatusSwitched;
    }

    private void OnDisable()
    {
        _aura.AuraStatusSwitched -= HandleAuraStatusSwitched;
    }

    private void HandleAuraStatusSwitched(bool isActive, float radius)
    {
        if(isActive)
        {
            SetActive(radius);
        }
        else
        {
            SetInactive();
        }
    }

    private void SetActive(float radius)
    {
        SetRadius(radius);
        _visual.enabled = true;
    }

    private void SetInactive()
    {
        _visual.enabled = false;
    }

    private void SetRadius(float radius)
    {
        _visual.gameObject.transform.localScale = new Vector3(radius, radius, 0);
    }
}