using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private const float SpawnDelay = 2;
    private const float SpawnInterval = 1.5f;
    private PlayerControllerX _playerControllerScript;

    private void SpawnObjects ()
    {
        // Set random spawn location and random object index
        var spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        var randomIndex = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!_playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[randomIndex], spawnLocation, objectPrefabs[randomIndex].transform.rotation);
        }

    }
    private void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        InvokeRepeating("SpawnObjects", SpawnDelay, SpawnInterval);
    }

}
