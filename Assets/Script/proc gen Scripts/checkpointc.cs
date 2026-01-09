using UnityEngine;

public class checkpointc : MonoBehaviour
{
    [SerializeField] float checkpointTimeExtension = 5f;
    [SerializeField] float obstacleDecreaseTimeAmount = .2f;

    GameManager gameManager;
    ObstacleSpawaner obstacleSpawaner;
    
    // Array to store all colliders on this checkpoint
    Collider[] checkpointColliders;

    const string playerString = "Player";

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawaner = FindFirstObjectByType<ObstacleSpawaner>();
        
        // Get all colliders on this GameObject and its children
        checkpointColliders = GetComponentsInChildren<Collider>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Only ignore collisions with obstacles (objects with Rigidbody that are NOT the player)
        if (collision.gameObject.GetComponent<Rigidbody>() != null && 
            !collision.gameObject.CompareTag(playerString))
        {
            // Ignore collision between checkpoint colliders and obstacle
            foreach (Collider checkpointCollider in checkpointColliders)
            {
                Collider obstacleCollider = collision.collider;
                if (checkpointCollider != null && obstacleCollider != null && !checkpointCollider.isTrigger)
                {
                    Physics.IgnoreCollision(checkpointCollider, obstacleCollider, true);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTime(checkpointTimeExtension);
            obstacleSpawaner.DecreaseObstacleSpawnTime(obstacleDecreaseTimeAmount);
        }
    }

}
