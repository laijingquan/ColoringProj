using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200005F RID: 95
	internal class CustomInAppPurchase : ICustomInAppPurchase
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000F6AF File Offset: 0x0000DAAF
		public CustomInAppPurchase(AndroidJavaObject purchase)
		{
			this.purchase = purchase;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000F6BE File Offset: 0x0000DABE
		public string ProductId
		{
			get
			{
				return this.purchase.Call<string>("getProductId", new object[0]);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F6D6 File Offset: 0x0000DAD6
		public void RecordResolution(PurchaseResolutionType resolution)
		{
			this.purchase.Call("recordResolution", new object[]
			{
				(int)resolution
			});
		}

		// Token: 0x040001FC RID: 508
		private AndroidJavaObject purchase;
	}
}
