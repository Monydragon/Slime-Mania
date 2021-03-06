using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefs : MonoBehaviour
{
    public float GameTimer = 30f;
    public float MultiplierTimer = 5f;
    public bool SlimeOverride = true;
    public float SlimeSpeed = 3f;
    public float SlimeTimeBetweenWalking = 3f;

    private void OnEnable()
    {
        EventManager.onSlimeSpawn += EventManager_onSlimeSpawn;
    }

    private void EventManager_onSlimeSpawn(GameObject value)
    {
        if (SlimeOverride)
        {
            var slime = value.GetComponent<SlimeController>();
            if (slime != null)
            {
                slime.speed = SlimeSpeed;
                slime.timeBetweenWalking = SlimeTimeBetweenWalking;
            }
        }
    }

    private void OnDisable()
    {
        EventManager.onSlimeSpawn -= EventManager_onSlimeSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.TimeToCompleteLevel = GameTimer;
        GameManager.Instance.TimeRemaining = GameTimer;
        GameManager.Instance.TimeToChangeMultiplier = MultiplierTimer;
        GameManager.Instance.TimeToMulplierChangeRemaining = MultiplierTimer;
        GameManager.Instance.MultiplierRandomize();
        GameManager.Instance.GameOver = false;

        if (GameManager.Instance.Spawner == null)
        {
            GameManager.Instance.Spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnController>();
        }

        if (GameManager.Instance.SilverSlimeUnlocked)
        {
            GameManager.Instance.Spawner.SlimePrefabs.Add(GameManager.Instance.silverSlimePrefab);
        }

        if (GameManager.Instance.GoldSlimeUnlocked)
        {
            GameManager.Instance.Spawner.SlimePrefabs.Add(GameManager.Instance.goldSlimePrefab);
        }

        if (GameManager.Instance.MysticSlimeUnlocked)
        {
            GameManager.Instance.Spawner.SlimePrefabs.Add(GameManager.Instance.mysticSlimePrefab);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
