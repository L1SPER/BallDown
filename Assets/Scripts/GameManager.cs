using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] GameObject startGameCanvas;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject firstBlock;

   
    Vector2 circleBeginningPos;
    Vector2 firstBlockBeginningPos;
    
    private void Awake()
    {
        Singleton();
        Time.timeScale = 0f;
    }

    private void Singleton()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        ShowStartGameCanvas();
        circleBeginningPos =circle.transform.position;
        firstBlockBeginningPos=firstBlock.transform.position;
        ScoreManager.Instance.ShowBestScoreInStartGame();
        ScoreManager.Instance.UpdateTriangleScoreDisplay();

    }
    public void StartGame()
    {
        AudioManager.Instance.Play("Theme");
        Time.timeScale = 1f;
        AddAllBlocksToPool();
        AddAllTrianglesToPool();
        CloseStartGameCanvas();
        ShowInGameCanvas();
        ScoreManager.Instance.ResetScore();
        circle.transform.position = circleBeginningPos;
        firstBlock.SetActive(true);
        firstBlock.transform.position = firstBlockBeginningPos;
        circle.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        firstBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);

        //Tüm bloklar poola eklencek
    }
    public void GameOver()
    {
        AudioManager.Instance.Stop("Theme");
        Time.timeScale = 0f;
        AddAllBlocksToPool();
        AddAllTrianglesToPool();
        CloseInGameCanvas();
        ShowGameOverCanvas();
        ScoreManager.Instance.ShowScoreInGameOver();

        //Tüm bloklar poola eklencek
    }
    public void RestartGame()
    {
        AudioManager.Instance.Play("Theme");
        CloseGameOverCanvas();
        ShowInGameCanvas();
        AddAllBlocksToPool();
        AddAllTrianglesToPool();
        circle.transform.position= circleBeginningPos;
        firstBlock.transform.position = firstBlockBeginningPos;
        firstBlock.SetActive(true);
        circle.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        firstBlock.GetComponent<Rigidbody2D>().velocity= new Vector2(0f, 3f);
        Time.timeScale=1f;
        ScoreManager.Instance.ResetScore();
        //Tüm bloklar poola eklencek
    }
    public void BackToStartGame()
    {
        CloseGameOverCanvas();
        ShowStartGameCanvas();
        ScoreManager.Instance.ShowBestScoreInStartGame();
        ScoreManager.Instance.UpdateTriangleScoreDisplay();
    }
    public void AddAllBlocksToPool()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in blocks)
        {
            block.SetActive(false);
        }
    }
    public void AddAllTrianglesToPool()
    {
        GameObject[] triangles = GameObject.FindGameObjectsWithTag("Triangle");
        foreach (GameObject triangle in triangles)
        {
            triangle.SetActive(false);
        }
    }
    public void ShowStartGameCanvas()
    {
        startGameCanvas.SetActive(true);
    }
    public void CloseStartGameCanvas()
    {
        startGameCanvas.SetActive(false);
    }
    public void ShowInGameCanvas()
    {
        inGameCanvas.SetActive(true);
    }
    public void CloseInGameCanvas()
    {
        inGameCanvas.SetActive(false);
    }
    public void ShowGameOverCanvas()
    {
        gameOverCanvas.SetActive(true);  
    }
    public void CloseGameOverCanvas()
    {
        gameOverCanvas.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadLevelByName(string name)
    {
        SceneManager.LoadScene(name);
    }
   

};
