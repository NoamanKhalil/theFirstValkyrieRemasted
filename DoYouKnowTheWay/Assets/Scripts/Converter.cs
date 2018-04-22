using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//converted from a JS script to C# by Noaman Khalil / TheRealCaedriel AKA BlackHurt#2379
//Edited by Luckie12345 🐺#3850/ @lucsteijvers & Noaman Khalil / TheRealCaedriel AKA BlackHurt#2379


public class Converter : MonoBehaviour
{
    [SerializeField]
    float scale = 1.0f;
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    Texture2D heightMap;
    [SerializeField]
    Material material;
    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        int width = Mathf.Min(heightMap.width, 255);
        int height = Mathf.Min(heightMap.height, 255);
        int y = 0;
        int x = 0;

        Vector3 [] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;

        Vector4[] tangents = new Vector4[height * width];
        // one fucking error here no idea why .... 
        Vector3 size = gameObject.transform.localScale = new Vector3(200, 30, 200);
        Vector2 uvScale = new Vector2(1.0f / (width - 1), 1.0f / (height - 1));
        Vector3 sizeScale = new Vector3(size.x / (width - 1), size.y, size.z / (height - 1));
        float pixelHeight = heightMap.GetPixel(x, y).grayscale;
        Vector3 vertex =new Vector3(x, pixelHeight, y);

        vertices[y * width + x] = Vector3.Scale(sizeScale, vertex);
        uv[y * width + x] = Vector2.Scale(new Vector2(x, y), uvScale);

        Vector3 vertexL = new Vector3(x - 1, heightMap.GetPixel(x - 1, y).grayscale, y);
        Vector3 vertexR =new Vector3(x + 1, heightMap.GetPixel(x + 1, y).grayscale, y);
        Vector3 tan = Vector3.Scale(sizeScale, vertexR - vertexL).normalized;

        tangents[y * width + x] =new Vector4(tan.x, tan.y, tan.z, -1.0f);

        mesh.vertices = vertices;
        mesh.uv = uv;



        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
