using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectile;
    public KeyCode fireButton = KeyCode.Space;
    private bool _isProjectileNull;

    private void Start()
    {
        _isProjectileNull = projectile == null;
    }

    private void Update()
    {
        if (_isProjectileNull) return;

        if (Input.GetKeyDown(fireButton))
        {
            // Launch projectile
        }
    }
}
