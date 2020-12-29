using System;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 rotateOffset;
    private bool _isTargetNull;

    private void Start()
    {
        _isTargetNull = target == null;
    }

    private void Update()
    {
        if (_isTargetNull) return;

        transform.LookAt(target);
        transform.Rotate(rotateOffset);
    }
}
