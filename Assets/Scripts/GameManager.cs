using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int charIndex;
    public GameObject[] charachters;
    public PlayerPresets activePreset;

    int highScore;
    [SerializeField] PlayerPresets[] playerPresets;
    [SerializeField] int Score;
    [SerializeField] int life;
    [SerializeField] Text ScoreTxt, highScoretxt, Livestxt;
    [SerializeField] GameObject GameOverScreen;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("SCORE"))
        {
            Debug.Log("exists ");
            highScore = PlayerPrefs.GetInt("SCORE");
        }
        else
        {
            highScore = 0;
        }
    }
    void Start()
    {
        activePreset = playerPresets[0];
        charIndex = PlayerPrefs.GetInt("charIndex");
        ChangeChar(charIndex);
        GameOverScreen.SetActive(false);
        highScoretxt.text = "High Score: " + highScore;
    }

    public void Lives()
    {
        life--;
        if (life == 0)
        {
            MainMenu.Instance.HideOrShow(false);
            GameOverScreen.SetActive(true);
        }
    }

    public void InGameScore()
    {
        Score++;
        SetDefaultScore();
    }

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void SetDefaultScore()
    {
        Livestxt.text = "Current Lives : " + life;
        highScoretxt.text = "High Score: " + highScore;
        ScoreTxt.text = "Score : " + Score;
    }
    public void SetHighScore()
    {
        if (Score >= highScore)
        {
            PlayerPrefs.SetInt("SCORE", Score);
            highScore = Score;
        }
    }

    public void ChangeChar(int character)
    {
        foreach (var item in charachters)
        {
            item.SetActive(false);
        }
        charachters[character].SetActive(true);
        activePreset = playerPresets[character];
        charIndex = character;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("charIndex", charIndex);
    }
}
