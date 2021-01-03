using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private const float Speed = 500;
    private GameObject _focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public ParticleSystem smokeParticles;
    public int powerUpDuration = 5;

    private const float NormalStrength = 10; // how hard to hit enemy without powerup
    private const float PowerupStrength = 25; // how hard to hit enemy with powerup
    private const float SpacebarMultiplier = 4;
    
    // Coroutine to count down powerup duration
    private IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }


    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        smokeParticles.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        var verticalInput = Input.GetAxis("Vertical");
        var forwardMovement = _focalPoint.transform.forward *  Speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerRigidbody.AddForce(forwardMovement * SpacebarMultiplier, ForceMode.Impulse);
            smokeParticles.Play();
        }
        else
        {
            _playerRigidbody.AddForce(forwardMovement * verticalInput);
        }
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = (other.gameObject.transform.position - transform.position).normalized;
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * PowerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * NormalStrength, ForceMode.Impulse);
            }
        }
    }
}
