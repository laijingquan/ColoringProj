using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Grid : MonoBehaviour {

    public int xSize, ySize;

    private Vector3[] vertices;
    WaitForSeconds wait = new WaitForSeconds(1f);
    private IEnumerator Generate()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for(int i =0,y=0;y<=ySize;y++)
        {
            for(int x =0;x<=xSize;x++,i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return wait;
            }
        }
    }

    private void Awake()
    {
        StartCoroutine(Generate());
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        Gizmos.color = Color.black;
        for(int i =0; i < vertices.Length;i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
