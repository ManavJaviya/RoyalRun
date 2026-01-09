using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraControler cameraControler;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject CheckPointchunkPrefabs;

    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")]
    [SerializeField] int startingChunkAmmount = 30;
    [SerializeField] int checkpointChunkInterval = 8;

    [SerializeField] float chunkLength = 10;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

    List<GameObject> chunks = new List<GameObject>();
    int chunkSwaned = 0;
    void Start()
    {
        SpanStratingChucks();
    }

    void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed =  moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed , minMoveSpeed, maxMoveSpeed);
        if (newMoveSpeed != moveSpeed )
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
        
            cameraControler.changeCameraFOV(speedAmount);
        }
    }
    
    public float GetCurrentMoveSpeed()
    {
        return moveSpeed;
    }
     public void SpanStratingChucks()
     {

        for (int i = 0; i < startingChunkAmmount; i++)
        {
            SpanChunk();
        }
    }

     float CalculateSpanPositionZ()
    {
        float spanPositionZ;
        if (chunks.Count == 0)
        {
            spanPositionZ = transform.position.z;
        }
        else
        {
            //spanPositionZ = transform.position.z + (i * chunkLength);
            spanPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }
        return spanPositionZ;
    }
    void MoveChunks ()
    {
        // Iterate backwards to safely remove items
        for (int i = chunks.Count - 1; i >= 0; i--)
        {
            GameObject chunk = chunks[i];
            
            // Check if chunk is null (in case it was already destroyed)
            if (chunk == null)
            {
                chunks.RemoveAt(i);
                continue;
            }
            
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.RemoveAt(i);  // Safe removal when iterating backwards
                Destroy(chunk);
                SpanChunk();
            }
        }
    }
    void SpanChunk()
    {
        float spanPositionZ = CalculateSpanPositionZ();
        Vector3 chunkSwanPoz = new Vector3(transform.position.x, transform.position.y, spanPositionZ);
        GameObject chunkToSpawn;

        if (chunkSwaned % checkpointChunkInterval == 0 && chunkSwaned != 0 )
        {
            chunkToSpawn = CheckPointchunkPrefabs;
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, chunkPrefabs.Length);
            chunkToSpawn = chunkPrefabs[randomIndex];
        }
        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSwanPoz, Quaternion.identity, chunkParent);

        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this , scoreManager);

        chunkSwaned++;
    }
}
