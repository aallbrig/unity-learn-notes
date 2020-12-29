using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectile;
    public KeyCode fireButton = KeyCode.Space;
    public Transform optionalFireLocation;

    private bool _isProjectileNull;
    private Transform _fireLocation;

    private void Start()
    {
        _isProjectileNull = projectile == null;
        _fireLocation = optionalFireLocation == null ? transform : optionalFireLocation;
    }

    private void Update()
    {
        if (_isProjectileNull) return;

        if (Input.GetKeyDown(fireButton)) Instantiate(projectile, _fireLocation.position, _fireLocation.rotation);
    }
}
