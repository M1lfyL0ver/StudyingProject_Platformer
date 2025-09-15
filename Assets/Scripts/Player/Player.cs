using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerInputHandler), typeof(GroundDetector))]
[RequireComponent(typeof(PlayerAnimationSwitcher), typeof(CoinCollector), typeof(Health))]
[RequireComponent(typeof(DamageDealer), typeof(HitShower), typeof(HealCollector))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInputHandler _playerInputHandler;
    private GroundDetector _groundDetector;
    private PlayerAnimationSwitcher _playerAnimationSwitcher;
    private Health _health;
    private HitShower _hitShower;
    private HealCollector _healCollector;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _groundDetector = GetComponent<GroundDetector>();
        _playerAnimationSwitcher = GetComponent<PlayerAnimationSwitcher>();
        _health = GetComponent<Health>();
        _hitShower = GetComponent<HitShower>();
        _healCollector = GetComponent<HealCollector>();
    }

    private void OnEnable()
    {
        _playerInputHandler.LeftRightPressed += ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed += Jump;
        _health.DamageDealed += HandleHit;
        _healCollector.HealPickedUp += HandleHeal;
    }

    private void OnDisable()
    {
        _playerInputHandler.LeftRightPressed -= ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed -= Jump;
        _health.DamageDealed -= HandleHit;
        _healCollector.HealPickedUp -= HandleHeal;
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

    private void HandleHit()
    {
        StartCoroutine(_hitShower.Flash());
    }

    private void HandleHeal(int heal)
    {
        _health.HealHitpoints(heal);
    }
}