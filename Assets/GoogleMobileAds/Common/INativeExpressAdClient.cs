using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000059 RID: 89
	internal interface INativeExpressAdClient
	{
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600030A RID: 778
		// (remove) Token: 0x0600030B RID: 779
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600030C RID: 780
		// (remove) Token: 0x0600030D RID: 781
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600030E RID: 782
		// (remove) Token: 0x0600030F RID: 783
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000310 RID: 784
		// (remove) Token: 0x06000311 RID: 785
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000312 RID: 786
		// (remove) Token: 0x06000313 RID: 787
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000314 RID: 788
		void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position);

		// Token: 0x06000315 RID: 789
		void CreateNativeExpressAdView(string adUnitId, AdSize adSize, int x, int y);

		// Token: 0x06000316 RID: 790
		void LoadAd(AdRequest request);

		// Token: 0x06000317 RID: 791
		void ShowNativeExpressAdView();

		// Token: 0x06000318 RID: 792
		void HideNativeExpressAdView();

		// Token: 0x06000319 RID: 793
		void DestroyNativeExpressAdView();
	}
}
