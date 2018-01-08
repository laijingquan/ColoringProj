using System;
using System.Collections.Generic;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200005E RID: 94
	public class CustomNativeTemplateClient : ICustomNativeTemplateClient
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000F5C6 File Offset: 0x0000D9C6
		public CustomNativeTemplateClient(AndroidJavaObject customNativeAd)
		{
			this.customNativeAd = customNativeAd;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000F5D5 File Offset: 0x0000D9D5
		public List<string> GetAvailableAssetNames()
		{
			return new List<string>(this.customNativeAd.Call<string[]>("getAvailableAssetNames", new object[0]));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F5F2 File Offset: 0x0000D9F2
		public string GetTemplateId()
		{
			return this.customNativeAd.Call<string>("getTemplateId", new object[0]);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000F60C File Offset: 0x0000DA0C
		public byte[] GetImageByteArray(string key)
		{
			byte[] array = this.customNativeAd.Call<byte[]>("getImage", new object[]
			{
				key
			});
			if (array.Length == 0)
			{
				return null;
			}
			return array;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F640 File Offset: 0x0000DA40
		public string GetText(string key)
		{
			string text = this.customNativeAd.Call<string>("getText", new object[]
			{
				key
			});
			if (text.Equals(string.Empty))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F67B File Offset: 0x0000DA7B
		public void PerformClick(string assetName)
		{
			this.customNativeAd.Call("performClick", new object[]
			{
				assetName
			});
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F697 File Offset: 0x0000DA97
		public void RecordImpression()
		{
			this.customNativeAd.Call("recordImpression", new object[0]);
		}

		// Token: 0x040001FB RID: 507
		private AndroidJavaObject customNativeAd;
	}
}
