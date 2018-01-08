using System;
using GoogleMobileAds.Android;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds
{
	// Token: 0x02000064 RID: 100
	internal class GoogleMobileAdsClientFactory
	{
		// Token: 0x060003AB RID: 939 RVA: 0x000109C0 File Offset: 0x0000EDC0
		internal static IBannerClient BuildBannerClient()
		{
			return new BannerClient();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000109C7 File Offset: 0x0000EDC7
		internal static IInterstitialClient BuildInterstitialClient()
		{
			return new InterstitialClient();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000109CE File Offset: 0x0000EDCE
		internal static IRewardBasedVideoAdClient BuildRewardBasedVideoAdClient()
		{
			return new RewardBasedVideoAdClient();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000109D5 File Offset: 0x0000EDD5
		internal static IAdLoaderClient BuildAdLoaderClient(AdLoader adLoader)
		{
			return new AdLoaderClient(adLoader);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000109DD File Offset: 0x0000EDDD
		internal static INativeExpressAdClient BuildNativeExpressAdClient()
		{
			return new NativeExpressAdClient();
		}
	}
}
