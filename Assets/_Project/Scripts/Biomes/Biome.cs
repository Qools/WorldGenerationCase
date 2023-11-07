using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : ScriptableObject
{
    public BiomeType biomeType;

    public Material groundMaterial;

    public GameObject[] obstacles;
    public GameObject[] decorativeElements;
    public GameObject[] terrains;
}
