using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class FreezingPallet : MonoBehaviour
{
	// Token: 0x06000407 RID: 1031 RVA: 0x00012718 File Offset: 0x00010B18
	private void Start()
	{
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0001271A File Offset: 0x00010B1A
	private void Update()
	{
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0001271C File Offset: 0x00010B1C
	public void OnDown()
	{
		this.CamScript.GetComponent<testPan>().enabled = false;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0001272F File Offset: 0x00010B2F
	public void UpPointer()
	{
		this.CamScript.GetComponent<testPan>().enabled = true;
	}

	// Token: 0x04000252 RID: 594
	public GameObject CamScript;
}
