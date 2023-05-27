using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject trianglePrefab;
    [SerializeField] float timer;
    [SerializeField] float spawnBlockTime;
    [SerializeField] float spawnTriangleTime;
    [SerializeField] Vector2 spawnBlockPos;
    [SerializeField] Vector2 spawnTrianglePos;

    [SerializeField] Transform blockParent;
    [SerializeField] Transform triangleParent;
    ObjectPooler objectPooler;
    private int counter;
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnBlockTime)
        {
            counter++;
            
            SpawnBlock();
            if(counter>=5)
            {
                SpawnTriangle();
                counter = 0;
            }
            timer -= spawnBlockTime;
        }
    }

    private  void SpawnBlock()
    {
        float xPos = Random.Range(-2.0f, 2.0f);
        spawnBlockPos = new Vector3(xPos, spawnBlockPos.y,0);
        GameObject spawnedBlock = objectPooler.SpawnFromPool("Block", spawnBlockPos, blockPrefab.transform.rotation); 
        spawnedBlock.transform.parent = blockParent.transform;
    }
    private void SpawnTriangle()
    {
        spawnTrianglePos = new Vector2(spawnBlockPos.x, spawnBlockPos.y + 0.4f);
        GameObject spawnedTriangle = objectPooler.SpawnFromPool("Triangle", spawnTrianglePos, trianglePrefab.transform.rotation);
        spawnedTriangle.transform.parent = triangleParent.transform;
    }
}
