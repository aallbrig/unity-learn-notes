using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public Vector3 rotateBy;

    private void Update()
    {
        transform.Rotate(rotateBy);
    }
}
