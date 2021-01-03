using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> explosionParticles;
    [SerializeField]
    private int pointRewards = 1;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        // Clicking is only enabled when the game is not over
        if (_gameManager.gameOver) return;

        Destroy(gameObject);
       // Play a random explosion particle FX (if applicable)
       if (explosionParticles.Count > 0)
           Instantiate(explosionParticles[Random.Range(0, explosionParticles.Count)], transform.position, transform.rotation);
       _gameManager.UpdateScore(pointRewards);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        // Game over if the player doesn't destroy the "good" targets before they fall
        if (!gameObject.CompareTag("Bad"))
            _gameManager.GameOver();
    }
}
