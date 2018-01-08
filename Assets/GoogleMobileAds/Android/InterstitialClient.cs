using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000060 RID: 96
	internal class InterstitialClient : AndroidJavaProxy, IInterstitialClient
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000F6F8 File Offset: 0x0000DAF8
		public InterstitialClient() : base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.interstitial = new AndroidJavaObject("com.google.unity.ads.Interstitial", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600035A RID: 858 RVA: 0x0000F748 File Offset: 0x0000DB48
		// (remove) Token: 0x0600035B RID: 859 RVA: 0x0000F780 File Offset: 0x0000DB80
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600035C RID: 860 RVA: 0x0000F7B8 File Offset: 0x0000DBB8
		// (remove) Token: 0x0600035D RID: 861 RVA: 0x0000F7F0 File Offset: 0x0000DBF0
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600035E RID: 862 RVA: 0x0000F828 File Offset: 0x0000DC28
		// (remove) Token: 0x0600035F RID: 863 RVA: 0x0000F860 File Offset: 0x0000DC60
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000360 RID: 864 RVA: 0x0000F898 File Offset: 0x0000DC98
		// (remove) Token: 0x06000361 RID: 865 RVA: 0x0000F8D0 File Offset: 0x0000DCD0
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000362 RID: 866 RVA: 0x0000F908 File Offset: 0x0000DD08
		// (remove) Token: 0x06000363 RID: 867 RVA: 0x0000F940 File Offset: 0x0000DD40
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000364 RID: 868 RVA: 0x0000F976 File Offset: 0x0000DD76
		public void CreateInterstitialAd(string adUnitId)
		{
			this.interstitial.Call("create", new object[]
			{
				adUnitId
			});
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F992 File Offset: 0x0000DD92
		public void LoadAd(AdRequest request)
		{
			this.interstitial.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F9B3 File Offset: 0x0000DDB3
		public bool IsLoaded()
		{
			return this.interstitial.Call<bool>("isLoaded", new object[0]);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F9CB File Offset: 0x0000DDCB
		public void ShowInterstitial()
		{
			this.interstitial.Call("show", new object[0]);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F9E3 File Offset: 0x0000DDE3
		public void DestroyInterstitial()
		{
			this.interstitial.Call("destroy", new object[0]);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F9FB File Offset: 0x0000DDFB
		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000FA1C File Offset: 0x0000DE1C
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

		// Token: 0x0600036B RID: 875 RVA: 0x0000FA50 File Offset: 0x0000DE50
		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000FA6E File Offset: 0x0000DE6E
		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000FA8C File Offset: 0x0000DE8C
		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x040001FD RID: 509
		private AndroidJavaObject interstitial;
	}
}
