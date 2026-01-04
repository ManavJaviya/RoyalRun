using System.Collections;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class ObstacleSpawaner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabes;
    [SerializeField] float obsticalSpawnTime = 1f;
    [SerializeField] float minObstacleSpawnTime = .2f;
    [SerializeField] float spawnWidth = 4f;
    [SerializeField] Transform obsticalParant;
    void Start()
    {
        StartCoroutine(SpawnObsticalRoutine());
    }
    public void DecreaseObstacleSpawnTime(float amount)
    {
        obsticalSpawnTime -= amount;

        if (obsticalSpawnTime <= minObstacleSpawnTime)
        {
            obsticalSpawnTime = minObstacleSpawnTime;
        }
    }
    IEnumerator SpawnObsticalRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefabe = obstaclePrefabes[Random.Range(0, obstaclePrefabes.Length)];
            Vector3 spawnPoz = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z );
            yield return new WaitForSeconds(obsticalSpawnTime);
            Instantiate(obstaclePrefabe, spawnPoz, Random.rotation , obsticalParant);
        }
    }

}
