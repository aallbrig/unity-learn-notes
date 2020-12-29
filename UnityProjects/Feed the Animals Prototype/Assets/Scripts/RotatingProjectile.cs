using UnityEngine;
using Random = System.Random;

public class RotatingProjectile : MonoBehaviour
{
    public int minRPS = 5;
    public int maxRPS = 20;
    private int _rotateBy;

    private void Start()
    {
        _rotateBy = new Random().Next(minRPS, maxRPS);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateBy);
    }
}
