using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnMouseDown()
    {
       Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
