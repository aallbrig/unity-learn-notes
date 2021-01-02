using System;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [Header("Revolutions Per Second")]
    public float revolutionsPerSecond;

    private void Update()
    {
        transform.Rotate(Vector3.up, (360 * revolutionsPerSecond) * Time.deltaTime);
    }
}
