using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public KeyCode spawnKey = KeyCode.S;
    public bool spawnKeyEnabled;
    public GameObject animal;
    public float spawnRangeX = 20;
    [Header("Spawn interval time (in seconds)")]
    public float spawnInterval = 1.5f;
    public bool spawnTimeEnabled = true;

    private float _timeSinceSpawn;
    
    private void SpawnAnimal()
    {
        Instantiate(
            animal,
            transform.position + new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, 0),
            transform.rotation
        );
    }

    private void Start()
    {
        _timeSinceSpawn = Time.time;
    }

    private void Update()
    {
        var spawnFromKey = spawnKeyEnabled && Input.GetKeyDown(spawnKey);
        var spawnFromTimer = spawnTimeEnabled && (Time.time - _timeSinceSpawn) > spawnInterval;
        
        if (spawnFromKey) SpawnAnimal();
        if (spawnFromTimer)
        {
            SpawnAnimal();
            _timeSinceSpawn = Time.time;
        }
    }
}
