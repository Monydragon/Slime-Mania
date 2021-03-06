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
    public TMP_Text shopCoinText;
    public TMP_Text nextLevelButtonText;
    public GameObject shopPanel;
    public GameObject gameoverPanel;
    public GameObject levelcompletePanel;
    public GameObject pausePanel;

    public List<Button> ShopButtons;

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

        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextLevelButtonText.text = "Shop";
        }
    }

    private void EventManager_onLoseLevel()
    {
        gameoverPanel.SetActive(true);
    }

    private void EventManager_onCoinsChange()
    {
        coinText.text = $"Coins: {GameManager.Instance.Coins}";
        gameOverCoinText.text = $"Coins: {GameManager.Instance.Coins}";
        shopCoinText.text = $"Coins: {GameManager.Instance.Coins}";
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
        var span = new TimeSpan(0, 0, (int)GameManager.Instance.TimeRemaining);
        timerText.text = $"Time: {span.Minutes}:{span.Seconds}";
        EventManager.CoinsChanged();

        if (GameManager.Instance.SilverSlimeUnlocked)
        {
            ShopButtons[0].interactable = false;
        }
        if (GameManager.Instance.GoldSlimeUnlocked)
        {
            ShopButtons[1].interactable = false;
        }
        if (GameManager.Instance.MysticSlimeUnlocked)
        {
            ShopButtons[2].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.P) && GameManager.Instance.State == GameManager.GameState.Playing)
        {
            pausePanel.SetActive(true);
            GameManager.Instance.State = GameManager.GameState.Menu;
        }
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        GameManager.Instance.State = GameManager.GameState.Playing;
    }

    public void StartGame()
    {
        GameManager.Instance.State = GameManager.GameState.Playing;
        SceneManager.LoadScene("Level1");
    }

    public void GotoNextLevel()
    {
        if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            EventManager.LoadNextLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            shopPanel.SetActive(true);
            levelcompletePanel.SetActive(false);
            Debug.Log("No More Levels to Load!");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioManager.Instance.Play(clip, Camera.main.transform);
    }

    public void UnlockSilverSlime(int cost)
    {
        if(GameManager.Instance.Coins >= cost)
        {
            GameManager.Instance.Coins -= cost;
            EventManager.CoinsChanged();
            GameManager.Instance.SilverSlimeUnlocked = true;
            ShopButtons[0].interactable = false;
        }
    }

    public void UnlockGoldSlime(int cost)
    {
        if (GameManager.Instance.Coins >= cost)
        {
            GameManager.Instance.Coins -= cost;
            EventManager.CoinsChanged();
            GameManager.Instance.GoldSlimeUnlocked = true;
            ShopButtons[1].interactable = false;
        }
    }
    public void UnlockMysticSlime(int cost)
    {
        if (GameManager.Instance.Coins >= cost)
        {
            GameManager.Instance.Coins -= cost;
            EventManager.CoinsChanged();
            GameManager.Instance.MysticSlimeUnlocked = true;
            ShopButtons[2].interactable = false;
        }
    }
}
