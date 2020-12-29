using UnityEngine;

public class ToggleCameraPerspective : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.E;
    public bool toggleKeyEnabled;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (toggleKeyEnabled && Input.GetKeyDown(toggleKey))
        {
            _camera.orthographic = !_camera.orthographic;
        }
    }
}
