using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    public GameObject player;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var lookDirection = (player.transform.position - transform.position).normalized;

        _rigidbody.AddForce(lookDirection * speed);
    }
}
