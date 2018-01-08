using System;
using ArabicSupport;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class SetArabicTextExample : MonoBehaviour
{
	// Token: 0x06000008 RID: 8 RVA: 0x000020DA File Offset: 0x000004DA
	private void Start()
	{
		base.gameObject.GetComponent<GUIText>().text = "This sentence (wrong display):\n" + this.text + "\n\nWill appear correctly as:\n" + ArabicFixer.Fix(this.text, false, false);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000050E
	private void Update()
	{
	}

	// Token: 0x04000007 RID: 7
	public string text;
}
