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

    [SerializeField] private TextMeshProUGUI m_BestScoreText;

    private void Start()
    {
        m_BestScoreText.text = $"Best score: {PlayerScoreHelper.GetBestScore()}";
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GAMEPLAY_SCENE);
    }
}

