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
       Destroy(gameObject);
       Instantiate(explosionParticles[Random.Range(0, explosionParticles.Count)], transform.position, transform.rotation);
       _gameManager.UpdateScore(pointRewards);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
