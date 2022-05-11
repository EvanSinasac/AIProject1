using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Text ScoreText;
    private static Text HighScoreText;
    private static int _Score;
    private static int _HighScore;

    // Start is called before the first frame update
    void Start()
    {
        _Score = 0;
        _HighScore = 0;
        ScoreText = GameObject.Find("Score Text").GetComponent<Text>();
        HighScoreText = GameObject.Find("High Score Text").GetComponent<Text>();
    }

    public static void AddPoint()
    {
        _Score++;
        ScoreText.text = "SCORE: " + _Score;
        if (_Score >= _HighScore)
        {
            _HighScore = _Score;
            HighScoreText.text = "HIGH SCORE: " + _HighScore;
        }
    }

    public static void ResetPoints()
    {
        _Score = 0;
        ScoreText.text = "SCORE: " + _Score;
    }
}
