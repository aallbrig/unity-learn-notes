using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce = 5;
    public float gravityModifier = 1.5f;
    private Rigidbody _playerRigidbody;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    private float _upperBounds = 15;
    private float _lowerBounds = 2;


    private void Jump()
    {
        _playerRigidbody.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
    }

    private void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameOver) return;
        var isPlayerBelowGround = transform.position.y < _lowerBounds;
        var isPlayerAboveViewport = transform.position.y > _upperBounds;
        if (isPlayerBelowGround || (Input.GetKey(KeyCode.Space) && !isPlayerAboveViewport)) Jump();
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
    }
}
