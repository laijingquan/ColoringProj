using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000063 RID: 99
	internal class Utils
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x000104C0 File Offset: 0x0000E8C0
		public static AndroidJavaObject GetAdSizeJavaObject(AdSize adSize)
		{
			if (adSize.IsSmartBanner)
			{
				return new AndroidJavaClass("com.google.android.gms.ads.AdSize").GetStatic<AndroidJavaObject>("SMART_BANNER");
			}
			return new AndroidJavaObject("com.google.android.gms.ads.AdSize", new object[]
			{
				adSize.Width,
				adSize.Height
			});
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001051C File Offset: 0x0000E91C
		public static AndroidJavaObject GetAdRequestJavaObject(AdRequest request)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.google.android.gms.ads.AdRequest$Builder", new object[0]);
			foreach (string text in request.Keywords)
			{
				androidJavaObject.Call<AndroidJavaObject>("addKeyword", new object[]
				{
					text
				});
			}
			foreach (string text2 in request.TestDevices)
			{
				if (text2 == "SIMULATOR")
				{
					string @static = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<string>("DEVICE_ID_EMULATOR");
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[]
					{
						@static
					});
				}
				else
				{
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[]
					{
						text2
					});
				}
			}
			if (request.Birthday != null)
			{
				DateTime valueOrDefault = request.Birthday.GetValueOrDefault();
				AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.util.Date", new object[]
				{
					valueOrDefault.Year,
					valueOrDefault.Month,
					valueOrDefault.Day
				});
				androidJavaObject.Call<AndroidJavaObject>("setBirthday", new object[]
				{
					androidJavaObject2
				});
			}
			if (request.Gender != null)
			{
				int? num = null;
				Gender valueOrDefault2 = request.Gender.GetValueOrDefault();
				if (valueOrDefault2 != Gender.Unknown)
				{
					if (valueOrDefault2 != Gender.Male)
					{
						if (valueOrDefault2 == Gender.Female)
						{
							num = new int?(new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_FEMALE"));
						}
					}
					else
					{
						num = new int?(new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_MALE"));
					}
				}
				else
				{
					num = new int?(new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_UNKNOWN"));
				}
				if (num != null)
				{
					androidJavaObject.Call<AndroidJavaObject>("setGender", new object[]
					{
						num
					});
				}
			}
			if (request.TagForChildDirectedTreatment != null)
			{
				androidJavaObject.Call<AndroidJavaObject>("tagForChildDirectedTreatment", new object[]
				{
					request.TagForChildDirectedTreatment.GetValueOrDefault()
				});
			}
			androidJavaObject.Call<AndroidJavaObject>("setRequestAgent", new object[]
			{
				"unity-3.4.0"
			});
			AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("android.os.Bundle", new object[0]);
			foreach (KeyValuePair<string, string> keyValuePair in request.Extras)
			{
				androidJavaObject3.Call("putString", new object[]
				{
					keyValuePair.Key,
					keyValuePair.Value
				});
			}
			AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("com.google.android.gms.ads.mediation.admob.AdMobExtras", new object[]
			{
				androidJavaObject3
			});
			androidJavaObject.Call<AndroidJavaObject>("addNetworkExtras", new object[]
			{
				androidJavaObject4
			});
			foreach (MediationExtras mediationExtras in request.MediationExtras)
			{
				AndroidJavaObject androidJavaObject5 = new AndroidJavaObject(mediationExtras.AndroidMediationExtraBuilderClassName, new object[0]);
				AndroidJavaObject androidJavaObject6 = new AndroidJavaObject("java.util.HashMap", new object[0]);
				foreach (KeyValuePair<string, string> keyValuePair2 in mediationExtras.Extras)
				{
					androidJavaObject6.Call<string>("put", new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
				AndroidJavaObject androidJavaObject7 = androidJavaObject5.Call<AndroidJavaObject>("buildExtras", new object[]
				{
					androidJavaObject6
				});
				androidJavaObject.Call<AndroidJavaObject>("addNetworkExtrasBundle", new object[]
				{
					androidJavaObject5.Call<AndroidJavaClass>("getAdapterClass", new object[0]),
					androidJavaObject7
				});
			}
			return androidJavaObject.Call<AndroidJavaObject>("build", new object[0]);
		}

		// Token: 0x04000218 RID: 536
		public const string AdListenerClassName = "com.google.android.gms.ads.AdListener";

		// Token: 0x04000219 RID: 537
		public const string AdRequestClassName = "com.google.android.gms.ads.AdRequest";

		// Token: 0x0400021A RID: 538
		public const string AdRequestBuilderClassName = "com.google.android.gms.ads.AdRequest$Builder";

		// Token: 0x0400021B RID: 539
		public const string AdSizeClassName = "com.google.android.gms.ads.AdSize";

		// Token: 0x0400021C RID: 540
		public const string AdMobExtrasClassName = "com.google.android.gms.ads.mediation.admob.AdMobExtras";

		// Token: 0x0400021D RID: 541
		public const string PlayStorePurchaseListenerClassName = "com.google.android.gms.ads.purchase.PlayStorePurchaseListener";

		// Token: 0x0400021E RID: 542
		public const string InAppPurchaseListenerClassName = "com.google.android.gms.ads.purchase.InAppPurchaseListener";

		// Token: 0x0400021F RID: 543
		public const string BannerViewClassName = "com.google.unity.ads.Banner";

		// Token: 0x04000220 RID: 544
		public const string InterstitialClassName = "com.google.unity.ads.Interstitial";

		// Token: 0x04000221 RID: 545
		public const string RewardBasedVideoClassName = "com.google.unity.ads.RewardBasedVideo";

		// Token: 0x04000222 RID: 546
		public const string NativeExpressAdViewClassName = "com.google.unity.ads.NativeExpressAd";

		// Token: 0x04000223 RID: 547
		public const string NativeAdLoaderClassName = "com.google.unity.ads.NativeAdLoader";

		// Token: 0x04000224 RID: 548
		public const string UnityAdListenerClassName = "com.google.unity.ads.UnityAdListener";

		// Token: 0x04000225 RID: 549
		public const string UnityRewardBasedVideoAdListenerClassName = "com.google.unity.ads.UnityRewardBasedVideoAdListener";

		// Token: 0x04000226 RID: 550
		public const string UnityCustomNativeAdListener = "com.google.unity.ads.UnityCustomNativeAdListener";

		// Token: 0x04000227 RID: 551
		public const string PluginUtilsClassName = "com.google.unity.ads.PluginUtils";

		// Token: 0x04000228 RID: 552
		public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";

		// Token: 0x04000229 RID: 553
		public const string BundleClassName = "android.os.Bundle";

		// Token: 0x0400022A RID: 554
		public const string DateClassName = "java.util.Date";
	}
}
