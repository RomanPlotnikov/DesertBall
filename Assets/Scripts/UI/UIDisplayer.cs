using TMPro;
using UnityEngine;

public class UIDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText; 
    [SerializeField] private CoinContainer _coinContainer; 

    private void Start()
    {
        DisplayCoins();
    }

    private void OnEnable()
    {
        _coinContainer.CoinCollected += DisplayCoins;
    }

    private void OnDisable()
    {
        _coinContainer.CoinCollected -= DisplayCoins;
    }

    private void DisplayCoins()
    {
        _coinText.text = _coinContainer.CollectedCoinsNumber + "/" + _coinContainer.TotalCoinsNumber;
    }
}