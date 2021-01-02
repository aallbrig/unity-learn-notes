using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody _rigidbody;
    private GameObject _focalPoint;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        _rigidbody.AddForce(Input.GetAxis("Vertical") * _focalPoint.transform.forward * speed);
    }
}
