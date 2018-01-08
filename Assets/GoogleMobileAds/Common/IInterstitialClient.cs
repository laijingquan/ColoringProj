using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000058 RID: 88
	internal interface IInterstitialClient
	{
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060002FB RID: 763
		// (remove) Token: 0x060002FC RID: 764
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060002FD RID: 765
		// (remove) Token: 0x060002FE RID: 766
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060002FF RID: 767
		// (remove) Token: 0x06000300 RID: 768
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000301 RID: 769
		// (remove) Token: 0x06000302 RID: 770
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000303 RID: 771
		// (remove) Token: 0x06000304 RID: 772
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000305 RID: 773
		void CreateInterstitialAd(string adUnitId);

		// Token: 0x06000306 RID: 774
		void LoadAd(AdRequest request);

		// Token: 0x06000307 RID: 775
		bool IsLoaded();

		// Token: 0x06000308 RID: 776
		void ShowInterstitial();

		// Token: 0x06000309 RID: 777
		void DestroyInterstitial();
	}
}
