using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bigEnemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject[] powerupPrefabs;
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;
    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        SpawnEnemyWave(waveNumber);
        
      
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            Instantiate(bigEnemyPrefab, GenerateSpawnPosition(), bigEnemyPrefab.transform.rotation);

        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3 (spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        { 
            waveNumber ++;
            if (waveNumber % bossRound == 0) 
            { 
                SpawnBossWave(waveNumber);
            } 
            else 
            { 
                SpawnEnemyWave(waveNumber);
            }

            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        }

        
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;  
        if (bossRound != 0)
        { 
            miniEnemysToSpawn = currentRound / bossRound; 
        } 
        else
        { 
            miniEnemysToSpawn = 1; 
        } 

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(),
            bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }


    public void SpawnMiniEnemy(int amount)
    { 
        for (int i = 0; i < amount; i++) 
        { 
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(),
                miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }



}
