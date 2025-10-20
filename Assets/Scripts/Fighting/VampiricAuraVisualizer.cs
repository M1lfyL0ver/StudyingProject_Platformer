using UnityEngine;

public class VampiricAuraVisualizer : MonoBehaviour
{
    [SerializeField] SpriteRenderer _visual;

    public void SetActive(float radius)
    {
        SetRadius(radius);
        _visual.enabled = true;
    }

    public void SetInactive()
    {
        _visual.enabled = false;
    }

    private void SetRadius(float radius)
    {
        _visual.gameObject.transform.localScale = new Vector3(radius, radius, 0);
    }
}