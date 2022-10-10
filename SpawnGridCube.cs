using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGridCube : MonoBehaviour
{
    [Header("Configurations")]
    public int gridx;
    public int gridz;
    public float gridSpacingOffset = 1f;
    [SerializeField] Vector3 gridOrigin = Vector3.zero;

    [Header("Floor")]
    [SerializeField] GameObject[] blockFloorToPickFrom;

    [Header("Environments")]
    [SerializeField] GameObject[] environmentsToPickFrom;
    [Range(0f, 1f)]
    [SerializeField] float chanceToSpawnEnvironment;
    // Start is called before the first frame update
    void Start()
    {
        SpawnGridFloor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnGridFloor()
    {
        for( int x = 0; x < gridx; x++)
        {
            for( int z = 0; z < gridz; z++)
            {
                Vector3 spawnPositionFloor = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin; //set the Cube's height
                PickAndSpawnFloor(spawnPositionFloor, Quaternion.identity); //Responsable for creating the cubes

                Vector3 spawnPositionEnvironment = new Vector3(x * gridSpacingOffset, gridSpacingOffset / 2, z * gridSpacingOffset) + gridOrigin;
                PickAndSpawnEnvironment(spawnPositionEnvironment, Quaternion.identity);
            }
        }
    }

    void PickAndSpawnFloor(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, blockFloorToPickFrom.Length);
        GameObject clone = Instantiate(blockFloorToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
    }


    void PickAndSpawnEnvironment(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        float chanceToSpawn = Random.Range(0f, 1f);

        if (chanceToSpawn <= chanceToSpawnEnvironment)
        {
            int randomIndex = Random.Range(0, environmentsToPickFrom.Length);
            GameObject clone = Instantiate(environmentsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
        }
    }
}
