using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public Sprite[] slimeSprites;
    public TMP_Text coinText;
    public TMP_Text multiplierText;
    public TMP_Text timerText;
    public TMP_Text gameOverCoinText;
    public TMP_Text levelCompleteText;
    public TMP_Text levelCompleteCoinText;
    public Image multiplierImage;

    public GameObject gameoverPanel;
    public GameObject levelcompletePanel;
    private void OnEnable()
    {
        EventManager.onTimeChanged += EventManager_onTimeChanged;
        EventManager.onMultiplierChange += EventManager_onMultiplierChange;
        EventManager.onCoinsChanged += EventManager_onCoinsChange;
        EventManager.onLoseLevel += EventManager_onLoseLevel;
        EventManager.onWinLevel += EventManager_onWinLevel;
    }

    private void EventManager_onWinLevel()
    {
        levelcompletePanel.SetActive(true);
        levelCompleteText.text = $"Level {SceneManager.GetActiveScene().buildIndex} Complete!";
        levelCompleteCoinText.text = $"Coins: {GameManager.Instance.Coins}";
    }

    private void EventManager_onLoseLevel()
    {
        gameoverPanel.SetActive(true);
    }

    private void EventManager_onCoinsChange()
    {
        coinText.text = $"Coins: {GameManager.Instance.Coins}";
        gameOverCoinText.text = $"Coins: {GameManager.Instance.Coins}";
    }

    private void EventManager_onMultiplierChange(SlimeType type, int value)
    {
        switch (type)
        {
            case SlimeType.Blue:
                multiplierImage.sprite = slimeSprites[0];
                break;
            case SlimeType.Green:
                multiplierImage.sprite = slimeSprites[1];
                break;
            case SlimeType.Red:
                multiplierImage.sprite = slimeSprites[2];
                break;
            case SlimeType.Pink:
                multiplierImage.sprite = slimeSprites[3];
                break;
            case SlimeType.Silver:
                multiplierImage.sprite = slimeSprites[4];
                break;
            case SlimeType.Gold:
                multiplierImage.sprite = slimeSprites[5];
                break;
            case SlimeType.Mystic:
                multiplierImage.sprite = slimeSprites[6];
                break;
        }
        multiplierText.text = $"Coin X {value}";
        //multiplierText.text = $"{type.ToString()} Slime X {value}";
    }

    private void EventManager_onTimeChanged()
    {
        var span = new TimeSpan(0, 0, (int)GameManager.Instance.TimeRemaining);
        timerText.text = $"Time: {span.Minutes}:{span.Seconds}";
    }

    private void OnDisable()
    {
        EventManager.onTimeChanged -= EventManager_onTimeChanged;
        EventManager.onMultiplierChange -= EventManager_onMultiplierChange;
        EventManager.onCoinsChanged -= EventManager_onCoinsChange;
        EventManager.onLoseLevel -= EventManager_onLoseLevel;
        EventManager.onWinLevel -= EventManager_onWinLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        coinText.text = $"Coins: {GameManager.Instance.Coins}";
        gameOverCoinText.text = $"Coins: {GameManager.Instance.Coins}";
        var span = new TimeSpan(0, 0, (int)GameManager.Instance.TimeRemaining);
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

    public void StartGame()
    {
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

    public void PlaySFX(AudioClip clip)
    {
        AudioManager.Instance.Play(clip, Camera.main.transform);
    }
}
