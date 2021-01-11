using UnityEngine;

public class MoveUpCommand : ICommand
{
    private readonly Transform _player;
    private readonly float _playerSpeed;

    public MoveUpCommand(Transform player, float speed)
    {
        _player = player;
        _playerSpeed = speed;
    }

    public void Execute()
    {
        _player.Translate(Vector3.up * _playerSpeed * Time.deltaTime);
    }

    public void Undo()
    {
        _player.Translate(Vector3.down * _playerSpeed * Time.deltaTime);
    }
}
