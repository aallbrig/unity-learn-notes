using UnityEngine;

public class MoveRightCommand : ICommand
{
    private readonly Transform _player;
    private readonly float _playerSpeed;

    public MoveRightCommand(Transform player, float speed)
    {
        _player = player;
        _playerSpeed = speed;
    }

    public void Execute()
    {
        _player.Translate(Vector3.right * _playerSpeed * Time.deltaTime);
    }

    public void Undo()
    {
        _player.Translate(Vector3.left * _playerSpeed * Time.deltaTime);
    }
}
