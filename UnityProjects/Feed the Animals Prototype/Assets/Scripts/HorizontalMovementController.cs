using System;
using UnityEngine;
using UnityEngine.WSA;

public class HorizontalMovementController : MonoBehaviour
{
    [Header("Speed (in meters per second)")]
    public float speed = 35;
    public float xBounds = 22;
    public float zBounds = 10;

    private void Update()
    {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        
        var pos = transform.position;
        if (pos.x < -xBounds)
        {
            transform.position = new Vector3(-xBounds, pos.y, pos.z);
        } else if (pos.x > xBounds)
        {
            transform.position = new Vector3(xBounds, pos.y, pos.z);
        }

        if (pos.z < -zBounds)
        {
            transform.position = new Vector3(transform.position.x, pos.y, -zBounds);
        } else if (pos.z > zBounds)
        {
            transform.position = new Vector3(transform.position.x, pos.y, zBounds);
        }
    }
}
