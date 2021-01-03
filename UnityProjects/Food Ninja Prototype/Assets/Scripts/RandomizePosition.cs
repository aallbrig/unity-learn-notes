using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    private const float XRange = 4;
    private const float YStart = -1;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-XRange, XRange), YStart);
    }
}
