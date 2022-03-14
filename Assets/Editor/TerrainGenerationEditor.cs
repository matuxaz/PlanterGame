using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(TerrainGeneration))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGeneration gen = (TerrainGeneration)target;

        if(DrawDefaultInspector())
        {
            if (gen.autoUpdate)
            {
                gen.generateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            gen.generateMap();
        }
    }
}
