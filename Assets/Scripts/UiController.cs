using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text multiplierText;
    public TMP_Text timerText;
    public TMP_Text gameOverScoreText;
    public TMP_Text levelCompleteText;
    public TMP_Text levelCompleteScoreText;

    public GameObject gameoverPanel;
    public GameObject levelcompletePanel;
    private void OnEnable()
    {
        EventManager.onTimeChanged += EventManager_onTimeChanged;
        EventManager.onMultiplierChange += EventManager_onMultiplierChange;
        EventManager.onScoreChanged += EventManager_onScoreChanged;
        EventManager.onLoseLevel += EventManager_onLoseLevel;
        EventManager.onWinLevel += EventManager_onWinLevel;
    }

    private void EventManager_onWinLevel()
    {
        levelcompletePanel.SetActive(true);
        levelCompleteText.text = $"Level {SceneManager.GetActiveScene().buildIndex + 1} Complete!";
        levelCompleteScoreText.text = $"Score: {GameManager.instance.Score}";
    }

    private void EventManager_onLoseLevel()
    {
        gameoverPanel.SetActive(true);
    }

    private void EventManager_onScoreChanged()
    {
        scoreText.text = $"Score: {GameManager.instance.Score}";
        gameOverScoreText.text = $"Score: {GameManager.instance.Score}";
    }

    private void EventManager_onMultiplierChange(SlimeType type, int value)
    {
        multiplierText.text = $"{type.ToString()} Slime X {value}";
    }

    private void EventManager_onTimeChanged()
    {
        var span = new TimeSpan(0, 0, (int)GameManager.instance.TimeRemaining);
        timerText.text = $"Time: {span.Minutes}:{span.Seconds}";
    }

    private void OnDisable()
    {
        EventManager.onTimeChanged -= EventManager_onTimeChanged;
        EventManager.onMultiplierChange -= EventManager_onMultiplierChange;
        EventManager.onScoreChanged -= EventManager_onScoreChanged;
        EventManager.onLoseLevel -= EventManager_onLoseLevel;
        EventManager.onWinLevel -= EventManager_onWinLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = $"Score: {GameManager.instance.Score}";
        gameOverScoreText.text = $"Score: {GameManager.instance.Score}";
        var span = new TimeSpan(0, 0, (int)GameManager.instance.TimeRemaining);
        timerText.text = $"Time: {span.Minutes}:{span.Seconds}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConvertSlimesToPoints()
    {
        EventManager.SlimeConvertToPoints();
    }

    public void Retry()
    {
        EventManager.GameReset();
        SceneManager.LoadScene("Level1");
    }

    public void GotoNextLevel()
    {
        if(SceneManager.sceneCountInBuildSettings >= SceneManager.GetActiveScene().buildIndex + 1)
        {
            EventManager.LoadNextLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("No More Levels to Load!");
        }
    }
}
