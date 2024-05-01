using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //single instance model 

    public static EnemyManager Instance { get; private set; } 

    [Header("enemyspawn")]
    public Transform[] spawnPoints;

    [Header("enemypatrol")]
    public Transform[] patrolPoints;

    [Header("enemies of the level")]
    public List<EnemyWave> enemyWaves;

    public int currentWaveIndex = 0;

    public int enemyCount = 0;



    public bool GetLastWave() => currentWaveIndex == enemyWaves.Count;

    private void Awake()
    {
        Instance = this; 
    }
    
    private void Update()
    {
        if (enemyCount == 0)
        {
            StartCoroutine(nameof(startNextWaveCoroutine));
        }
    }

    IEnumerator startNextWaveCoroutine()
    {
         if ( currentWaveIndex >= enemyWaves.Count )
        {
            yield break;
        }
        List<EnemyData> enemies = enemyWaves[currentWaveIndex].enemies;
        foreach(EnemyData enemyData in enemies)
        {
            for (int i = 0; i < enemyData.waveEnemyCount; i++)
            {
                GameObject enemy = Instantiate(enemyData.enemyPreferb, GetRandomSpawnPoint(), Quaternion.identity);

                if (patrolPoints != null )
                {
                    enemy.GetComponent<Enemy>().patrolPoints = patrolPoints;
                }

                yield return new WaitForSeconds(enemyData.spawnInrterval);
            }
        }

        currentWaveIndex++;
    }
    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }

 
}

   

[System.Serializable]
public class EnemyData
{
    public GameObject enemyPreferb;
    public float spawnInrterval;
    public float waveEnemyCount;
}

[System.Serializable]
public class EnemyWave
{
    public List<EnemyData> enemies;

}