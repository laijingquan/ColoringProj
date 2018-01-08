using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class FixGUITextCS : MonoBehaviour
{
	// Token: 0x06000005 RID: 5 RVA: 0x000020A7 File Offset: 0x000004A7
	private void Start()
	{
		base.gameObject.GetComponent<GUIText>().text = ArabicFixer.Fix(this.text, this.tashkeel, this.hinduNumbers);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000004D0
	private void Update()
	{
	}

	// Token: 0x04000004 RID: 4
	public string text;

	// Token: 0x04000005 RID: 5
	public bool tashkeel = true;

	// Token: 0x04000006 RID: 6
	public bool hinduNumbers = true;
}
