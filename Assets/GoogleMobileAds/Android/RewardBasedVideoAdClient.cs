using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000062 RID: 98
	internal class RewardBasedVideoAdClient : AndroidJavaProxy, IRewardBasedVideoAdClient
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000FEC8 File Offset: 0x0000E2C8
		public RewardBasedVideoAdClient() : base("com.google.unity.ads.UnityRewardBasedVideoAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.androidRewardBasedVideo = new AndroidJavaObject("com.google.unity.ads.RewardBasedVideo", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000386 RID: 902 RVA: 0x0001000C File Offset: 0x0000E40C
		// (remove) Token: 0x06000387 RID: 903 RVA: 0x00010044 File Offset: 0x0000E444
		public event EventHandler<EventArgs> OnAdLoaded = delegate
		{
		};

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000388 RID: 904 RVA: 0x0001007C File Offset: 0x0000E47C
		// (remove) Token: 0x06000389 RID: 905 RVA: 0x000100B4 File Offset: 0x0000E4B4
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad = delegate
		{
		};

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x0600038A RID: 906 RVA: 0x000100EC File Offset: 0x0000E4EC
		// (remove) Token: 0x0600038B RID: 907 RVA: 0x00010124 File Offset: 0x0000E524
		public event EventHandler<EventArgs> OnAdOpening = delegate
		{
		};

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600038C RID: 908 RVA: 0x0001015C File Offset: 0x0000E55C
		// (remove) Token: 0x0600038D RID: 909 RVA: 0x00010194 File Offset: 0x0000E594
		public event EventHandler<EventArgs> OnAdStarted = delegate
		{
		};

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600038E RID: 910 RVA: 0x000101CC File Offset: 0x0000E5CC
		// (remove) Token: 0x0600038F RID: 911 RVA: 0x00010204 File Offset: 0x0000E604
		public event EventHandler<EventArgs> OnAdClosed = delegate
		{
		};

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000390 RID: 912 RVA: 0x0001023C File Offset: 0x0000E63C
		// (remove) Token: 0x06000391 RID: 913 RVA: 0x00010274 File Offset: 0x0000E674
		public event EventHandler<Reward> OnAdRewarded = delegate
		{
		};

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000392 RID: 914 RVA: 0x000102AC File Offset: 0x0000E6AC
		// (remove) Token: 0x06000393 RID: 915 RVA: 0x000102E4 File Offset: 0x0000E6E4
		public event EventHandler<EventArgs> OnAdLeavingApplication = delegate
		{
		};

		// Token: 0x06000394 RID: 916 RVA: 0x0001031A File Offset: 0x0000E71A
		public void CreateRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("create", new object[0]);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00010332 File Offset: 0x0000E732
		public void LoadAd(AdRequest request, string adUnitId)
		{
			this.androidRewardBasedVideo.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request),
				adUnitId
			});
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010357 File Offset: 0x0000E757
		public bool IsLoaded()
		{
			return this.androidRewardBasedVideo.Call<bool>("isLoaded", new object[0]);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001036F File Offset: 0x0000E76F
		public void ShowRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("show", new object[0]);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00010387 File Offset: 0x0000E787
		public void DestroyRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("destroy", new object[0]);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001039F File Offset: 0x0000E79F
		private void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000103C0 File Offset: 0x0000E7C0
		private void onAdFailedToLoad(string errorReason)
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

		// Token: 0x0600039B RID: 923 RVA: 0x000103F4 File Offset: 0x0000E7F4
		private void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010412 File Offset: 0x0000E812
		private void onAdStarted()
		{
			if (this.OnAdStarted != null)
			{
				this.OnAdStarted(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00010430 File Offset: 0x0000E830
		private void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00010450 File Offset: 0x0000E850
		private void onAdRewarded(string type, float amount)
		{
			if (this.OnAdRewarded != null)
			{
				Reward e = new Reward
				{
					Type = type,
					Amount = (double)amount
				};
				this.OnAdRewarded(this, e);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001048C File Offset: 0x0000E88C
		private void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000209 RID: 521
		private AndroidJavaObject androidRewardBasedVideo;
	}
}
