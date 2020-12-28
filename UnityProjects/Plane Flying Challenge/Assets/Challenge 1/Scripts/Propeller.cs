using UnityEngine;

public class Propeller : MonoBehaviour
{
    [Header("Propeller spin speed (in rotations per second)")]
    public float spinSpeed = 10;

    private void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed * 360 * Time.deltaTime);
    }
}
