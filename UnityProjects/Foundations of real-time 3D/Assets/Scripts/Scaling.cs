using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    [Header("Scale")]
    public Vector3 scaleBy;
    public Vector3 scaleLimit = new Vector3(10, 10, 10);
    [Header("Rotation")]
    public Vector3 rotateBy;

    private void Update()
    {
        if (transform.localScale.sqrMagnitude <= scaleLimit.sqrMagnitude)
        {
            transform.localScale += scaleBy;
        }
        transform.Rotate(rotateBy);
    }
}
