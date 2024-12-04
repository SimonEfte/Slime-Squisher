using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnSlimes : MonoBehaviour
{
    public static int slimesSquished;
    public static int slimesWaveSpawnCount;
    public static bool isWaveCompleted;
    public static float slimeSpeed;

    public static float slimeHealth;

    public static float slimeSpawnTime;

    public static int slimeWave;

    void Start()
    {
        slimeWave = 1;
        slimeHealth = 1;
        slimeSpeed = 1;
        slimesWaveSpawnCount = 5;
        slimeSpawnTime = 1;
        StartCoroutine(WaitForSpawn());
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(slimeSpawnTime);
        SpawnSlime();
    }

    public static bool allSlimesSpawned;
    public static int slimesSpawned;

    public void SpawnSlime()
    {
        if(isWaveCompleted == false && allSlimesSpawned == false) 
        {
            slimesSpawned += 1;
            if(slimesSpawned >= slimesWaveSpawnCount) { allSlimesSpawned = true; }

            GameObject slime = ObjectPool.instance.GetSlime1FromPool(); 
        }
        StartCoroutine(WaitForSpawn());
    }
   
}
