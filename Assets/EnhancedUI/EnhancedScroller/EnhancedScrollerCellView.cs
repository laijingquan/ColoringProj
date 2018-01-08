using System;
using UnityEngine;

namespace EnhancedUI.EnhancedScroller
{
	// Token: 0x02000022 RID: 34
	public class EnhancedScrollerCellView : MonoBehaviour
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00005E2E File Offset: 0x0000422E
		public virtual void RefreshCellView()
		{
		}

		// Token: 0x040000D6 RID: 214
		public string cellIdentifier;

		// Token: 0x040000D7 RID: 215
		[NonSerialized]
		public int cellIndex;

		// Token: 0x040000D8 RID: 216
		[NonSerialized]
		public int dataIndex;

		// Token: 0x040000D9 RID: 217
		[NonSerialized]
		public bool active;
	}
}
