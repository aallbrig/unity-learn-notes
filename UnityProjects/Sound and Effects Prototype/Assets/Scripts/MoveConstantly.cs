using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConstantly : MonoBehaviour
{
    [Header("Vector is in meters per second")]
    public Vector3 vector;
    public float speed = 30;

    private void Update()
    {
        transform.Translate(vector * Time.deltaTime * speed);
    }
}
