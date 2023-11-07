using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorManager : MonoBehaviour
{
    public MapCell cellPrefab;

    public int worldSizeX, worldSizeZ;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorldCell();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWorldCell()
    {
        Vector3 startPosition = Vector3.zero;

        for (int i = 1; i <= worldSizeZ; i++)
        {
            for (int k = 1; k <= worldSizeX; k++)
            {
                GameObject generatedCell = Instantiate(cellPrefab.gameObject, startPosition, Quaternion.identity);

                startPosition += new Vector3(cellPrefab.transform.lossyScale.x, 0f, 0f);
            }

            startPosition = new Vector3(0f, 0f, cellPrefab.transform.lossyScale.z * i);
        }
    }
}
