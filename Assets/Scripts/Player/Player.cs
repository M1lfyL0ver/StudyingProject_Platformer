using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerInputHandler), typeof(GroundDetector))]
[RequireComponent(typeof(PlayerAnimationSwitcher), typeof(CoinCollector))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInputHandler _playerInputHandler;
    private GroundDetector _groundDetector;
    private PlayerAnimationSwitcher _playerAnimationSwitcher;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _groundDetector = GetComponent<GroundDetector>();
        _playerAnimationSwitcher = GetComponent<PlayerAnimationSwitcher>();
    }

    private void OnEnable()
    {
        _playerInputHandler.LeftRightPressed += ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed += Jump;
    }

    private void OnDisable()
    {
        _playerInputHandler.LeftRightPressed -= ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed -= Jump;
    }

    private void Jump(Vector2 direction)
    {
        _playerMover.Jump(direction, _groundDetector.IsGrounded());
    }

    private void ChangeMovementSpeed(Vector2 direction)
    {
        _playerAnimationSwitcher.SetRunAnimation(direction);
        _playerMover.ChangeMovementSpeed(direction);
    }
}