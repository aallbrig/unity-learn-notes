using UnityEngine;

public class RandomizeForces : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _forceLower = 12;
    private float _forceUpper = 16;
    private float _torqueLower = -10;
    private float _torqueUpper = 10;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(Vector3.up * Random.Range(_forceLower, _forceUpper), ForceMode.Impulse);
        _rigidbody.AddTorque(
            Random.Range(_torqueLower, _forceUpper),
            Random.Range(_torqueLower, _torqueUpper),
            Random.Range(_torqueLower, _torqueUpper),
            ForceMode.Impulse
        );
    }
}
