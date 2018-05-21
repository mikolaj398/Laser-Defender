using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private int score = 0;
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
    public void Reset()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
