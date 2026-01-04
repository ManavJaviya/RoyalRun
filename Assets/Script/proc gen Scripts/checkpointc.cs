using UnityEngine;

public class checkpointc : MonoBehaviour
{
    [SerializeField] float checkpointTimeExtension = 5f;
    [SerializeField] float obstacleDecreaseTimeAmount = .2f;

    GameManager gameManager;
    ObstacleSpawaner obstacleSpawaner;

    const string playerString = "Player";

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawaner = FindFirstObjectByType<ObstacleSpawaner>();
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
