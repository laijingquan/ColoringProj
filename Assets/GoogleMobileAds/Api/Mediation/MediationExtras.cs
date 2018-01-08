using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Api.Mediation
{
	// Token: 0x02000050 RID: 80
	public abstract class MediationExtras
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000DFC5 File Offset: 0x0000C3C5
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000DFCD File Offset: 0x0000C3CD
		public Dictionary<string, string> Extras { get; protected set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600027F RID: 639
		public abstract string AndroidMediationExtraBuilderClassName { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000280 RID: 640
		public abstract string IOSMediationExtraBuilderClassName { get; }
	}
}
