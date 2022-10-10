using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _asteroidContainer;
    [SerializeField]
    private GameObject _asteroidPrefab;
    [SerializeField]
    private float spawningRate = 2.0f;
    private float _asteroidSpawningRate = 10.0f;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerUPs;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(spawnPowerUPs());
        StartCoroutine(SpawnAsteroid());
    }

    //spawn enemy every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield Events
    //While Loop

    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 10.0f, 0.0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(spawningRate);
        }
    }

    IEnumerator spawnPowerUPs()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(3);
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 10.0f, 0.0f);
            Instantiate(_powerUPs[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(spawningRate);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

    IEnumerator SpawnAsteroid()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(_asteroidSpawningRate);
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 10.0f, 0.0f);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, posToSpawn, Quaternion.identity);
            newAsteroid.transform.parent = _asteroidContainer.transform;
            yield return new WaitForSeconds(_asteroidSpawningRate);
        }
    }
}
