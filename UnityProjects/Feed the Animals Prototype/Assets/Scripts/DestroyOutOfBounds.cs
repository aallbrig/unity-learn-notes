using System;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float topBounds = 70;
    public float bottomBounds = -10;

    private void Update()
    {
        if (transform.position.z > topBounds || transform.position.z < bottomBounds) Destroy(gameObject);
    }
}
