using UnityEngine;

public class StopOnGameOver : MonoBehaviour
{
    private PlayerController _playerController;
    private MoveConstantly _moveConstantly;
    private ObstacleSpawner _obstacleSpawner;

    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _obstacleSpawner = GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>();
        _moveConstantly = GetComponent<MoveConstantly>();
    }

    private void Update()
    {
        if (!_playerController.gameOver) return;
        if (_moveConstantly.canMove) _moveConstantly.canMove = false;
        if (_obstacleSpawner.canSpawn) _obstacleSpawner.canSpawn = false;
        var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var go in obstacles) Destroy(go);
    }
}
