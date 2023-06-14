using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualManager : MonoBehaviour
{
    private const string MENU_SCENE = "Menu";

    [Header("Gameplay")]
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private float m_StartTimeToIncrease;
    [SerializeField] private float m_EndTimeToIncrease;
    [SerializeField] private float m_StartTimeScale;
    [SerializeField] private float m_EndTimeScale;

    [Header("Game Over")]
    [SerializeField] private GameObject m_GameOverScreen;
    [SerializeField] private TextMeshProUGUI m_GameOverScoreText;

    private float m_GameOverTimer = 0;

    private float m_GameTimer = 0;

    private int m_LastKnownScore = -1;

    private void Start()
    {
        Time.timeScale = m_StartTimeScale;
        PlayerController.Instance.OnGameOver += OnGameOver; 
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        m_GameTimer += Time.unscaledDeltaTime;

        if (m_LastKnownScore != PlayerController.Instance.Score)
        {
            m_LastKnownScore = PlayerController.Instance.Score;
            m_ScoreText.text = $"Score: {m_LastKnownScore}";
        }

        if (PlayerController.Instance.IsGameOver)
        {
            m_GameOverTimer += Time.deltaTime;

            if (m_GameOverTimer > 1.5f && Input.GetKeyDown(KeyCode.Space))
            {
                Replay();
            }
        }

        if (m_GameTimer < m_StartTimeToIncrease)
        {
            Time.timeScale = m_StartTimeScale;
        }
        else if (m_GameTimer > m_EndTimeToIncrease)
        {
            Time.timeScale = m_EndTimeScale;
        }
        else
        {
            float timeRange = m_EndTimeToIncrease - m_StartTimeToIncrease;
            float normalisedTimePassed = (m_GameTimer - m_StartTimeToIncrease) / timeRange;

            float scaleRange = m_EndTimeScale - m_StartTimeScale;
            float currentScaleValue = (scaleRange * normalisedTimePassed) + m_StartTimeScale;

            Time.timeScale = currentScaleValue;
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

    public void BackToMenu()
    {
        SceneManager.LoadScene(MENU_SCENE);
    }
}

