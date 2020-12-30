using System;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 10;
    public float gravityModifier = 10;
    public bool gameOver;
    private Rigidbody _rigidbody;
    private bool _isOnGround = true;

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _isOnGround = false;
    }
    private void Start()
    {
        Physics.gravity *= gravityModifier;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!gameOver && _isOnGround && Input.GetKeyDown(jumpKey)) Jump();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        } else if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
        }
    }
}
