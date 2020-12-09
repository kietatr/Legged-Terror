using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNoiseMapping : MonoBehaviour
{
    public float[,] GenNoiseMap(int depth, int width, float scale,float x,float z) {
        float[,] noiseMaps = new float[depth, width];
        for (int i = 0; i < depth; i++) { 
            for(int j = 0; j < width; j++)
                // generates noise at this index, scaling it accordingly
                noiseMaps[i, j] = Mathf.PerlinNoise((j+x) / scale, (i+z) / scale);
        }
        return noiseMaps;
    }
}
