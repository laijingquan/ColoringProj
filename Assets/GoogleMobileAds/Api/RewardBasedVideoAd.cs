using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000053 RID: 83
	public class RewardBasedVideoAd
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000E3B8 File Offset: 0x0000C7B8
		private RewardBasedVideoAd()
		{
			this.client = GoogleMobileAdsClientFactory.BuildRewardBasedVideoAdClient();
			this.client.CreateRewardBasedVideoAd();
			this.client.OnAdLoaded += delegate(object sender, EventArgs args)
			{
				if (this.OnAdLoaded != null)
				{
					this.OnAdLoaded(this, args);
				}
			};
			this.client.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
			{
				if (this.OnAdFailedToLoad != null)
				{
					this.OnAdFailedToLoad(this, args);
				}
			};
			this.client.OnAdOpening += delegate(object sender, EventArgs args)
			{
				if (this.OnAdOpening != null)
				{
					this.OnAdOpening(this, args);
				}
			};
			this.client.OnAdStarted += delegate(object sender, EventArgs args)
			{
				if (this.OnAdStarted != null)
				{
					this.OnAdStarted(this, args);
				}
			};
			this.client.OnAdClosed += delegate(object sender, EventArgs args)
			{
				if (this.OnAdClosed != null)
				{
					this.OnAdClosed(this, args);
				}
			};
			this.client.OnAdLeavingApplication += delegate(object sender, EventArgs args)
			{
				if (this.OnAdLeavingApplication != null)
				{
					this.OnAdLeavingApplication(this, args);
				}
			};
			this.client.OnAdRewarded += delegate(object sender, Reward args)
			{
				if (this.OnAdRewarded != null)
				{
					this.OnAdRewarded(this, args);
				}
			};
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000E482 File Offset: 0x0000C882
		public static RewardBasedVideoAd Instance
		{
			get
			{
				if (RewardBasedVideoAd.instance == null)
				{
					RewardBasedVideoAd.instance = new RewardBasedVideoAd();
				}
				return RewardBasedVideoAd.instance;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600029E RID: 670 RVA: 0x0000E4A0 File Offset: 0x0000C8A0
		// (remove) Token: 0x0600029F RID: 671 RVA: 0x0000E4D8 File Offset: 0x0000C8D8
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060002A0 RID: 672 RVA: 0x0000E510 File Offset: 0x0000C910
		// (remove) Token: 0x060002A1 RID: 673 RVA: 0x0000E548 File Offset: 0x0000C948
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060002A2 RID: 674 RVA: 0x0000E580 File Offset: 0x0000C980
		// (remove) Token: 0x060002A3 RID: 675 RVA: 0x0000E5B8 File Offset: 0x0000C9B8
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060002A4 RID: 676 RVA: 0x0000E5F0 File Offset: 0x0000C9F0
		// (remove) Token: 0x060002A5 RID: 677 RVA: 0x0000E628 File Offset: 0x0000CA28
		public event EventHandler<EventArgs> OnAdStarted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060002A6 RID: 678 RVA: 0x0000E660 File Offset: 0x0000CA60
		// (remove) Token: 0x060002A7 RID: 679 RVA: 0x0000E698 File Offset: 0x0000CA98
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060002A8 RID: 680 RVA: 0x0000E6D0 File Offset: 0x0000CAD0
		// (remove) Token: 0x060002A9 RID: 681 RVA: 0x0000E708 File Offset: 0x0000CB08
		public event EventHandler<Reward> OnAdRewarded;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060002AA RID: 682 RVA: 0x0000E740 File Offset: 0x0000CB40
		// (remove) Token: 0x060002AB RID: 683 RVA: 0x0000E778 File Offset: 0x0000CB78
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x060002AC RID: 684 RVA: 0x0000E7AE File Offset: 0x0000CBAE
		public void LoadAd(AdRequest request, string adUnitId)
		{
			this.client.LoadAd(request, adUnitId);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000E7BD File Offset: 0x0000CBBD
		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000E7CA File Offset: 0x0000CBCA
		public void Show()
		{
			this.client.ShowRewardBasedVideoAd();
		}

		// Token: 0x040001E0 RID: 480
		private IRewardBasedVideoAdClient client;

		// Token: 0x040001E1 RID: 481
		private static RewardBasedVideoAd instance;
	}
}
