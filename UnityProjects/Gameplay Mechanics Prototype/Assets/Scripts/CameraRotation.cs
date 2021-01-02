using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(
            Vector3.up,
            Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime
        );
    }
}
