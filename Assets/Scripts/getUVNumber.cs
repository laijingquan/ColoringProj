using UnityEngine;
using System.Collections;

public class getUVNumber : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        var uvs = GetComponent<MeshFilter>().mesh.uv;
        var triangle = GetComponent<MeshFilter>().mesh.triangles;
        var vertices = GetComponent<MeshFilter>().mesh.vertices;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
