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
    
    LevelGenerator levelGenerator;
    float baseSpawnTime;
    float baseMoveSpeed;
    float spawnTimeDecreaseAmount = 0f;
    
    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        baseSpawnTime = obsticalSpawnTime;
        baseMoveSpeed = levelGenerator != null ? levelGenerator.GetCurrentMoveSpeed() : 8f;
        StartCoroutine(SpawnObsticalRoutine());
    }
    
    public void DecreaseObstacleSpawnTime(float amount)
    {
        spawnTimeDecreaseAmount += amount;
    }
    
    float CalculateAdjustedSpawnTime()
    {
        if (levelGenerator == null) return obsticalSpawnTime;
        
        // Get current move speed from LevelGenerator
        float currentMoveSpeed = levelGenerator.GetCurrentMoveSpeed();
        
        // Calculate spawn time that scales inversely with speed
        // When speed doubles, spawn time should double (spawn half as often)
        // This maintains consistent obstacle density relative to movement speed
        float speedRatio = baseMoveSpeed / currentMoveSpeed;
        float adjustedSpawnTime = baseSpawnTime * speedRatio;
        
        // Apply checkpoint-based decrease
        adjustedSpawnTime -= spawnTimeDecreaseAmount;
        
        // Clamp to minimum
        if (adjustedSpawnTime <= minObstacleSpawnTime)
        {
            adjustedSpawnTime = minObstacleSpawnTime;
        }
        
        return adjustedSpawnTime;
    }
    
    IEnumerator SpawnObsticalRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefabe = obstaclePrefabes[Random.Range(0, obstaclePrefabes.Length)];
            Vector3 spawnPoz = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z );
            
            float currentSpawnTime = CalculateAdjustedSpawnTime();
            yield return new WaitForSeconds(currentSpawnTime);
            
            Instantiate(obstaclePrefabe, spawnPoz, Random.rotation , obsticalParant);
        }
    }

}
