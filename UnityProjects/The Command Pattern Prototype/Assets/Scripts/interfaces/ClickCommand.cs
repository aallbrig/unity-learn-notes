using UnityEngine;

public class ClickCommand : ICommand
{
    private readonly GameObject _cube;
    private readonly Color _color;
    private readonly MeshRenderer _meshRenderer;
    private Color _previousColor;

    public ClickCommand(GameObject cube, Color color)
    {
        _cube = cube;
        _color = color;
        _meshRenderer = _cube.GetComponent<MeshRenderer>();
    }

    public void Execute()
    {
        _previousColor = _meshRenderer.material.color;
        _meshRenderer.material.color = _color;
    }

    public void Undo()
    {
        _meshRenderer.material.color = _previousColor;
    }
}
