using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] SlimePrefabs;
    public GameObject[] SpawnPoints;
    public int NumberToSpawn;
    public float SpawnDelay = 0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSlimes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnSlimes()
    {
        for (int i = 0; i < NumberToSpawn; i++)
        {
            var randSlime = SlimePrefabs[Random.Range(0, SlimePrefabs.Length)];
            var randSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            Instantiate(randSlime, randSpawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnDelay);
        }
        yield return null;
    }
}
