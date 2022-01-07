using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefs : MonoBehaviour
{
    public float GameTimer = 30f;
    public float MultiplierTimer = 5f;
    public float SlimeSpeed = 3f;
    public int SlimeBaseScore = 1;
    public float SlimeTimeBetweenWalking = 3f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.TimeToCompleteLevel = GameTimer;
        GameManager.instance.TimeRemaining = GameTimer;
        GameManager.instance.TimeToChangeMultiplier = MultiplierTimer;
        GameManager.instance.TimeToMulplierChangeRemaining = MultiplierTimer;
    }

    // Update is called once per frame
    void Update()
    {
        var slimes = GameObject.FindGameObjectsWithTag("Slime");

        for (int i = 0; i < slimes.Length; i++)
        {
            var slime = slimes[i].GetComponent<SlimeController>();
            slime.speed = SlimeSpeed;
            slime.baseScore = SlimeBaseScore;
            slime.timeBetweenWalking = SlimeTimeBetweenWalking;
        }
    }
}
