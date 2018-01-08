using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000061 RID: 97
	internal class NativeExpressAdClient : AndroidJavaProxy, INativeExpressAdClient
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0000FAAC File Offset: 0x0000DEAC
		public NativeExpressAdClient() : base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.nativeExpressAdView = new AndroidJavaObject("com.google.unity.ads.NativeExpressAd", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x0600036F RID: 879 RVA: 0x0000FAFC File Offset: 0x0000DEFC
		// (remove) Token: 0x06000370 RID: 880 RVA: 0x0000FB34 File Offset: 0x0000DF34
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000371 RID: 881 RVA: 0x0000FB6C File Offset: 0x0000DF6C
		// (remove) Token: 0x06000372 RID: 882 RVA: 0x0000FBA4 File Offset: 0x0000DFA4
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000373 RID: 883 RVA: 0x0000FBDC File Offset: 0x0000DFDC
		// (remove) Token: 0x06000374 RID: 884 RVA: 0x0000FC14 File Offset: 0x0000E014
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000375 RID: 885 RVA: 0x0000FC4C File Offset: 0x0000E04C
		// (remove) Token: 0x06000376 RID: 886 RVA: 0x0000FC84 File Offset: 0x0000E084
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000377 RID: 887 RVA: 0x0000FCBC File Offset: 0x0000E0BC
		// (remove) Token: 0x06000378 RID: 888 RVA: 0x0000FCF4 File Offset: 0x0000E0F4
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000379 RID: 889 RVA: 0x0000FD2A File Offset: 0x0000E12A
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.nativeExpressAdView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				(int)position
			});
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000FD58 File Offset: 0x0000E158
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, int x, int y)
		{
			this.nativeExpressAdView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				x,
				y
			});
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000FD90 File Offset: 0x0000E190
		public void LoadAd(AdRequest request)
		{
			this.nativeExpressAdView.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000FDB1 File Offset: 0x0000E1B1
		public void SetAdSize(AdSize adSize)
		{
			this.nativeExpressAdView.Call("setAdSize", new object[]
			{
				Utils.GetAdSizeJavaObject(adSize)
			});
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000FDD2 File Offset: 0x0000E1D2
		public void ShowNativeExpressAdView()
		{
			this.nativeExpressAdView.Call("show", new object[0]);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000FDEA File Offset: 0x0000E1EA
		public void HideNativeExpressAdView()
		{
			this.nativeExpressAdView.Call("hide", new object[0]);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000FE02 File Offset: 0x0000E202
		public void DestroyNativeExpressAdView()
		{
			this.nativeExpressAdView.Call("destroy", new object[0]);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000FE1A File Offset: 0x0000E21A
		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000FE38 File Offset: 0x0000E238
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

		// Token: 0x06000382 RID: 898 RVA: 0x0000FE6C File Offset: 0x0000E26C
		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000FE8A File Offset: 0x0000E28A
		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000FEA8 File Offset: 0x0000E2A8
		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000203 RID: 515
		private AndroidJavaObject nativeExpressAdView;
	}
}
