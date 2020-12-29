using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    public KeyCode changeColorKey = KeyCode.Space;
    [SerializeField]
    private float _upRotationSpeed = 10.0f;
    private float _rightRotationSpeed = 0.0f;
    private float _rotationSpeedMin = -100f;
    private float _rotationSpeedMax = 100f;

    private void RandomColor()
    {
        Renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), 0.3f, Random.Range(0.5f, 0.9f));
    }
    private void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;

        RandomColor();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(changeColorKey)) RandomColor();

        _upRotationSpeed = _upRotationSpeed + Input.GetAxis("Vertical");
        if (_upRotationSpeed > _rotationSpeedMax) _upRotationSpeed = _rotationSpeedMax;
        if (_upRotationSpeed < _rotationSpeedMin) _upRotationSpeed = _rotationSpeedMin;

        _rightRotationSpeed = _rightRotationSpeed + Input.GetAxis("Horizontal");
        if (_rightRotationSpeed > _rotationSpeedMax) _rightRotationSpeed = _rotationSpeedMax;
        if (_rightRotationSpeed < _rotationSpeedMin) _rightRotationSpeed = _rotationSpeedMin;

        transform.Rotate(_upRotationSpeed * Time.deltaTime, _rightRotationSpeed * Time.deltaTime, 0.0f);
    }
}
