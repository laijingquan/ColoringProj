using System;
using System.Collections.Generic;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Api
{
	// Token: 0x0200004B RID: 75
	public class CustomNativeTemplateAd
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000DBBB File Offset: 0x0000BFBB
		internal CustomNativeTemplateAd(ICustomNativeTemplateClient client)
		{
			this.client = client;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DBCA File Offset: 0x0000BFCA
		public List<string> GetAvailableAssetNames()
		{
			return this.client.GetAvailableAssetNames();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000DBD7 File Offset: 0x0000BFD7
		public string GetCustomTemplateId()
		{
			return this.client.GetTemplateId();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DBE4 File Offset: 0x0000BFE4
		public Texture2D GetTexture2D(string key)
		{
			byte[] imageByteArray = this.client.GetImageByteArray(key);
			if (imageByteArray == null)
			{
				return null;
			}
			return Utils.GetTexture2DFromByteArray(imageByteArray);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000DC0C File Offset: 0x0000C00C
		public string GetText(string key)
		{
			return this.client.GetText(key);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000DC1A File Offset: 0x0000C01A
		public void PerformClick(string assetName)
		{
			this.client.PerformClick(assetName);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000DC28 File Offset: 0x0000C028
		public void RecordImpression()
		{
			this.client.RecordImpression();
		}

		// Token: 0x040001C7 RID: 455
		private ICustomNativeTemplateClient client;
	}
}
