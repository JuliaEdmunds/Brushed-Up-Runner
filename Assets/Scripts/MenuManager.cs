﻿using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private const string GAMEPLAY_SCENE = "Main";
    private float m_Timer = 0;

    [SerializeField] private TextMeshProUGUI m_BestScoreText;
    [SerializeField] private TextMeshProUGUI m_InstructionText;
    [SerializeField] private GameObject m_ExitButton;

    private void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        const bool IS_EXIT_BUTTON_VISIBLE = true;
        m_InstructionText.text = "Press Play or Spacebar \r\nto start the game";
#else
        const bool IS_EXIT_BUTTON_VISIBLE = false;
        m_InstructionText.text = "Press Play button \r\nto start the game";
#endif
        m_ExitButton.SetActive(IS_EXIT_BUTTON_VISIBLE);

        m_BestScoreText.text = $"Best score: {PlayerScoreHelper.GetBestScore()}";
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR

        m_Timer += Time.deltaTime;

        if (m_Timer > 1.5f && Input.GetKeyDown(KeyCode.Space))
        {
            LoadGame();
        }
#endif
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
