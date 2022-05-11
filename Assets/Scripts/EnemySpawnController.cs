using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on the spawn controller made in the asynchronous lectures
// Minor changes for the project
// i.e. instead of one enemyPrefab, there will be an array of them for the different types of enemies
// and instead of changing the material here, each enemy will change it's material depending on what kind of behaviour it is exhibiting

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private float spawnTimer = 0.0f;
    private float spawnClock = 2.5f;

    public Transform[] spawnPoints;
    private static int EnemyCount;
    public int Max_Enemies = 6;



    // Start is called before the first frame update
    void Start()
    {
        EnemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnClock && EnemyCount < Max_Enemies)
        {
            int spawnLocation = Random.Range(0, spawnPoints.Length);
            //GameObject zombie = Instantiate(enemyPrefab, spawnPoints[spawnLocation].position, Quaternion.identity);
            //zombie.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = zombieSkins[Random.Range(0, zombieSkins.Length)];
            int enemyType = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyType], spawnPoints[spawnLocation].position, Quaternion.identity);
            spawnTimer = 0.0f;
            EnemyCount++;
        }
    }

    public static void RemoveAllEnemies()
    {
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(zombie);
        }
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy Type A"))
        {
            Destroy(enemy);
        }
        EnemyCount = 0;
        Score.ResetPoints();
    }

    public static void ReduceEnemyCount()
    {
        EnemyCount--;
    }
}
