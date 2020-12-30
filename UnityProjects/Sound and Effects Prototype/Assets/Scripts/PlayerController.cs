using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 10;
    public float gravityModifier = 10;
    private Rigidbody _rigidbody;

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void Start()
    {
        Physics.gravity *= gravityModifier;
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
