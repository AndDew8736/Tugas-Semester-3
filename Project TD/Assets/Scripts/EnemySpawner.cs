using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    //game object array because theres multiple enemy types
    [Header("Stats")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScaling = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    public int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private void Start()
    {
        StartCoroutine(StartWave());
    }
    void Update()
    {
        if (!isSpawning)
            return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0;
            //spawns enemies if theres enemies left to spawn
        }
        if (enemiesAlive <= 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }
    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        //sets the enemy type to spawn
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        //generates an enemy using the start point in level manager script and quaternion for the rotation
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScaling));
        //automatically increases the amount of enemies per wave
    }
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }
    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    // Update is called once per frame

}
