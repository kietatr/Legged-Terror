using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainType {
    public string name;
    public float height;
    public Color color;
}

public class TileGen : MonoBehaviour
{
    [SerializeField]
    private float heightMult;

     [SerializeField]
    private AnimationCurve heightCurve;

    [SerializeField]
    private TerrainType[] terrainTypes;
    [SerializeField]
    private MakeNoiseMapping makeNoiseMapping;
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private MeshCollider meshCollider;
    [SerializeField]
    private float scale;
    
    void Init() {
        makeNoiseMapping = GetComponent<MakeNoiseMapping>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void GenerateTile() {
        Init();

        int depth = (int)Mathf.Sqrt(this.meshFilter.mesh.vertices.Length);
        int width = depth;
        float x = -this.gameObject.transform.position.x;
        float z = -this.gameObject.transform.position.z;
        float[,] map = this.makeNoiseMapping.GenNoiseMap(depth, width, scale, x, z);
        this.meshRenderer.material.mainTexture = BuildTexture(map);

        UpdateVertices(map);
    }

    void UpdateVertices(float[,] heightMapping)
    {
        int depth = heightMapping.GetLength(0);
        int width = heightMapping.GetLength(1);
        Vector3[] vertices = this.meshFilter.mesh.vertices;
        int ind = 0;
        for(int i = 0; i < depth; i++)
        {
            for(int j = 0; j < width; j++)
            {
                float height = heightMapping[i, j];
                Vector3 v = vertices[ind];
                vertices[ind++] = new Vector3(v.x, this.heightCurve.Evaluate(height) * heightMult, v.z);
            }
        }
        this.meshFilter.mesh.vertices = vertices;
		this.meshFilter.mesh.RecalculateBounds ();
		this.meshFilter.mesh.RecalculateNormals ();
		this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }

    TerrainType ChooseTerrain(float height)
    {
        foreach (TerrainType terrain in terrainTypes) {
            if (height < terrain.height) return terrain;
        }
        return terrainTypes[terrainTypes.Length - 1];
    }
    private Texture2D BuildTexture(float[,] heightMapping) {
        int depth = heightMapping.GetLength(0);
        int width = heightMapping.GetLength(1);
        Color[] colorMap = new Color[depth * width];
        for (int i = 0; i < depth; i++) 
        { 
            for(int j = 0; j < width; j++)
            {
                colorMap[i * width + j] = ChooseTerrain(heightMapping[i,j]).color;
            }
        }
        Texture2D tileTex = new Texture2D(width, depth);
        tileTex.wrapMode = TextureWrapMode.Clamp;
        tileTex.SetPixels(colorMap);
        tileTex.Apply();
        return tileTex;
    }
}
