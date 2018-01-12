using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000006 RID: 6
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GridMesh : MonoBehaviour
{
    List<Vector3> list;
    WaitForSeconds wait = new WaitForSeconds(1f);
    public void Awake()
    {
        StartCoroutine(UpdateGrid());
        //UpdateGrid();
    }

    public void Start()
    {
        
    }
    // Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000005F8
    public IEnumerator UpdateGrid()
	{
		int gridSize = this._GridSize;
		MeshFilter component = base.gameObject.GetComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		list = new List<Vector3>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < gridSize / this.CellSize; i++)
		{
            //最左下，最左上，最左下，最右下
			list.Add(new Vector3((float)(i * this.CellSize), 0f, 0f));
			list.Add(new Vector3((float)(i * this.CellSize), 0f, (float)gridSize));
			list.Add(new Vector3(0f, 0f, (float)(i * this.CellSize)));
			list.Add(new Vector3((float)gridSize, 0f, (float)(i * this.CellSize)));
            //相当于按顺序连线
			list2.Add(4 * i);
			list2.Add(4 * i + 1);
			list2.Add(4 * i + 2);
			list2.Add(4 * i + 3);

            //为了能看到绘制顺序,这里要重新刷一下
            {
                mesh.vertices = list.ToArray();
                mesh.SetIndices(list2.ToArray(), MeshTopology.Lines, 0);
                component.mesh = mesh;
            }
            yield return wait;
        }
		mesh.vertices = list.ToArray();
		mesh.SetIndices(list2.ToArray(), MeshTopology.Lines, 0);
		component.mesh = mesh;
		MeshRenderer component2 = base.gameObject.GetComponent<MeshRenderer>();
		component2.material = new Material(Shader.Find("Sprites/Default"));
		component2.material.color = Color.black;
        //yield return wait;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (list == null) return;
        for (int i = 0; i < list.Count; i++)
        {
            Gizmos.DrawSphere(list[i], 1f);
        }
    }

    // Token: 0x04000011 RID: 17
    [Range(1f, 10000f)]
	public int _GridSize;//总大小

	// Token: 0x04000012 RID: 18
	[Range(1f, 10000f)]
	public int CellSize;//每个格子的大小
}
