using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PlayerScoreHelper
{
    private const string SCORE_PREF_KEY = "NA.BestScore";

    public static int GetBestScore()
    {
        if (PlayerPrefs.HasKey(SCORE_PREF_KEY))
        {
            return PlayerPrefs.GetInt(SCORE_PREF_KEY);
        }

        return 0;
    }

    public static void SetBestScore(int score)
    {
        PlayerPrefs.SetInt(SCORE_PREF_KEY, score);
    }
}

