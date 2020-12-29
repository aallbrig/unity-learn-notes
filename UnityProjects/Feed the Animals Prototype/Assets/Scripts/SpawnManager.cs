using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public KeyCode spawnKey = KeyCode.S;
    public bool spawnKeyEnabled;
    public GameObject animal;
    public float spawnRangeX = 20;

    private void Update()
    {
        if (spawnKeyEnabled && Input.GetKeyDown(spawnKey))
        {
            Instantiate(
                animal,
                transform.position + new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, 0),
                transform.rotation
            );
        }
    }
}
