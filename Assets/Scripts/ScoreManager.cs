using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTextInGame;
    [SerializeField] TextMeshProUGUI scoreTextInGameOver;
    [SerializeField] TextMeshProUGUI bestScoreTextInGameOver;
    [SerializeField] TextMeshProUGUI bestScoreTextInStartGame;
    [SerializeField] TextMeshProUGUI triangleScoreInStartGame;
    public static ScoreManager Instance { get; private set; }
    public float Score { get; private set; }
    public float BestScore { get; private set; }
    public int TriangleScore { get;private set; }
    private void Start()
    {
        scoreTextInGame.text = "";
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        IncreaseScore(1*Time.deltaTime);
        //if (Input.GetKey(KeyCode.Space))
        //    ResetBestScore();
    }
    public void IncreaseScore(float amount)
    {
        Score += amount;
        if(Score > PlayerPrefs.GetInt("High Score", 0))
        {
            BestScore= Score;
            PlayerPrefs.SetInt("High Score", Mathf.FloorToInt(BestScore));
        }   
        UpdateScoreDisplay();
    }
    public void IncreaseTriangleScore(int amount)
    {
        TriangleScore += amount;
        PlayerPrefs.SetInt("Triangle Score", TriangleScore);
    }
    public void UpdateTriangleScoreDisplay()
    {
        triangleScoreInStartGame.text= ""+ PlayerPrefs.GetInt("Triangle Score", 0).ToString();
    }
    public void ResetScore()
    {
        Score=0;
    }
    public void ResetBestScore()
    {
        BestScore = 0;
        PlayerPrefs.DeleteKey("High Score");
    }
    public void UpdateScoreDisplay()
    {
        scoreTextInGame.text = ""+ Mathf.FloorToInt(Score).ToString();
    }
    public void ShowScoreInGameOver()
    {
        scoreTextInGameOver.text= ""+Mathf.FloorToInt(Score).ToString();
        bestScoreTextInGameOver.text = ""+PlayerPrefs.GetInt("High Score",0).ToString();
    }
    public void ShowBestScoreInStartGame()
    {
        bestScoreTextInStartGame.text = "Best Score : " + PlayerPrefs.GetInt("High Score", 0).ToString() ;
    }
    
}
