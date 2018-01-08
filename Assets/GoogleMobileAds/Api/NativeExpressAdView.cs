using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000051 RID: 81
	public class NativeExpressAdView
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000DFD6 File Offset: 0x0000C3D6
		public NativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.client = GoogleMobileAdsClientFactory.BuildNativeExpressAdClient();
			this.client.CreateNativeExpressAdView(adUnitId, adSize, position);
			this.configureNativeExpressAdEvents();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DFFD File Offset: 0x0000C3FD
		public NativeExpressAdView(string adUnitId, AdSize adSize, int x, int y)
		{
			this.client = GoogleMobileAdsClientFactory.BuildNativeExpressAdClient();
			this.client.CreateNativeExpressAdView(adUnitId, adSize, x, y);
			this.configureNativeExpressAdEvents();
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000283 RID: 643 RVA: 0x0000E028 File Offset: 0x0000C428
		// (remove) Token: 0x06000284 RID: 644 RVA: 0x0000E060 File Offset: 0x0000C460
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000285 RID: 645 RVA: 0x0000E098 File Offset: 0x0000C498
		// (remove) Token: 0x06000286 RID: 646 RVA: 0x0000E0D0 File Offset: 0x0000C4D0
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000287 RID: 647 RVA: 0x0000E108 File Offset: 0x0000C508
		// (remove) Token: 0x06000288 RID: 648 RVA: 0x0000E140 File Offset: 0x0000C540
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000289 RID: 649 RVA: 0x0000E178 File Offset: 0x0000C578
		// (remove) Token: 0x0600028A RID: 650 RVA: 0x0000E1B0 File Offset: 0x0000C5B0
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600028B RID: 651 RVA: 0x0000E1E8 File Offset: 0x0000C5E8
		// (remove) Token: 0x0600028C RID: 652 RVA: 0x0000E220 File Offset: 0x0000C620
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x0600028D RID: 653 RVA: 0x0000E256 File Offset: 0x0000C656
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E264 File Offset: 0x0000C664
		public void Hide()
		{
			this.client.HideNativeExpressAdView();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E271 File Offset: 0x0000C671
		public void Show()
		{
			this.client.ShowNativeExpressAdView();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E27E File Offset: 0x0000C67E
		public void Destroy()
		{
			this.client.DestroyNativeExpressAdView();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E28C File Offset: 0x0000C68C
		private void configureNativeExpressAdEvents()
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

		// Token: 0x040001D8 RID: 472
		private INativeExpressAdClient client;
	}
}
