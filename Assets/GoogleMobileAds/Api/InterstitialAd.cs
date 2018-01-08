using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x0200004F RID: 79
	public class InterstitialAd
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000DC38 File Offset: 0x0000C038
		public InterstitialAd(string adUnitId)
		{
			this.client = GoogleMobileAdsClientFactory.BuildInterstitialClient();
			this.client.CreateInterstitialAd(adUnitId);
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
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000269 RID: 617 RVA: 0x0000DCD8 File Offset: 0x0000C0D8
		// (remove) Token: 0x0600026A RID: 618 RVA: 0x0000DD10 File Offset: 0x0000C110
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600026B RID: 619 RVA: 0x0000DD48 File Offset: 0x0000C148
		// (remove) Token: 0x0600026C RID: 620 RVA: 0x0000DD80 File Offset: 0x0000C180
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600026D RID: 621 RVA: 0x0000DDB8 File Offset: 0x0000C1B8
		// (remove) Token: 0x0600026E RID: 622 RVA: 0x0000DDF0 File Offset: 0x0000C1F0
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600026F RID: 623 RVA: 0x0000DE28 File Offset: 0x0000C228
		// (remove) Token: 0x06000270 RID: 624 RVA: 0x0000DE60 File Offset: 0x0000C260
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000271 RID: 625 RVA: 0x0000DE98 File Offset: 0x0000C298
		// (remove) Token: 0x06000272 RID: 626 RVA: 0x0000DED0 File Offset: 0x0000C2D0
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000273 RID: 627 RVA: 0x0000DF06 File Offset: 0x0000C306
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DF14 File Offset: 0x0000C314
		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DF21 File Offset: 0x0000C321
		public void Show()
		{
			this.client.ShowInterstitial();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DF2E File Offset: 0x0000C32E
		public void Destroy()
		{
			this.client.DestroyInterstitial();
		}

		// Token: 0x040001D1 RID: 465
		private IInterstitialClient client;
	}
}
