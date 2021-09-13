using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region VARIABLES
    [Header("VARIABLES")]
    [SerializeField] private Image soundsIcon;
    public Text scoreText, bestScoreText;
    public Text gameOverScore,gameOverBestScore;

    [Space]
    [Header("UI Panels")]
    public GameObject gamePlayPanel,gameOverPanel,menuPanel;
    #endregion

    public static UIManager Singleton;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        GameManager.Singleton.GetBestScore();
    }


    #region GAMEOVER PANEL
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverScore.text = "SCORE: "+GameManager.Score;
        gameOverBestScore.text = "BEST SCORE: "+PlayerPrefs.GetInt("bestScore");
        GameManager.isGameStarted = false;

    }
    #endregion

    #region SOUNDS
    public void SoundsButton()
    {
        // Sound On and Off Operations.
        if (GameManager.isSoundsActive)
        {
            soundsIcon.sprite = Resources.Load<Sprite>("Sprites/soundsOffIcon");
            GameManager.isSoundsActive = false;
        }
        else
        {
            soundsIcon.sprite = Resources.Load<Sprite>("Sprites/soundsOnIcon");
            GameManager.isSoundsActive = true;
        }
    }
    #endregion

    #region DATA CLEAR
    public void ResetData()
    {
        // All User Data Deleted.
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
