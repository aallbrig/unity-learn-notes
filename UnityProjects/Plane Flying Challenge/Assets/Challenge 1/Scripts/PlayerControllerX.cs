using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private void Update()
    {
        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Input.GetAxis("Vertical") * Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
