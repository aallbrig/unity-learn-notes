using System.Collections.Generic;
using UnityEngine;

public class UserClick : MonoBehaviour
{
    private Camera _camera;
    private List<ICommand> actionQueue = new List<ICommand>();
    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.CompareTag("Cube"))
                {
                    var cube = hit.collider.gameObject;
                    var randomColor = new Color(Random.value, Random.value, Random.value);
                    var click = new ClickCommand(cube, randomColor);
                    actionQueue.Add(click);
                    click.Execute();
                }
            }
        }
    }
}
