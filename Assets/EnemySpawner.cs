using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int maxNumEnemiesInScene = 4;

    void Update()
    {
        int numEnemies = GetComponentsInChildren<Enemy>().Length;
        if (numEnemies < maxNumEnemiesInScene)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length - 1);
            Vector3 randomPos = new Vector3(Random.Range(30f, 150f), 0, Random.Range(30f, 150f)); 
            GameObject randomEnemy = Instantiate(enemyPrefabs[randomIndex], transform);
            randomEnemy.transform.position = randomPos;
        }
    }
}
