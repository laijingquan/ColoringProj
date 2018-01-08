using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class GridManager : MonoBehaviour
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000534
	private void Start()
	{
		this._cam = Camera.main.transform;
		this.pastDist = Mathf.Abs(this._cam.position.y - base.transform.position.y);
		this.gridMesh = base.transform.GetComponentsInChildren<GridMesh>(true);
		this.gridMesh[0].UpdateGrid();
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021A4 File Offset: 0x000005A4
	private void Update()
	{
		float axis = Input.GetAxis("Vertical");
		Camera.main.orthographicSize -= axis * Time.deltaTime * this.zoomSPeed;
		if (Camera.main.orthographicSize < this.maxDist)
		{
		}
	}

	// Token: 0x04000008 RID: 8
	private Transform _cam;

	// Token: 0x04000009 RID: 9
	private GridMesh[] gridMesh;

	// Token: 0x0400000A RID: 10
	private int multiplier = 1;

	// Token: 0x0400000B RID: 11
	private int childCount = 5;

	// Token: 0x0400000C RID: 12
	private float pastDist;

	// Token: 0x0400000D RID: 13
	private float distDevider = 1.8f;

	// Token: 0x0400000E RID: 14
	public float activeNextAfter;

	// Token: 0x0400000F RID: 15
	public float zoomSPeed;

	// Token: 0x04000010 RID: 16
	public float maxDist;
}
