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
        _rigidbody.AddForce(
            (player.transform.position - transform.position).normalized * speed
        );
    }
}
