using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody _enemyRigidbody;
    private GameObject _playerGoal;

    private void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody>();
        _playerGoal = GameObject.Find("Player Goal");
    }

    private void Update()
    {
        // Set enemy direction towards player goal and move there
        var lookDirection = (_playerGoal.transform.position - transform.position).normalized;
        _enemyRigidbody.AddForce(lookDirection * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.name)
        {
            // If enemy collides with either goal, destroy it
            case "Enemy Goal":
            case "Player Goal":
                Destroy(gameObject);
                break;
        }
    }
}
