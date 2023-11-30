using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private string _scoreFormat = "000000";

    private int _currentScore;

    private void Awake()
    {
        UpdateScore(_currentScore);
    }

    private void OnEnable()
    {
        _enemySpawner.EnemyDied += UpdateScore;
    }

    private void OnDisable()
    {
        _enemySpawner.EnemyDied -= UpdateScore;
    }

    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScore(_currentScore);
    }

    private void UpdateScore(int score)
    {
        _currentScore += score;
        _scoreDisplay.text = _currentScore.ToString(_scoreFormat);
    }
}
