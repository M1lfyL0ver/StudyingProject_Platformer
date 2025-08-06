using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundDetector : MonoBehaviour
{
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(_boxCollider.size.x, 0.15f);
        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (_boxCollider.size.y / 2 + 0.1f);
        float distance = 0.1f;

        return Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, distance);
    }
}