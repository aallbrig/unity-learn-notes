using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float leftBounds = -15;

    private void Update()
    {
        if (transform.position.x < leftBounds) Destroy(transform.gameObject);
    }
}
