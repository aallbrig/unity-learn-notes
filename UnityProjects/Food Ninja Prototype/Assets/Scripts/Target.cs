using UnityEngine;

public class Target : MonoBehaviour
{
    private const int PointRewards = 1;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
       Destroy(gameObject);
       _gameManager.UpdateScore(PointRewards);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
