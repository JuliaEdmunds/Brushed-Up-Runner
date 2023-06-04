using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private const string GAMEPLAY_SCENE = "Main";
    private float m_Timer = 0;

    [SerializeField] private TextMeshProUGUI m_BestScoreText;
    [SerializeField] private GameObject m_ExitButton;

    private void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        const bool IS_EXIT_BUTTON_VISIBLE = true;
#else
        const bool IS_EXIT_BUTTON_VISIBLE = false;
#endif
        m_ExitButton.SetActive(IS_EXIT_BUTTON_VISIBLE);

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

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }
}

