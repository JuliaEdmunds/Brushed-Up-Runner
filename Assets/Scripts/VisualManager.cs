using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TextMeshProUGUI m_ScoreText;

    [Header("Game Over")]
    [SerializeField] private GameObject m_GameOverScreen;
    [SerializeField] private TextMeshProUGUI m_GameOverScoreText;


    private void Start()
    {
        PlayerController.Instance.OnGameOver += OnGameOver;
    }

    private void Update()
    {
        m_ScoreText.text = $"Score: {PlayerController.Instance.Score}";
    }

    private void OnGameOver()
    {
        int score = PlayerController.Instance.Score;

        m_GameOverScoreText.text = $"Your score: {score}";
        m_GameOverScreen.SetActive(true);
    }

    public void Replay()
    {
        m_GameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

