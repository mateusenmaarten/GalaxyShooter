using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerUp;

    private bool _stopSpawning;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine(5));
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine(int waitTime)
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemy, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            float randomSpawnTime = Random.Range(10, 20);
            int randomPowerUp = Random.Range(0,3);

            Instantiate(_powerUp[randomPowerUp], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(randomSpawnTime);
        }
        
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
