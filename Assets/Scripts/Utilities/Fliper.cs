using UnityEngine;

public class Fliper : MonoBehaviour
{
    private Quaternion _right = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion _left = Quaternion.Euler(0f, 180f, 0f);

    public void Flip(Vector2 direction)
    {
        if (direction.x > 0 && !IsFacingRight() || direction.x < 0 && IsFacingRight())
        {
            transform.rotation = transform.rotation == _right ? _left : _right;
        }
    }

    private bool IsFacingRight()
    {
        return transform.rotation == _right;
    }
}