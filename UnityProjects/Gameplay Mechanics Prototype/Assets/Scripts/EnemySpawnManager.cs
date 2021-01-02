using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float _spawnRange = 9;
    private int _spawnWave = 1;

    private Vector3 GenerateSpawnPosition()
    {
        return new Vector3(
            Random.Range(-_spawnRange, _spawnRange),
            0,
            Random.Range(-_spawnRange, _spawnRange)
        );
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < _spawnWave; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), transform.rotation);
        }

        _spawnWave++;
    }
    private void Start()
    {
        SpawnEnemy();
    }
}
