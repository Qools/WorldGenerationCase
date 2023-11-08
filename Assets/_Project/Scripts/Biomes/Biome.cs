using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    [CreateAssetMenu(fileName = "newBiome")]
    public class Biome : ScriptableObject
    {
        public BiomeType biomeType;

        public Material groundMaterial;

        public GameObject[] obstacles;
        public GameObject[] decorativeElements;
        public GameObject[] terrains;
    }
}