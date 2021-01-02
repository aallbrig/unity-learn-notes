using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private const float SpawnRange = 9;
    private int _spawnWave = 0;
    private int _enemyCount;

    private static Vector3 GenerateSpawnPosition()
    {
        return new Vector3(Random.Range(-SpawnRange, SpawnRange), 0, Random.Range(-SpawnRange, SpawnRange));
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), transform.rotation);
    }

    private void SpawnEnemyWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++) SpawnEnemy();
    }
    private void Start()
    {
        SpawnEnemyWave(_spawnWave++);
    }

    private void Update()
    {
        _enemyCount = FindObjectsOfType<Enemy>().Length;

        if (_enemyCount == 0) SpawnEnemyWave(_spawnWave++);
    }
}
