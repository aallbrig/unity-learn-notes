using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveForward : MonoBehaviour
{
    public Vector3 vector = new Vector3(0, 0, 20);

    private void Update()
    {
        transform.localPosition += vector * Time.deltaTime;
    }
}
