using System.Collections;
using UnityEngine;

public class HitShower : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.red;
    [SerializeField] private float _flashDuration = 0.1f;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public IEnumerator Flash()
    {
        _spriteRenderer.color = _flashColor;
        yield return new WaitForSeconds(_flashDuration);
        _spriteRenderer.color = _originalColor;
    }
}
