using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    [Header("Object Spawning")]
    [SerializeField] private GameObject block;
    [SerializeField] private Transform blockTidy;
    [SerializeField] private int numberOfBlocksToSpawn;
    private List<GameObject> spawnedBuildingBlocks;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints = new Transform[4]; 
    
    [Header("Colours")]
    [SerializeField] private Material[] blockColours = new Material[4]; 

    void Start()
    {
        SpawnInBlocks();
    }

    void SpawnInBlocks()
    {
        spawnedBuildingBlocks = new List<GameObject>();

        for(int i = 0; i < numberOfBlocksToSpawn; i++)
        {
            GameObject _buildingBlock = Instantiate(block,transform.position,block.transform.rotation) as GameObject;
            _buildingBlock.transform.parent = blockTidy;
            spawnedBuildingBlocks.Add(_buildingBlock);
            _buildingBlock.SetActive(false);
        }

        InvokeRepeating("SpawnCube", 1.0f, 2.0f);
    }

    void SpawnCube()
    {
        GameObject _buildingBlock = null;

        for(int i = 0; i < spawnedBuildingBlocks.Count; i++)
        {
            if (!spawnedBuildingBlocks[i].activeInHierarchy)
            {
                _buildingBlock = spawnedBuildingBlocks[i];
            }
        }

        if (_buildingBlock == null)
        {
            _buildingBlock = Instantiate(block, transform.position, block.transform.rotation) as GameObject;
            _buildingBlock.SetActive(false);
            _buildingBlock.transform.parent = blockTidy;
            spawnedBuildingBlocks.Add(_buildingBlock);
        }

        //ChooseColour
        int _colour = Random.Range(0, blockColours.Length);
        _buildingBlock.GetComponent<MeshRenderer>().material = blockColours[_colour];
        _buildingBlock.GetComponent<BuildingBlockBehaviour>().currentColour = blockColours[_colour];

        //Place Random Block
        int _spawnPos = Random.Range(0, spawnPoints.Length);
        _buildingBlock.transform.position = spawnPoints[_spawnPos].position;
        _buildingBlock.SetActive(true);
    }
}
