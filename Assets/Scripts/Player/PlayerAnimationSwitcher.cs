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
        _playerMover.directionChanged += SetRunAnimation;
    }

    private void OnDisable()
    {
        _playerMover.directionChanged -= SetRunAnimation;
    }

    private void SetRunAnimation(Vector2 direction)
    {
        if(direction.x != 0)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }
}