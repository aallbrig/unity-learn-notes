using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject plane;
    public Vector3 offset = new Vector3(30, 0, 10);

    private void Update()
    {
        transform.position = plane.transform.position + offset;
    }
}
