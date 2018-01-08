using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x0200004D RID: 77
	public interface ICustomInAppPurchase
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000266 RID: 614
		string ProductId { get; }

		// Token: 0x06000267 RID: 615
		void RecordResolution(PurchaseResolutionType resolution);
	}
}
