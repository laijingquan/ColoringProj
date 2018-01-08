using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000049 RID: 73
	public class BannerView
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0000D7EB File Offset: 0x0000BBEB
		public BannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.client = GoogleMobileAdsClientFactory.BuildBannerClient();
			this.client.CreateBannerView(adUnitId, adSize, position);
			this.configureBannerEvents();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D812 File Offset: 0x0000BC12
		public BannerView(string adUnitId, AdSize adSize, int x, int y)
		{
			this.client = GoogleMobileAdsClientFactory.BuildBannerClient();
			this.client.CreateBannerView(adUnitId, adSize, x, y);
			this.configureBannerEvents();
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000248 RID: 584 RVA: 0x0000D83C File Offset: 0x0000BC3C
		// (remove) Token: 0x06000249 RID: 585 RVA: 0x0000D874 File Offset: 0x0000BC74
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600024A RID: 586 RVA: 0x0000D8AC File Offset: 0x0000BCAC
		// (remove) Token: 0x0600024B RID: 587 RVA: 0x0000D8E4 File Offset: 0x0000BCE4
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600024C RID: 588 RVA: 0x0000D91C File Offset: 0x0000BD1C
		// (remove) Token: 0x0600024D RID: 589 RVA: 0x0000D954 File Offset: 0x0000BD54
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600024E RID: 590 RVA: 0x0000D98C File Offset: 0x0000BD8C
		// (remove) Token: 0x0600024F RID: 591 RVA: 0x0000D9C4 File Offset: 0x0000BDC4
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000250 RID: 592 RVA: 0x0000D9FC File Offset: 0x0000BDFC
		// (remove) Token: 0x06000251 RID: 593 RVA: 0x0000DA34 File Offset: 0x0000BE34
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000252 RID: 594 RVA: 0x0000DA6A File Offset: 0x0000BE6A
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000DA78 File Offset: 0x0000BE78
		public void Hide()
		{
			this.client.HideBannerView();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DA85 File Offset: 0x0000BE85
		public void Show()
		{
			this.client.ShowBannerView();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000DA92 File Offset: 0x0000BE92
		public void Destroy()
		{
			this.client.DestroyBannerView();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000DAA0 File Offset: 0x0000BEA0
		private void configureBannerEvents()
		{
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

		// Token: 0x040001C0 RID: 448
		private IBannerClient client;
	}
}
