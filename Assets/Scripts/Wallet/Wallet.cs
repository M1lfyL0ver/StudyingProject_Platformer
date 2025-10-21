using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private CoinCollector _player;

    private int _coinsCount;

    public Action<int> CoinsCountChanged;

    private void OnEnable()
    {
        _player.CoinPickedUp += AddCoinToWallet;
    }

    private void OnDisable()
    {
        _player.CoinPickedUp -= AddCoinToWallet;
    }

    private void AddCoinToWallet()
    {
        _coinsCount++;
        CoinsCountChanged?.Invoke(_coinsCount);
    }
}