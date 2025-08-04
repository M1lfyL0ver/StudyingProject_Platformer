using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMover))]
public class PlayerAnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.DirectionChanged += SetRunAnimation;
    }

    private void OnDisable()
    {
        _playerMover.DirectionChanged -= SetRunAnimation;
    }

    private void SetRunAnimation(Vector2 direction)
    {
        _animator.SetBool("IsRunning", direction.x != 0);
    }
}