using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200005C RID: 92
	public class AdLoaderClient : AndroidJavaProxy, IAdLoaderClient
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000EF10 File Offset: 0x0000D310
		public AdLoaderClient(AdLoader unityAdLoader) : base("com.google.unity.ads.UnityCustomNativeAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.adLoader = new AndroidJavaObject("com.google.unity.ads.NativeAdLoader", new object[]
			{
				@static,
				unityAdLoader.AdUnitId,
				this
			});
			this.CustomNativeTemplateCallbacks = unityAdLoader.CustomNativeTemplateClickHandlers;
			if (unityAdLoader.AdTypes.Contains(NativeAdType.CustomTemplate))
			{
				foreach (string text in unityAdLoader.TemplateIds)
				{
					this.adLoader.Call("configureCustomNativeTemplateAd", new object[]
					{
						text,
						this.CustomNativeTemplateCallbacks.ContainsKey(text)
					});
				}
			}
			this.adLoader.Call("create", new object[0]);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000F010 File Offset: 0x0000D410
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000F018 File Offset: 0x0000D418
		private Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateCallbacks { get; set; }

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000331 RID: 817 RVA: 0x0000F024 File Offset: 0x0000D424
		// (remove) Token: 0x06000332 RID: 818 RVA: 0x0000F05C File Offset: 0x0000D45C
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000333 RID: 819 RVA: 0x0000F094 File Offset: 0x0000D494
		// (remove) Token: 0x06000334 RID: 820 RVA: 0x0000F0CC File Offset: 0x0000D4CC
		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x06000335 RID: 821 RVA: 0x0000F102 File Offset: 0x0000D502
		public void LoadAd(AdRequest request)
		{
			this.adLoader.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F124 File Offset: 0x0000D524
		public void onCustomTemplateAdLoaded(AndroidJavaObject ad)
		{
			if (this.OnCustomNativeTemplateAdLoaded != null)
			{
				CustomNativeEventArgs e = new CustomNativeEventArgs
				{
					nativeAd = new CustomNativeTemplateAd(new CustomNativeTemplateClient(ad))
				};
				this.OnCustomNativeTemplateAdLoaded(this, e);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F164 File Offset: 0x0000D564
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

		// Token: 0x06000338 RID: 824 RVA: 0x0000F198 File Offset: 0x0000D598
		public void onCustomClick(AndroidJavaObject ad, string assetName)
		{
			CustomNativeTemplateAd customNativeTemplateAd = new CustomNativeTemplateAd(new CustomNativeTemplateClient(ad));
			this.CustomNativeTemplateCallbacks[customNativeTemplateAd.GetCustomTemplateId()](customNativeTemplateAd, assetName);
		}

		// Token: 0x040001F1 RID: 497
		private AndroidJavaObject adLoader;
	}
}
