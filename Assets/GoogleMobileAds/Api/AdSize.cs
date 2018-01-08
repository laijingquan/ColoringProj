using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000048 RID: 72
	public class AdSize
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000D73A File Offset: 0x0000BB3A
		public AdSize(int width, int height)
		{
			this.isSmartBanner = false;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D757 File Offset: 0x0000BB57
		private AdSize(bool isSmartBanner)
		{
			this.isSmartBanner = isSmartBanner;
			this.width = 0;
			this.height = 0;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000D774 File Offset: 0x0000BB74
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000D77C File Offset: 0x0000BB7C
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000D784 File Offset: 0x0000BB84
		public bool IsSmartBanner
		{
			get
			{
				return this.isSmartBanner;
			}
		}

		// Token: 0x040001B8 RID: 440
		private bool isSmartBanner;

		// Token: 0x040001B9 RID: 441
		private int width;

		// Token: 0x040001BA RID: 442
		private int height;

		// Token: 0x040001BB RID: 443
		public static readonly AdSize Banner = new AdSize(320, 50);

		// Token: 0x040001BC RID: 444
		public static readonly AdSize MediumRectangle = new AdSize(300, 250);

		// Token: 0x040001BD RID: 445
		public static readonly AdSize IABBanner = new AdSize(468, 60);

		// Token: 0x040001BE RID: 446
		public static readonly AdSize Leaderboard = new AdSize(728, 90);

		// Token: 0x040001BF RID: 447
		public static readonly AdSize SmartBanner = new AdSize(true);
	}
}
