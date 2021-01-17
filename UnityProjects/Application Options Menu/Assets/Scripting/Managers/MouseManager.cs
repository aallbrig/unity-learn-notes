using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : Singleton<MouseManager>
{
    public EventVector3 OnClickEnvironment;
    public EventVector3 OnEnqueueClickEnvironment;
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
                (Input.GetKey(KeyCode.LeftShift) ? OnEnqueueClickEnvironment : OnClickEnvironment)?.Invoke(hit.point);
            }
        }
    }
}