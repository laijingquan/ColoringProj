using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class EventManager : MonoBehaviour
{
	// Token: 0x14000054 RID: 84
	// (add) Token: 0x060003FF RID: 1023 RVA: 0x0001269C File Offset: 0x00010A9C
	// (remove) Token: 0x06000400 RID: 1024 RVA: 0x000126D0 File Offset: 0x00010AD0
	public static event EventManager.EventAction CorrectColor;

	// Token: 0x06000401 RID: 1025 RVA: 0x00012704 File Offset: 0x00010B04
	public static void CheckImage()
	{
		EventManager.CorrectColor();
	}

	// Token: 0x02000071 RID: 113
	// (Invoke) Token: 0x06000403 RID: 1027
	public delegate void EventAction();
}
