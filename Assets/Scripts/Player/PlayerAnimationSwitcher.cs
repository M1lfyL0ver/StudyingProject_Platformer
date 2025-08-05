using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationSwitcher : MonoBehaviour
{
    private const string IsRunning = nameof(IsRunning);

    private Animator _animator;
    private int _isRunning;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isRunning = Animator.StringToHash(nameof(IsRunning));
    }

    public void SetRunAnimation(Vector2 direction)
    {
        _animator.SetBool(_isRunning, direction.x != 0);
    }
}