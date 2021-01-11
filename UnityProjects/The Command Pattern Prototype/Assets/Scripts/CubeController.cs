using UnityEngine;
using UnityEngine.PlayerLoop;

public class CubeController : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _startingColor;

    private void ResetCube()
    {
        _meshRenderer.material.color = _startingColor;
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _startingColor = _meshRenderer.material.color;

        CommandManager.OnDoneBehavior += ResetCube;
    }
}
