using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] SlimePrefabs;
    public GameObject[] SpawnPoints;
    public GameObject RootGameobject;
    public int NumberToSpawn;
    public float SpawnDelay = 0f;
    public int RemainingToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        RemainingToSpawn = NumberToSpawn;
        StartCoroutine(SpawnSlimes());
    }

    public IEnumerator SpawnSlimes()
    {
        GameManager.Instance.TotalSlimesSpawned = NumberToSpawn;
        for (int i = 0; i < NumberToSpawn; i++)
        {
            var randSlime = SlimePrefabs[Random.Range(0, SlimePrefabs.Length)];
            var randSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            var spawnedSlime = Instantiate(randSlime, randSpawnPoint.transform.position, Quaternion.identity, RootGameobject.transform);
            EventManager.SpawnSlime(spawnedSlime);
            RemainingToSpawn--;
            GameManager.Instance.TotalSlimesRemainingToSpawn = RemainingToSpawn;
            yield return new WaitForSeconds(SpawnDelay);
        }
        yield return null;
    }
}
