using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [Header("Speed is in meters per second")]
    public float speed = 40;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
