using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject spherePrefab;
    public TMP_Text counterText;
    private int _count = 0;

    private void UpdateCount()
    {
        counterText.text = "Counter : " + ++_count;
    }
    private void SpawnSphere()
    {
        var sphere = Instantiate(spherePrefab, spawnPoint.transform);
        sphere.transform.position += new Vector3(
            0,
            0,
            Random.Range(-10, 10)
        );

        var sphereRigidbody = sphere.GetComponent<Rigidbody>();
        sphereRigidbody.AddForce(Vector3.up * Random.Range(0, 10), ForceMode.Impulse);
        sphereRigidbody.AddTorque(Vector3.up * Random.Range(0, 10), ForceMode.Impulse);
    }

    private IEnumerator SpawnSpheres()
    {
        while (true)
        {
            SpawnSphere();
            yield return new WaitForSecondsRealtime(Random.Range(0, 1.25f));
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnSpheres());
        Counter.OnObjectInBox += UpdateCount;
    }
}
