using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private const string GAMEPLAY_SCENE = "Main";
    private float m_Timer = 0;

    [SerializeField] private TextMeshProUGUI m_BestScoreText;

    private void Start()
    {
        m_BestScoreText.text = $"Best score: {PlayerScoreHelper.GetBestScore()}";
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > 1.5f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadGame();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GAMEPLAY_SCENE);
    }
}

