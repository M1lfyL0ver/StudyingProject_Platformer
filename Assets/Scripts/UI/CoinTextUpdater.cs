using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinTextUpdater : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _wallet.CoinsCountChanged += ChangeCoinsText;
    }

    private void OnDisable()
    {
        _wallet.CoinsCountChanged -= ChangeCoinsText;
    }

    private void ChangeCoinsText(int coinsCount)
    {
        _text.text = $"Монетки : {coinsCount.ToString()}";
    }
}