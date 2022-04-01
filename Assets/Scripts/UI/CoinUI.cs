using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _coinsText;

    private int _coinsCount;

    public void AddCoins(int value)
    {
        if (value < 0)
            return;

        _coinsCount += value;

        UpdateCoinsUI();
    }

    private void Start()
    {
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        _coinsText.text = _coinsCount.ToString();
    }
}
