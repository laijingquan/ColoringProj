using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x0200005A RID: 90
	internal interface IRewardBasedVideoAdClient
	{
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600031A RID: 794
		// (remove) Token: 0x0600031B RID: 795
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600031C RID: 796
		// (remove) Token: 0x0600031D RID: 797
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600031E RID: 798
		// (remove) Token: 0x0600031F RID: 799
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000320 RID: 800
		// (remove) Token: 0x06000321 RID: 801
		event EventHandler<EventArgs> OnAdStarted;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000322 RID: 802
		// (remove) Token: 0x06000323 RID: 803
		event EventHandler<Reward> OnAdRewarded;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000324 RID: 804
		// (remove) Token: 0x06000325 RID: 805
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000326 RID: 806
		// (remove) Token: 0x06000327 RID: 807
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000328 RID: 808
		void CreateRewardBasedVideoAd();

		// Token: 0x06000329 RID: 809
		void LoadAd(AdRequest request, string adUnitId);

		// Token: 0x0600032A RID: 810
		bool IsLoaded();

		// Token: 0x0600032B RID: 811
		void ShowRewardBasedVideoAd();
	}
}
