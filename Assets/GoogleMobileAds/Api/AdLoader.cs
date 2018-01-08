using System;
using System.Collections.Generic;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000043 RID: 67
	public class AdLoader
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000D238 File Offset: 0x0000B638
		private AdLoader(AdLoader.Builder builder)
		{
			this.AdUnitId = string.Copy(builder.AdUnitId);
			this.CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>(builder.CustomNativeTemplateClickHandlers);
			this.TemplateIds = new HashSet<string>(builder.TemplateIds);
			this.AdTypes = new HashSet<NativeAdType>(builder.AdTypes);
			this.adLoaderClient = GoogleMobileAdsClientFactory.BuildAdLoaderClient(this);
			this.adLoaderClient.OnCustomNativeTemplateAdLoaded += delegate(object sender, CustomNativeEventArgs args)
			{
				if (this.OnCustomNativeTemplateAdLoaded != null)
				{
					this.OnCustomNativeTemplateAdLoaded(this, args);
				}
			};
			this.adLoaderClient.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
			{
				if (this.OnAdFailedToLoad != null)
				{
					this.OnAdFailedToLoad(this, args);
				}
			};
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001FF RID: 511 RVA: 0x0000D2CC File Offset: 0x0000B6CC
		// (remove) Token: 0x06000200 RID: 512 RVA: 0x0000D304 File Offset: 0x0000B704
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000201 RID: 513 RVA: 0x0000D33C File Offset: 0x0000B73C
		// (remove) Token: 0x06000202 RID: 514 RVA: 0x0000D374 File Offset: 0x0000B774
		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000D3AA File Offset: 0x0000B7AA
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000D3B2 File Offset: 0x0000B7B2
		public Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000D3BB File Offset: 0x0000B7BB
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000D3C3 File Offset: 0x0000B7C3
		public string AdUnitId { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000D3CC File Offset: 0x0000B7CC
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000D3D4 File Offset: 0x0000B7D4
		public HashSet<NativeAdType> AdTypes { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000D3DD File Offset: 0x0000B7DD
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000D3E5 File Offset: 0x0000B7E5
		public HashSet<string> TemplateIds { get; private set; }

		// Token: 0x0600020B RID: 523 RVA: 0x0000D3EE File Offset: 0x0000B7EE
		public void LoadAd(AdRequest request)
		{
			this.adLoaderClient.LoadAd(request);
		}

		// Token: 0x04000195 RID: 405
		private IAdLoaderClient adLoaderClient;

		// Token: 0x02000044 RID: 68
		public class Builder
		{
			// Token: 0x0600020E RID: 526 RVA: 0x0000D430 File Offset: 0x0000B830
			public Builder(string adUnitId)
			{
				this.AdUnitId = adUnitId;
				this.AdTypes = new HashSet<NativeAdType>();
				this.TemplateIds = new HashSet<string>();
				this.CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>();
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x0600020F RID: 527 RVA: 0x0000D460 File Offset: 0x0000B860
			// (set) Token: 0x06000210 RID: 528 RVA: 0x0000D468 File Offset: 0x0000B868
			internal string AdUnitId { get; private set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000211 RID: 529 RVA: 0x0000D471 File Offset: 0x0000B871
			// (set) Token: 0x06000212 RID: 530 RVA: 0x0000D479 File Offset: 0x0000B879
			internal HashSet<NativeAdType> AdTypes { get; private set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000213 RID: 531 RVA: 0x0000D482 File Offset: 0x0000B882
			// (set) Token: 0x06000214 RID: 532 RVA: 0x0000D48A File Offset: 0x0000B88A
			internal HashSet<string> TemplateIds { get; private set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000215 RID: 533 RVA: 0x0000D493 File Offset: 0x0000B893
			// (set) Token: 0x06000216 RID: 534 RVA: 0x0000D49B File Offset: 0x0000B89B
			internal Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers { get; private set; }

			// Token: 0x06000217 RID: 535 RVA: 0x0000D4A4 File Offset: 0x0000B8A4
			public AdLoader.Builder ForCustomNativeAd(string templateId)
			{
				this.TemplateIds.Add(templateId);
				this.AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			// Token: 0x06000218 RID: 536 RVA: 0x0000D4C1 File Offset: 0x0000B8C1
			public AdLoader.Builder ForCustomNativeAd(string templateId, Action<CustomNativeTemplateAd, string> callback)
			{
				this.TemplateIds.Add(templateId);
				this.CustomNativeTemplateClickHandlers[templateId] = callback;
				this.AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			// Token: 0x06000219 RID: 537 RVA: 0x0000D4EB File Offset: 0x0000B8EB
			public AdLoader Build()
			{
				return new AdLoader(this);
			}
		}
	}
}
