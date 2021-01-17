using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : Singleton<MouseManager>
{
    public EventVector3 OnClickEnvironment;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        // Player right clicks
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, 50))
            {
                Debug.LogWarning(hit);
                OnClickEnvironment?.Invoke(hit.point);
            }
            else
            {
                Debug.LogWarning("no hit");
            }
        }
    }
}