using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int totalEnemiesToSpawn = 4;
    public float spawnTime = 1f; 

    private int spawnedEnemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedEnemyCount < totalEnemiesToSpawn)
        {
            SpawnEnemy();
            spawnedEnemyCount++;

            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

       
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.SetSpawner(this);
        }
    }

   
    public void DecreaseSpawnedEnemyCount()
    {
        spawnedEnemyCount--;
    }
}
