using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell : MonoBehaviour
{
    public float sizeX;
    public float sizeZ;

    // Start is called before the first frame update
    void Start()
    {
        SetSCellSize(sizeX, sizeZ);

        this.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();
    }

    public void SetSCellSize(float _sizeX, float _sizeZ)
    {
        this.transform.localScale = new Vector3 (_sizeX, 1, _sizeZ);
    }

    public Vector3 GetCellSize()
    {
        return this.transform.localScale;
    }
}
