using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class GenerateGrid : MonoBehaviour {

    public int xSize;//x方向有多少个四边形
    public int ySize;//y方向有多少个四边形
    WaitForSeconds wait = new WaitForSeconds(0.01f);
    private Mesh mesh;
    public Texture2D mainTex;
    public int mainTexID = 16;


    public Vector2[] uv;
    public Vector3[] vertices;
    public int[] triangles;
    private void Awake()
    {
        Init();
        GenerateLine();
            //GenerateLine2();
    }

    public bool add = false;
    public void LateUpdate()
    {
        if(add)
        {
            //addSquad();
            GenerateLine();
            add = false;
        }
    }

    void addSquad()
    {
        var triLength = triangles.Length;
        var verlength = vertices.Length;
        var pos = verlength / 2;

        var uv2 = new Vector2[vertices.Length + 2];
        var vertices2 = new Vector3[vertices.Length + 2];
        var triangles2 = new int[triangles.Length + 6];
        uv.CopyTo(uv2, 0);
        vertices.CopyTo(vertices2, 0);
        triangles.CopyTo(triangles2, 0);

        uv = uv2;
        vertices = vertices2;
        triangles = triangles2;


        vertices[verlength] = new Vector3(pos , 0, 0);
        vertices[verlength + 1] = new Vector3(pos , 0, 1);
        //vertices[verlength + 2] = new Vector3(pos , 0, 1);
        //vertices[verlength + 3] = new Vector3(pos + 1, 0, 1);

        uv[verlength] = new Vector2(0,0);
        uv[verlength + 1] = new Vector2(1,0);
        //uv[verlength + 2] = new Vector2(0, 1);
        //uv[verlength + 3] = new Vector2(1, 1);

        triangles[0 + triLength] = (verlength-1) /2;
        triangles[1 + triLength] = triangles[4 + triLength] = verlength -1;
        triangles[2 + triLength] = triangles[3 + triLength] = verlength;
        triangles[5 + triLength] = verlength + 1;

        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void RemoveSquad()
    {

    }

    void GenerateLine2()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        uv = new Vector2[vertices.Length];

        int _index = 0;
        for(int i=0; i <= xSize;i++)
        {
            vertices[_index] = new Vector3(i, 0, 0);
            uv[_index] = new Vector2(_index % 2, 0);
            _index++;
            vertices[_index] = new Vector3(i, 0, 1);
            uv[_index] = new Vector2(_index%2, 1);
            _index++;
        }

        //for (int i = 0, y = 0; y <= ySize; y++)
        //{
        //    for (int x = 0; x <= xSize; x++, i++)
        //    {
        //        vertices[i] = new Vector3(x, 0, y);
        //        uv[i] = new Vector2(x % 2, y);
        //        yield return wait;
        //    }
        //}

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        GetComponent<MeshRenderer>().material.mainTexture = mainTex;


        triangles = new int[xSize * ySize * 6];//一个四边形有六个三角形索引

        for(int index = 0,jump=2;index<triangles.Length;index+=6,jump+=2)
        {
            triangles[0] = 0 + jump;
            triangles[1] = triangles[4] = 1 + jump;
            triangles[2] = triangles[3] = 2 + jump;
            triangles[5] = 3 + jump; 
        }

        //for (int y = 0, t = 0, v = 0; y < ySize; y++, v++)
        //{
        //    for (int x = 0; x < xSize; t += 6, x++, v++)
        //    {
        //        triangles[0 + t] = 0 + v;
        //        triangles[1 + t] = triangles[4 + t] = xSize + 1 + v;
        //        triangles[2 + t] = triangles[3 + t] = 1 + v;
        //        triangles[5 + t] = xSize + 2 + v;
        //        yield return wait;
        //        mesh.triangles = triangles;
        //    }
        //    Debug.Log("一行");
        //}

        mesh.triangles = triangles;
    }

    void Init()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        if (mainTex == null)
        {
            mainTex = Resources.Load<Texture2D>("Grayscale/" + mainTexID + "PicGrey");
        }
        GetComponent<MeshRenderer>().material.mainTexture = mainTex;
    }

    

    void GenerateLine()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, y);
                uv[i] = new Vector2(x%2,y);
                //yield return wait;
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;


        triangles = new int[xSize * ySize * 6];//一个四边形有六个三角形索引
        for (int y = 0, t = 0, v = 0; y < ySize; y++, v++)
        {
            for (int x = 0; x < xSize; t += 6, x++, v++)
            {
                triangles[0 + t] = 0 + v;
                triangles[1 + t] = triangles[4 + t] = xSize + 1 + v;
                triangles[2 + t] = triangles[3 + t] = 1 + v;
                triangles[5 + t] = xSize + 2 + v;
                //yield return wait;
                mesh.triangles = triangles;
            }
            Debug.Log("一行");
        }
        mesh.triangles = triangles;
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
                yield return wait;
            }
        }

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;

        mainTex = Resources.Load<Texture2D>("Grayscale/"+ mainTexID + "PicGrey");
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
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
