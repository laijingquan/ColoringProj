using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class GenerateGrid : MonoBehaviour {

    public int xSize;//x方向有多少个四边形
    public int ySize;//y方向有多少个四边形
    public Vector3[] vertices;
    WaitForSeconds wait = new WaitForSeconds(0.01f);
    private Mesh mesh;
    private void Awake()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, 0,y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
            }
        }

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;

        var mainTex = Resources.Load<Texture2D>("Grayscale/16PicGrey");
        mainTex.filterMode = FilterMode.Point;
        GetComponent<MeshRenderer>().material.mainTexture = mainTex;


        int[] triangles = new int[xSize*ySize*6];//一个四边形有六个三角形索引
        for(int y = 0,t = 0,v=0; y < ySize;y++,v++)
        {
            for (int x = 0; x < xSize; t += 6, x++,v++)
            {
                triangles[0 + t] = 0 + v;
                triangles[1 + t] = triangles[4 + t] = xSize + 1 + v;
                triangles[2 + t] = triangles[3 + t] = 1 + v;
                triangles[5 + t] = xSize + 2 + v;
                yield return wait;
                mesh.triangles = triangles;
            }
            Debug.Log("一行");
        }

        mesh.triangles = triangles;
        //yield return wait;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (vertices == null) return;
        for(int i=0; i < vertices.Length;i++)
        {
            Gizmos.DrawSphere(vertices[i],0.1f);
        }
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
