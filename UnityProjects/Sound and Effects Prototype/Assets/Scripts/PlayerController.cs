using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Golly, this script seems real busy. IMO, after this, I'll try to decompose these types of scripts more
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce = 10;
    public float gravityModifier = 10;
    public bool gameOver;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtSplatter;
    public AudioClip jumpSound;
    public AudioClip dieSound;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private AudioSource _audioPlayer;
    private bool _isOnGround = true;
    private static readonly int DeathB = Animator.StringToHash("Death_b");
    private static readonly int DeathTypeINT = Animator.StringToHash("DeathType_int");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");

    private void Jump()
    {
        dirtSplatter.Stop();
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _isOnGround = false;
        _animator.SetTrigger(JumpTrig);
        _audioPlayer.PlayOneShot(jumpSound, 1f);
    }
    private void Start()
    {
        Physics.gravity *= gravityModifier;
        _rigidbody = GetComponent<Rigidbody>();
        var activePrefab = GetComponent<RandomizePrefab>().GetActivePrefab();
        _animator = activePrefab.GetComponent<Animator>();
        _audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gameOver && _isOnGround && Input.GetKeyDown(jumpKey)) Jump();
        // Ensure the player doesn't tip over
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!gameOver && other.gameObject.CompareTag("Ground"))
        {
            dirtSplatter.Play();
            _isOnGround = true;
        } else if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            _animator.SetBool(DeathB, true);
            _animator.SetInteger(DeathTypeINT, 1);
            dirtSplatter.Stop();
            explosionParticle.Play();
            _audioPlayer.PlayOneShot(dieSound, 1f);
        }
    }
}
