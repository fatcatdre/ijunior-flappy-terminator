using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Screen _startScreen;
    [SerializeField] private Screen _gameOverScreen;
    [SerializeField] private PoolGroup _pools;
    [SerializeField] private ScoreDisplay _scoreDisplay;

    private void Awake()
    {
        Time.timeScale = 0f;

        _startScreen.Open();
        _gameOverScreen.Close();
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;

        _startScreen.ButtonClick += OnStartButtonClicked;
        _gameOverScreen.ButtonClick += OnRestartButtonClicked;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;

        _startScreen.ButtonClick -= OnStartButtonClicked;
        _gameOverScreen.ButtonClick -= OnRestartButtonClicked;
    }

    private void OnPlayerDied()
    {
        StopGame();
    }

    private void OnStartButtonClicked()
    {
        StartGame();

        _startScreen.Close();
    }

    private void OnRestartButtonClicked()
    {
        StartGame();

        _gameOverScreen.Close();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;

        _scoreDisplay.ResetScore();
        _pools.ClearAll();
        _player.Respawn();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;

        _gameOverScreen.Open();
    }
}
