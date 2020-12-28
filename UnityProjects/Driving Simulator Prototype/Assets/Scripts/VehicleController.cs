using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Speed in meters per second")]
    public float speed = 10;

    public float turnSpeed = 10;

    private void Update()
    {
        transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * speed);
        transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * Time.deltaTime * turnSpeed);
    }
}
