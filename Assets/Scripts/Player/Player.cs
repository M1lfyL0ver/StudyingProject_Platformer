using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerInputHandler))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInputHandler _playerInputHandler;
    private GroundDetector _groundDetector;
    private PlayerAnimationSwitcher _playerAnimationSwitcher;
    private CoinCollector _coinCollector;

    public Action<int> CoinsCountChanged;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _groundDetector = gameObject.AddComponent<GroundDetector>();
        _playerAnimationSwitcher = gameObject.AddComponent<PlayerAnimationSwitcher>();
        _coinCollector = gameObject.AddComponent<CoinCollector>();
    }

    private void OnEnable()
    {
        _playerInputHandler.LeftRightPressed += ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed += Jump;
        _coinCollector.CoinsCountChanged += ChangeCoinCountText;
    }

    private void OnDisable()
    {
        _playerInputHandler.LeftRightPressed -= ChangeMovementSpeed;
        _playerInputHandler.UpDownPressed -= Jump;
        _coinCollector.CoinsCountChanged -= ChangeCoinCountText;
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

    private void ChangeCoinCountText(int coinsCount)
    {
        CoinsCountChanged?.Invoke(coinsCount);
    }
}