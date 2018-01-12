using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class Fix3dTextCS : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002066 File Offset: 0x00000466
	private void Start()
	{
		base.gameObject.GetComponent<TextMesh>().text = ArabicFixer.Fix(this.text, this.tashkeel, this.hinduNumbers);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000208F File Offset: 0x0000048F
	private void Update()
	{
	}

	// Token: 0x04000001 RID: 1
	public string text;

	// Token: 0x04000002 RID: 2
	public bool tashkeel = true;

	// Token: 0x04000003 RID: 3
	public bool hinduNumbers = true;
}
