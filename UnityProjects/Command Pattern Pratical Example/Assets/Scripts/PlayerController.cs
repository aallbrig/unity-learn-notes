using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Transform _transform;
    private bool _disableInput;

    private void Start()
    {
        _transform = transform;
        CommandManager.OnRewindStart += () => _disableInput = true;
        CommandManager.OnRewindComplete += () => _disableInput = false;
    }

    private void Update()
    {
        if (_disableInput) return;

        var verticalAxis = Input.GetAxis("Vertical");
        var horizontalAxis = Input.GetAxis("Horizontal");

        if (verticalAxis > 0)
            CommandManager.Instance.AddAndExecute(new MoveUpCommand(_transform, _speed));
        else if (verticalAxis < 0)
            CommandManager.Instance.AddAndExecute(new MoveDownCommand(_transform, _speed));

        if (horizontalAxis > 0)
            CommandManager.Instance.AddAndExecute(new MoveRightCommand(_transform, _speed));
        else if (horizontalAxis < 0)
            CommandManager.Instance.AddAndExecute(new MoveLeftCommand(_transform, _speed));
    }
}
