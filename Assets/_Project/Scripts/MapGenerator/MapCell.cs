using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell : MonoBehaviour
{
    [SerializeField] private GameObject bottomLeftMarker;
    [SerializeField] private GameObject topRightMarker;

    [Header("Objects to spawn")]
    [SerializeField] private GameObject[] trees;
    [SerializeField] private GameObject[] stones;
    [SerializeField] private GameObject[] terrain;

    private int stoneChanceToSpawn = 5;

    //Spawning grid values / variables to control cell size
    private Vector3 currentPos;
    private Vector3 worldObjectStartPos;
    private Vector3 terrainStartPos;
    
    private float groundWidth;

    private float worldObjectIncrementAmount;
    private float terrainIncrementAmount;

    private float worldObjectRandomAmount;
    private float terrainRandomAmount;

    [Header("Values to control loop through grid")]
    [SerializeField] private int worldObjectRowsAndCols;
    [SerializeField] private int terrainRowsAndCols;

    [SerializeField] private int repeatPasses;
    private int currentPass;

    [SerializeField] private float worldObjectSphereRad;
    [SerializeField] private float terrainSphereRad;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask worldObjectLayer;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnCellObjects());

        this.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
    }

    private IEnumerator SpawnCellObjects()
    {
        groundWidth = topRightMarker.transform.position.x - bottomLeftMarker.transform.position.x;

        worldObjectIncrementAmount = groundWidth / worldObjectRowsAndCols;
        worldObjectRandomAmount = worldObjectIncrementAmount / 2f;

        terrainIncrementAmount = groundWidth / terrainRowsAndCols;
        terrainRandomAmount = terrainIncrementAmount / 2f;

        worldObjectStartPos = new Vector3(
            bottomLeftMarker.transform.position.x - (worldObjectIncrementAmount / 2f),
            bottomLeftMarker.transform.position.y,
            bottomLeftMarker.transform.position.z + (worldObjectIncrementAmount / 2f));

        terrainStartPos = new Vector3(bottomLeftMarker.transform.position.x - (terrainIncrementAmount / 2f),
            bottomLeftMarker.transform.position.y,
            bottomLeftMarker.transform.position.z + (terrainIncrementAmount / 2f));

        for (int _repeatPasses = 0; _repeatPasses < repeatPasses; _repeatPasses++)
        {
            currentPass = _repeatPasses;

            if (currentPass == 0)
            {
                currentPos = terrainStartPos;

                for (int cols = 0; cols < terrainRowsAndCols; cols++)
                {
                    for (int rows = 0; rows < terrainRowsAndCols; rows++)
                    {
                        currentPos = new Vector3(currentPos.x + terrainIncrementAmount, currentPos.y, currentPos.z);

                        GameObject newSpawn = terrain[Random.Range(0, terrain.Length)];

                        SpawnObject(currentPos, newSpawn, terrainSphereRad, true);

                        yield return new WaitForSeconds(0.01f);
                    }

                    currentPos = new Vector3(terrainStartPos.x, currentPos.y, currentPos.z + terrainIncrementAmount);
                }
            }

            else if(currentPass > 0) 
            {
                currentPos = worldObjectStartPos;

                for (int cols = 0; cols < worldObjectRowsAndCols; cols++)
                {
                    for (int rows = 0; rows < worldObjectRowsAndCols; rows++)
                    {
                        currentPos = new Vector3(currentPos.x + worldObjectIncrementAmount, currentPos.y, currentPos.z);

                        int spawnChance = Random.Range(0, stoneChanceToSpawn + 1);

                        if (spawnChance == 1)
                        {
                            GameObject newSpawn = stones[Random.Range(0, stones.Length)];
                            SpawnObject(currentPos, newSpawn, worldObjectSphereRad, false);

                            yield return new WaitForSeconds(0.01f);
                        }

                        else
                        {
                            GameObject newSpawn = trees[Random.Range(0, trees.Length)];
                            SpawnObject(currentPos, newSpawn, worldObjectSphereRad, false);

                            yield return new WaitForSeconds(0.01f);
                        }

                        currentPos = new Vector3(
                            worldObjectStartPos.x, 
                            currentPos.y,
                            currentPos.z + worldObjectIncrementAmount);
                    }
                }
            }
        }

        CellGenerationDone();
    }

    private void CellGenerationDone()
    {
        Debug.LogWarning("Spawned");
    }

    private void SpawnObject(Vector3 newSpawnPos, GameObject objectToSpawn, float radiusOsPhere, bool isObjectTerrain)
    {
        if (isObjectTerrain)
        {
            Vector3 randPos = new Vector3(
                newSpawnPos.x + Random.Range(-terrainRandomAmount, terrainRandomAmount + 1),
                0f,
                newSpawnPos.z + Random.Range(-terrainRandomAmount, terrainRandomAmount + 1));
            Vector3 rayPos = new Vector3(randPos.x, 10f, randPos.z);

            if (Physics.Raycast(rayPos, Vector3.down, Mathf.Infinity, groundLayer))
            {

                Collider[] objectsHit = Physics.OverlapSphere(rayPos, radiusOsPhere, terrainLayer);

                if (objectsHit.Length == 0)
                {
                    GameObject terrainObject = Instantiate(objectToSpawn, randPos, Quaternion.identity, this.transform);

                    terrainObject.transform.localScale = new Vector3(terrainObject.transform.localScale.x / this.transform.localScale.x,
                        terrainObject.transform.localScale.y / this.transform.localScale.y,
                        terrainObject.transform.localScale.z / this.transform.localScale.z);

                    terrainObject.transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x, 
                        Random.Range(0f, 360f), 
                        transform.eulerAngles.z);
                }
            }
        }

        else
        {
            Vector3 randPos = new Vector3(
                newSpawnPos.x + Random.Range(-worldObjectRandomAmount, worldObjectRandomAmount + 1),
                newSpawnPos.y,
                newSpawnPos.z + Random.Range(-worldObjectRandomAmount, worldObjectRandomAmount + 1));

            Vector3 rayPos = new Vector3(randPos.x, 20f, randPos.z);

            RaycastHit hit;

            if (Physics.Raycast(rayPos, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                randPos = new Vector3(randPos.x, hit.point.y, randPos.z);
                
                Collider[] objectsHit = Physics.OverlapSphere(rayPos, radiusOsPhere, worldObjectLayer);

                if (objectsHit.Length == 0)
                {
                    GameObject worldObject = Instantiate(objectToSpawn, randPos, Quaternion.identity, this.transform);

                    worldObject.transform.localScale = new Vector3(worldObject.transform.localScale.x / this.transform.localScale.x,
                        worldObject.transform.localScale.y / this.transform.localScale.y,
                        worldObject.transform.localScale.z / this.transform.localScale.z);

                    worldObject.transform.position = new Vector3(
                        worldObject.transform.position.x,
                        worldObject.transform.position.y + (worldObject.GetComponent<Renderer>().bounds.extents.y * 0.7f),
                        worldObject.transform.position.z);

                    worldObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0f, 360f), transform.eulerAngles.z);
                }
            }
        }
    }
}
