using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200005D RID: 93
	internal class BannerClient : AndroidJavaProxy, IBannerClient
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000F1CC File Offset: 0x0000D5CC
		public BannerClient() : base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.bannerView = new AndroidJavaObject("com.google.unity.ads.Banner", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600033A RID: 826 RVA: 0x0000F21C File Offset: 0x0000D61C
		// (remove) Token: 0x0600033B RID: 827 RVA: 0x0000F254 File Offset: 0x0000D654
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600033C RID: 828 RVA: 0x0000F28C File Offset: 0x0000D68C
		// (remove) Token: 0x0600033D RID: 829 RVA: 0x0000F2C4 File Offset: 0x0000D6C4
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600033E RID: 830 RVA: 0x0000F2FC File Offset: 0x0000D6FC
		// (remove) Token: 0x0600033F RID: 831 RVA: 0x0000F334 File Offset: 0x0000D734
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000340 RID: 832 RVA: 0x0000F36C File Offset: 0x0000D76C
		// (remove) Token: 0x06000341 RID: 833 RVA: 0x0000F3A4 File Offset: 0x0000D7A4
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000342 RID: 834 RVA: 0x0000F3DC File Offset: 0x0000D7DC
		// (remove) Token: 0x06000343 RID: 835 RVA: 0x0000F414 File Offset: 0x0000D814
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000344 RID: 836 RVA: 0x0000F44A File Offset: 0x0000D84A
		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.bannerView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				(int)position
			});
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F478 File Offset: 0x0000D878
		public void CreateBannerView(string adUnitId, AdSize adSize, int x, int y)
		{
			this.bannerView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				x,
				y
			});
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F4B0 File Offset: 0x0000D8B0
		public void LoadAd(AdRequest request)
		{
			this.bannerView.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F4D1 File Offset: 0x0000D8D1
		public void ShowBannerView()
		{
			this.bannerView.Call("show", new object[0]);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F4E9 File Offset: 0x0000D8E9
		public void HideBannerView()
		{
			this.bannerView.Call("hide", new object[0]);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000F501 File Offset: 0x0000D901
		public void DestroyBannerView()
		{
			this.bannerView.Call("destroy", new object[0]);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000F519 File Offset: 0x0000D919
		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000F538 File Offset: 0x0000D938
		public void onAdFailedToLoad(string errorReason)
		{
			if (this.OnAdFailedToLoad != null)
			{
				AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
				{
					Message = errorReason
				};
				this.OnAdFailedToLoad(this, e);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F56C File Offset: 0x0000D96C
		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F58A File Offset: 0x0000D98A
		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000F5A8 File Offset: 0x0000D9A8
		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x040001F5 RID: 501
		private AndroidJavaObject bannerView;
	}
}
