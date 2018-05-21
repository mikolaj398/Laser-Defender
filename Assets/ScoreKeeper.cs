using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text scoreText;
    private void Start()
    {
        scoreText = GetComponent<Text>();
        Reset();
    }
    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    public static void Reset()
    {
        score = 0;
    }
}
