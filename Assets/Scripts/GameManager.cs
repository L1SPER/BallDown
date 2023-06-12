using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject[] canvasses;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject firstBlock;
    public Toggle[] toggles;
   
    Vector2 circleBeginningPos;
    Vector2 firstBlockBeginningPos;
    int gameModeValue;
    [SerializeField] float touchScreenCanvasWaitTime;
    public enum GameMod
    {
        PhoneRotation,
        TouchScreen
    }
    public GameMod currentGameMod;
    private void Awake()
    {
        UpdateMode();
        Singleton();
        Time.timeScale = 0f;
    }
    private void Update()
    {
        Debug.Log("Current game mode: " + currentGameMod);
        Debug.Log("gameModeValue " + gameModeValue);
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
        ShowCanvas("StartGameCanvas");
        circleBeginningPos =circle.transform.position;
        firstBlockBeginningPos=firstBlock.transform.position;
        ScoreManager.Instance.ShowBestScoreInStartGame();
        ScoreManager.Instance.UpdateTriangleScoreDisplay();

    }
    public void StartGame()
    {
        if(currentGameMod==GameMod.TouchScreen)
        {
            StartCoroutine(WaitTime("TouchScreenCanvas", touchScreenCanvasWaitTime));
        }
        AudioManager.Instance.Play("Theme");
        Time.timeScale = 1f;
        AddAllBlocksToPool();
        AddAllTrianglesToPool();
        CloseCanvas("StartGameCanvas");
        ShowCanvas("InGameCanvas");
        ScoreManager.Instance.ResetScore();
        circle.transform.position = circleBeginningPos;
        firstBlock.SetActive(true);
        firstBlock.transform.position = firstBlockBeginningPos;
        circle.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        firstBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);
    }
    public void GameOver()
    {
        AudioManager.Instance.Stop("Theme");
        Time.timeScale = 0f;
        AddAllBlocksToPool();
        AddAllTrianglesToPool();
        CloseCanvas("InGameCanvas");
        ShowCanvas("GameOverCanvas");
        ScoreManager.Instance.ShowScoreInGameOver();
    }
    public void RestartGame()
    {
        if (currentGameMod == GameMod.TouchScreen)
        {
            StartCoroutine(WaitTime("TouchScreenCanvas", touchScreenCanvasWaitTime));
        }
        AudioManager.Instance.Play("Theme");
        CloseCanvas("GameOverCanvas");
        ShowCanvas("InGameCanvas");
        AddAllBlocksToPool();
        AddAllTrianglesToPool();
        circle.transform.position= circleBeginningPos;
        firstBlock.transform.position = firstBlockBeginningPos;
        firstBlock.SetActive(true);
        circle.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        firstBlock.GetComponent<Rigidbody2D>().velocity= new Vector2(0f, 3f);
        Time.timeScale=1f;
        ScoreManager.Instance.ResetScore();
    }
    public void SettingsMenu()
    {
        CloseCanvas("StartGameCanvas");
        ShowCanvas("SettingsCanvas");
    }
    public void HowToPlayCanvas()
    {
        CloseCanvas("StartGameCanvas");
        ShowCanvas("HowToPlayGameCanvas");
    }
    public void BackToStartGame()
    {
        UpdateMode();
        CloseCanvas("SettingsCanvas");
        CloseCanvas("GameOverCanvas");
        CloseCanvas("HowToPlayGameCanvas");
        ShowCanvas("StartGameCanvas");
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
    public void ShowCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }
    public void CloseCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }
    public void ShowCanvas(string canvas)
    {
        GameObject tmpCanvas = Array.Find(canvasses, canvass => canvass.name == canvas);
        if(tmpCanvas == null)
        {
            Debug.LogWarning("Canvas " + canvas + "not found");
            return;
        }
        tmpCanvas.SetActive(true);
    }
    public void CloseCanvas(string canvas)
    {
        GameObject tmpCanvas = Array.Find(canvasses, canvass => canvass.name == canvas);
        if (tmpCanvas == null)
        {
            Debug.LogWarning("Canvas " + canvas + "not found");
            return;
        }
        tmpCanvas.SetActive(false);
    }
    public void ChangeMode()
    {
        if (toggles[0].isOn)
        {
            currentGameMod = GameMod.PhoneRotation;
            PlayerPrefs.SetInt("GameMode", 0);
        }
        else if (toggles[1].isOn)
        {
            currentGameMod = GameMod.TouchScreen;
            PlayerPrefs.SetInt("GameMode", 1);
        }
    }
    public void UpdateMode()
    {
        gameModeValue = PlayerPrefs.GetInt("GameMode", 0);
        if (gameModeValue == 0)
        {
            toggles[0].isOn = true;
            toggles[1].isOn = false;
            currentGameMod = GameMod.PhoneRotation;
        }
        else if (gameModeValue == 1)
        {
            toggles[1].isOn = true;
            toggles[0].isOn = false;
            currentGameMod = GameMod.TouchScreen;
        }
    }
    IEnumerator WaitTime(string canvas,float time)
    {
        GameObject tmpCanvas = Array.Find(canvasses, canvass => canvass.name == canvas);
        if (tmpCanvas == null)
        {
            Debug.LogWarning("Canvas " + canvas + "not found");
            yield break;
        }
        tmpCanvas.SetActive(true);
        yield return new WaitForSeconds(time);
        tmpCanvas.SetActive(false);
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
