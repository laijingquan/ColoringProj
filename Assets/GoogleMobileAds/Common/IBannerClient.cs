using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000056 RID: 86
	internal interface IBannerClient
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060002E5 RID: 741
		// (remove) Token: 0x060002E6 RID: 742
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060002E7 RID: 743
		// (remove) Token: 0x060002E8 RID: 744
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060002E9 RID: 745
		// (remove) Token: 0x060002EA RID: 746
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060002EB RID: 747
		// (remove) Token: 0x060002EC RID: 748
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060002ED RID: 749
		// (remove) Token: 0x060002EE RID: 750
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x060002EF RID: 751
		void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position);

		// Token: 0x060002F0 RID: 752
		void CreateBannerView(string adUnitId, AdSize adSize, int x, int y);

		// Token: 0x060002F1 RID: 753
		void LoadAd(AdRequest request);

		// Token: 0x060002F2 RID: 754
		void ShowBannerView();

		// Token: 0x060002F3 RID: 755
		void HideBannerView();

		// Token: 0x060002F4 RID: 756
		void DestroyBannerView();
	}
}
