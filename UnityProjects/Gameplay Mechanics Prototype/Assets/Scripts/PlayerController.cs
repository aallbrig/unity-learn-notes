using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.AddForce(Input.GetAxis("Vertical") * Vector3.forward * speed);
    }
}
