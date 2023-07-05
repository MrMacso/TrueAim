using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private TextMeshProUGUI scoreText;

    int score;


    private void Awake()
    {
        instance = this;
        scoreText = this.GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        scoreText.text = score.ToString();
    }
    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
