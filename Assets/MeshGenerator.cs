using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f; //making the middle 0, 0

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y); //creating vertices
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height); //creating UV array

                if (heightMap[x, y] < 0.5f)
                {
                    meshData.colors[vertexIndex] = new Color(1, 0, 0);
                }
                else if (heightMap[x, y] > 0.51f)
                {
                    meshData.colors[vertexIndex] = new Color(0, 0, 1);
                }else meshData.colors[vertexIndex] = new Color(0, 1, 0);

                if (x < width - 1 && y < height - 1) //no need to add triangles on the right or bottom vertices
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width); //adding triangles
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Color[] colors;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        colors = new Color[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c) //adding vertices indexes for a triangle
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() //creating a mesh object
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }
}