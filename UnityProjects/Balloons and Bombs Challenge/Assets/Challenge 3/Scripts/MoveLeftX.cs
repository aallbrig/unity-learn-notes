using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    public float speed;
    public float leftBound = -10;
    private PlayerControllerX _playerControllerScript;

    private void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    private void Update()
    {
        if (!_playerControllerScript.gameOver)
          transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
          Destroy(gameObject);
    }
}
