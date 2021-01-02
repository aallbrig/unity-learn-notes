using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public bool hasPowerup = false;
    private float _powerupStrength = 15;
    private Rigidbody _rigidbody;
    private GameObject _focalPoint;

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        _rigidbody.AddForce(Input.GetAxis("Vertical") * _focalPoint.transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (hasPowerup && other.gameObject.CompareTag("Enemy"))
        {
            var enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = (other.gameObject.transform.position - transform.position).normalized;

            enemyRigidbody.AddForce(awayFromPlayer * _powerupStrength, ForceMode.Impulse);
        }
    }
}
