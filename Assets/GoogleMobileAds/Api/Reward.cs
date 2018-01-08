using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000052 RID: 82
	public class Reward : EventArgs
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000E396 File Offset: 0x0000C796
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000E39E File Offset: 0x0000C79E
		public string Type { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000E3A7 File Offset: 0x0000C7A7
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000E3AF File Offset: 0x0000C7AF
		public double Amount { get; set; }
	}
}
