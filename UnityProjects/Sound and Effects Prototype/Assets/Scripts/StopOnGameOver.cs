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
        if (_playerController.gameOver && _moveConstantly.canMove) _moveConstantly.canMove = false;
        if (_playerController.gameOver && _obstacleSpawner.canSpawn) _obstacleSpawner.canSpawn = false;
    }
}
