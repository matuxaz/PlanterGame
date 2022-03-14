using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTerrain : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        Mesh mesh = meshData.CreateMesh();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
