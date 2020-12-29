using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    [Header("Fire interval (in seconds)")]
    public float fireInterval = 0.5f;
    private float _timeSinceLastFire;

    void Start()
    {
        _timeSinceLastFire = Time.time - fireInterval;
    }
    void Update()
    {
        var canFire = Time.time - _timeSinceLastFire > fireInterval;
        if (canFire && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            _timeSinceLastFire = Time.time;
        }
    }
}
