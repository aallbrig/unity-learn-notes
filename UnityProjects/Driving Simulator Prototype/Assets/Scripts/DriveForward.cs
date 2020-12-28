using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveForward : MonoBehaviour
{
    public Vector3 vector = new Vector3(0, 0, 30);

    private void Update()
    {
        transform.Translate(vector * Time.deltaTime);
    }
}
