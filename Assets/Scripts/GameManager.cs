using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int Score;
    public float TimeRemaining = 30f;
    public float TimeToMulplierChangeRemaining = 5f;
    public float TimeToCompleteLevel = 30f;
    public float TimeToChangeMultiplier = 5f;
    public int RemainingSlimes;
    public bool GameOver;
    public int StandardMultiplier = 1;
    public SlimeType SlimeTypeMultiplier;
    public int MultiplierForSlimeType = 2;
    public int minMutliplierMod = 2;
    public int maxMutliplierMod = 6;

    private void OnEnable()
    {
        EventManager.onMultiplierChange += EventManager_onMultiplierChange;
        EventManager.onGameReset += EventManager_onGameReset;
        EventManager.onLoadNextLevel += EventManager_onLoadNextLevel;
    }

    private void EventManager_onLoadNextLevel()
    {
        TimeRemaining = 30f;
        TimeToMulplierChangeRemaining = 5f;
        GameOver = false;
        EventManager.ScoreChanged();
        EventManager.TimeChanged();
    }

    private void EventManager_onGameReset()
    {
        Score = 0;
        TimeRemaining = 30f;
        TimeToMulplierChangeRemaining = 5f;
        GameOver = false;
        EventManager.ScoreChanged();
        EventManager.TimeChanged();
    }

    private void EventManager_onMultiplierChange(SlimeType type, int value)
    {
        SlimeTypeMultiplier = type;
        MultiplierForSlimeType = value;
    }

    private void OnDisable()
    {
        EventManager.onMultiplierChange -= EventManager_onMultiplierChange;
        EventManager.onGameReset -= EventManager_onGameReset;
        EventManager.onLoadNextLevel -= EventManager_onLoadNextLevel;

    }

    private void Awake()
    {
        if (instance == null)

        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeRemaining = TimeToCompleteLevel;
        TimeToMulplierChangeRemaining = TimeToChangeMultiplier;
        MultiplierRandomize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOver)
        {
            RemainingSlimes = GameObject.FindGameObjectsWithTag("Slime").Length;
            TimeRemaining -= Time.deltaTime;
            EventManager.TimeChanged();
            TimeToMulplierChangeRemaining -= Time.deltaTime;
            if (TimeRemaining < 0)
            {
                GameOver = true;
                Debug.Log("Game Over");
                if(RemainingSlimes <= 0)
                {
                    EventManager.LevelComplete();
                }
                else
                {
                    EventManager.LevelFail();
                }
            }

            if(TimeToMulplierChangeRemaining <= 0)
            {
                MultiplierRandomize();
                TimeToMulplierChangeRemaining = TimeToChangeMultiplier;
            }
        }

    }

    public void MultiplierRandomize()
    {
        var ranEnumVal = UnityEngine.Random.Range(0, Enum.GetNames(typeof(SlimeType)).Length);
        SlimeType ranSlimeType = (SlimeType)ranEnumVal;
        EventManager.MultiplierChange(ranSlimeType, UnityEngine.Random.Range(minMutliplierMod, maxMutliplierMod));
    }
}
