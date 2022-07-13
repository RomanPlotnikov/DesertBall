using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameReloader : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private CoinContainer _coinContainer;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartLevel);
        _coinContainer.CoinCollected += CheckCoinsNumber;

        _restartButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartLevel);
        _coinContainer.CoinCollected -= CheckCoinsNumber;
    }

    private void CheckCoinsNumber()
    {
        if (_coinContainer.CollectedCoinsNumber == _coinContainer.TotalCoinsNumber)
            PauseGame();
    }

    private void PauseGame()
    {
        _restartButton.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}