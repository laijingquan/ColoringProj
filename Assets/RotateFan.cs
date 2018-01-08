using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class RotateFan : MonoBehaviour
{
	// Token: 0x06000480 RID: 1152 RVA: 0x00013C9B File Offset: 0x0001209B
	private void Start()
	{
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00013C9D File Offset: 0x0001209D
	private void Update()
	{
		base.transform.Rotate(this.xSpeed * Time.deltaTime, this.ySpeed * Time.deltaTime, this.zSpeed * Time.deltaTime);
	}

	// Token: 0x0400028C RID: 652
	public float xSpeed;

	// Token: 0x0400028D RID: 653
	public float ySpeed;

	// Token: 0x0400028E RID: 654
	public float zSpeed;
}
