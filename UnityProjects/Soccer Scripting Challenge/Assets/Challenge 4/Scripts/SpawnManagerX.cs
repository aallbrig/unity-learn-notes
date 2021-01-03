using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private const float SpawnRangeX = 10;
    private const float SpawnZMin = 15;
    private const float SpawnZMax = 25;

    public int enemyCount;
    public int waveCount = 1;
    public GameObject player;
    private readonly Vector3 _playerResetPosition = new Vector3(0, 1, -7);
    private readonly Vector3 _powerupSpawnOffset = new Vector3(0, 0, -15);

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn a powerup only if none remain
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
            Instantiate(powerupPrefab, GenerateSpawnPosition() + _powerupSpawnOffset, powerupPrefab.transform.rotation);

        // Spawn number of enemy balls based on wave number
        for (var i = 0; i < enemiesToSpawn; i++)
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);

        waveCount++;
        ResetPlayerPosition(); // put player back at start
    }

    // Move player back to position in front of own goal
    private void ResetPlayerPosition ()
    {
        player.transform.position = _playerResetPosition;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        if (enemyCount == 0) SpawnEnemyWave(waveCount);
    }

    // Generate random spawn position for powerups and enemy balls
    private static Vector3 GenerateSpawnPosition ()
    {
        return new Vector3(Random.Range(-SpawnRangeX, SpawnRangeX), 0, Random.Range(SpawnZMin, SpawnZMax));
    }
}
