using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000055 RID: 85
	internal interface IAdLoaderClient
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060002E0 RID: 736
		// (remove) Token: 0x060002E1 RID: 737
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060002E2 RID: 738
		// (remove) Token: 0x060002E3 RID: 739
		event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x060002E4 RID: 740
		void LoadAd(AdRequest request);
	}
}
