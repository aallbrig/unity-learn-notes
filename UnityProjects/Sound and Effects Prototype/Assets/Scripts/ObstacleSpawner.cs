using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public float startDelay = 2;
    public float repeatRate = 2;

    private void SpawnObstacle()
    {
        Instantiate(obstacle, transform.position, transform.rotation);
    }

    private void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }
}
