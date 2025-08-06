using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyCounter : MonoBehaviour
{
    [SerializeField] Player _player;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _player.CoinsCountChanged += ChangeCoinsText;
    }

    private void OnDisable()
    {
        _player.CoinsCountChanged -= ChangeCoinsText;
    }

    private void ChangeCoinsText(int coinsCount)
    {
        _text.text = $"Монетки : {coinsCount.ToString()}";
    }
}