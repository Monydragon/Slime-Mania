using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Coins;
    public float TimeRemaining = 30f;
    public float TimeToMulplierChangeRemaining = 5f;
    public float TimeToCompleteLevel = 30f;
    public float TimeToChangeMultiplier = 5f;
    public int RemainingSlimes;
    public bool GameOver;
    public int StandardMultiplier = 1;
    public SlimeType SlimeTypeMultiplier;
    public int MultiplierForSlimeType = 2;
    public int MinMultiplierMod = 2;
    public int MaxMultiplierMod = 6;
    public List<Transform> SlimeTransforms = new List<Transform>();
    public int TotalSlimesSpawned;
    public int TotalSlimesRemainingToSpawn;
    public GameState State = GameState.Menu;
    public AudioClip LevelCompleteAudioClip;
    public AudioClip LevelFailedAudioClip;

    public enum GameState
    {
        Menu,
        Playing
    }

    private void OnEnable()
    {
        EventManager.onMultiplierChange += EventManager_onMultiplierChange;
        EventManager.onGameReset += EventManager_onGameReset;
        EventManager.onLoadNextLevel += EventManager_onLoadNextLevel;
        EventManager.onSlimeSpawn += EventManager_onSlimeSpawn;
        EventManager.onSlimeConvert += EventManager_onSlimeConvert;
        EventManager.onSlimeDestroy += EventManager_onSlimeDestroy;
    }

    private void EventManager_onSlimeDestroy(GameObject value)
    {
        if(value != null)
        {
            SlimeTransforms.Remove(value.transform);
            Destroy(value.gameObject);
        }
    }

    private void EventManager_onSlimeConvert(GameObject value)
    {
        SlimeTransforms.Remove(value.transform);
    }

    private void EventManager_onSlimeSpawn(GameObject value)
    {
        SlimeTransforms.Add(value.transform);
    }

    private void EventManager_onLoadNextLevel()
    {
        TimeRemaining = 30f;
        TimeToMulplierChangeRemaining = 5f;
        GameOver = false;
        SlimeTransforms.Clear();
        EventManager.CoinsChanged();
        EventManager.TimeChanged();
        State = GameState.Playing;
    }

    private void EventManager_onGameReset()
    {
        Coins = 0;
        TimeRemaining = 30f;
        TimeToMulplierChangeRemaining = 5f;
        GameOver = false;
        SlimeTransforms.Clear();
        EventManager.CoinsChanged();
        EventManager.TimeChanged();
        State= GameState.Playing;
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
        EventManager.onSlimeSpawn -= EventManager_onSlimeSpawn;
        EventManager.onSlimeConvert -= EventManager_onSlimeConvert;
        EventManager.onSlimeDestroy -= EventManager_onSlimeDestroy;

    }

    private void Awake()
    {
        if (Instance == null)

        {
            Instance = this;
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
        switch (State)
        {
            case GameState.Menu:
                break;
            case GameState.Playing:
                if (!GameOver)
                {
                    RemainingSlimes = GameObject.FindGameObjectsWithTag("Slime").Length;
                    TimeRemaining -= Time.deltaTime;
                    EventManager.TimeChanged();
                    TimeToMulplierChangeRemaining -= Time.deltaTime;

                    if (RemainingSlimes <= 0 && TotalSlimesRemainingToSpawn <= 0)
                    {
                        GameOver = true;
                        AudioManager.Instance.Play(LevelCompleteAudioClip, transform);
                        EventManager.LevelComplete();
                    }

                    if (TimeRemaining <= 0)
                    {
                        GameOver = true;
                        if (RemainingSlimes > 0)
                        {
                            AudioManager.Instance.Play(LevelFailedAudioClip, transform);
                            EventManager.LevelFail();
                        }
                    }

                    if (TimeToMulplierChangeRemaining <= 0)
                    {
                        MultiplierRandomize();
                        TimeToMulplierChangeRemaining = TimeToChangeMultiplier;
                    }
                }
                break;
        }
    }

    public void MultiplierRandomize()
    {
        var ranEnumVal = UnityEngine.Random.Range(0, Enum.GetNames(typeof(SlimeType)).Length);
        SlimeType ranSlimeType = (SlimeType)ranEnumVal;
        EventManager.MultiplierChange(ranSlimeType, UnityEngine.Random.Range(MinMultiplierMod, MaxMultiplierMod));
    }
}
