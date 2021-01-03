using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("List of prefabs 'targets' to periodically spawn")]
    public List<GameObject> targets;

    [Header("Spawn rate in seconds")]
    public float spawnRate = 1;

    private IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(targets[Random.Range(0, targets.Count)]);
        }
    }
    
    private void Start()
    {
        StartCoroutine(SpawnTarget());
    }
}
