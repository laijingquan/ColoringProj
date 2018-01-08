using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000057 RID: 87
	internal interface ICustomNativeTemplateClient
	{
		// Token: 0x060002F5 RID: 757
		string GetTemplateId();

		// Token: 0x060002F6 RID: 758
		byte[] GetImageByteArray(string key);

		// Token: 0x060002F7 RID: 759
		List<string> GetAvailableAssetNames();

		// Token: 0x060002F8 RID: 760
		string GetText(string key);

		// Token: 0x060002F9 RID: 761
		void PerformClick(string assetName);

		// Token: 0x060002FA RID: 762
		void RecordImpression();
	}
}
