using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh, Fall };
    public DrawMode drawMode;

    [Range(0, 1000)] public int mapWidth;
    [Range(0, 1000)] public int mapHeight;
    [Range(1, 100)] public float noiseScale;

    [Range(1, 50)] public int octaves;
    [Range(0, 1)] public float persistance;
    [Range(0, 10)] public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] falloffMap;

    private void Start()
    {
        falloffMap = FallOffGenerator.GenerateFalloffMap(mapWidth);
    }

    public void generateMap()
    {
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        DisplayTerrain display = FindObjectOfType<DisplayTerrain>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }else if (drawMode == DrawMode.Fall)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FallOffGenerator.GenerateFalloffMap(mapWidth)));
        }


        void OnValidate()
        {
            if (lacunarity < 1) lacunarity = 1;
            if (octaves < 0) octaves = 0;

            falloffMap = FallOffGenerator.GenerateFalloffMap(mapWidth);
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}