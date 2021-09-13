using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region VARIABLES
    [HideInInspector] public static int Score;
    public static bool isGameStarted;
    public static bool isSoundsActive;
    public static GameManager Singleton;
    


    #endregion
    private void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
        Score = 0;
        isSoundsActive = true;
    }
    public void GameStarted()
    {
        // When we start the game, we close the menu panel and open the game panel.
        UIManager.Singleton.menuPanel.SetActive(false);
        UIManager.Singleton.gamePlayPanel.SetActive(true);

        isGameStarted = true;
    }

    #region SCORE OPERATIONS
    public void SetScore(int _score)
    {
        // Adding the Outside Score.
        Score += _score;
        // Printing Score to Text
        UIManager.Singleton.scoreText.text = "SCORE: " + Score;

        // Save the Score.
        if (Score > PlayerPrefs.GetInt("bestScore", 0))
        {
            PlayerPrefs.SetInt("bestScore", Score);
            UIManager.Singleton.bestScoreText.text = "BEST SCORE: " + Score;
        }
    }

    public void GetBestScore()
    {
        // If there is saved data
        if (PlayerPrefs.HasKey("bestScore"))
        {
            UIManager.Singleton.bestScoreText.text = "BEST SCORE: " + PlayerPrefs.GetInt("bestScore");
        }
        else
        {
            UIManager.Singleton.bestScoreText.text = "BEST SCORE: 0";
        }
    }
    #endregion


    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




}
