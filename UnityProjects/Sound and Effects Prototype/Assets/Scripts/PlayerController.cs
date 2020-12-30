using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 100;
    private Rigidbody _rigidbody;

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce);
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Jump();
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }
    }
}
