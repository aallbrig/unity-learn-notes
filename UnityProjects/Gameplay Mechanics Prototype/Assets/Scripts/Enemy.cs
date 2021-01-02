using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    private GameObject _player;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        var lookDirection = (_player.transform.position - transform.position).normalized;

        _rigidbody.AddForce(lookDirection * speed);
    }
}
