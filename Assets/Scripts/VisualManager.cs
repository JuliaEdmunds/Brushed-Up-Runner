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

    private float m_GameOverTimer = 0;

    private float m_GameTimer = 0;

    private void Start()
    {
        Time.timeScale = 1.0f;
        PlayerController.Instance.OnGameOver += OnGameOver;
    }

    private void Update()
    {
        m_GameTimer += Time.unscaledDeltaTime;

        m_ScoreText.text = $"Score: {PlayerController.Instance.Score}";

        if (PlayerController.Instance.IsGameOver)
        {
            m_GameOverTimer += Time.deltaTime;

            if (m_GameOverTimer > 1.5f && Input.GetKeyDown(KeyCode.Space))
            {
                Replay();
            }            
        }

        if (m_GameTimer > 10)
        {
            Time.timeScale = 1.2f;
        }
        else if (m_GameTimer > 30)
        {
            Time.timeScale = 1.4f;
        }
    }

    private void OnGameOver()
    {
        int score = PlayerController.Instance.Score;

        string textToShow = PlayerController.Instance.IsNewBestScore ? $"New best score: {score}" : $"Your score: {score} \r\nBest score: {PlayerScoreHelper.GetBestScore()}";

        m_GameOverScoreText.text = textToShow;
        m_GameOverScreen.SetActive(true);
    }

    public void Replay()
    {
        m_GameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

