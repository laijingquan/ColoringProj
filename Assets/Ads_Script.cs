using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class Ads_Script : MonoBehaviour
{
	// Token: 0x060003D3 RID: 979 RVA: 0x00011954 File Offset: 0x0000FD54
	private void Start()
	{
		Ads_Script.current = this;
		if (GlobalValues.removeAds == 0)
		{
			base.Invoke("RequestBanner1", 0.5f);
			base.Invoke("ShowBanner1", 1f);
		}
		this.ads.RequestInterstitial();
		GoogleMobileAdsDemoScript.BannerChangedEvent += this.BannerEvent;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x000119AD File Offset: 0x0000FDAD
	public void BannerEvent(bool status)
	{
		this.bannerIsOnScreen = status;
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x000119B6 File Offset: 0x0000FDB6
	private void Show_Interstitial_Ads()
	{
		if (GlobalValues.removeAds == 0)
		{
			this.ads.ShowInterstitial();
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x000119CD File Offset: 0x0000FDCD
	private void Show_Video_Ads()
	{
		this.ads.ShowRewardBasedVideo();
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x000119DA File Offset: 0x0000FDDA
	private void RequestBanner1()
	{
		this.ads.RequestBanner1();
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x000119E7 File Offset: 0x0000FDE7
	private void RequestBanner2()
	{
		this.ads.RequestBanner2();
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x000119F4 File Offset: 0x0000FDF4
	private void Update()
	{
		if (Ads_Script.Interstitial_Shown)
		{
			Ads_Script.Interstitial_Shown = false;
			this.ads.RequestInterstitial();
		}
		if (Ads_Script.Reward_Video_Shown)
		{
			Ads_Script.Reward_Video_Shown = false;
			this.ads.RequestRewardBasedVideo();
		}
		if (GlobalValues.removeAds == 1 && !Ads_Script.Hide_Banner)
		{
			Ads_Script.Hide_Banner = true;
			this.HideBanner1();
			this.HideBanner2();
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00011A5E File Offset: 0x0000FE5E
	public void HideBanner1()
	{
		this.ads.HideBanner1();
		Ads_Script.DontShowOnLoad1 = true;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00011A71 File Offset: 0x0000FE71
	public void HideBanner2()
	{
		this.ads.HideBanner2();
		Ads_Script.DontShowOnLoad2 = true;
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00011A84 File Offset: 0x0000FE84
	public void ShowBanner1()
	{
		this.ads.ShowBanner1();
		Ads_Script.DontShowOnLoad1 = false;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00011A97 File Offset: 0x0000FE97
	public void ShowBanner2()
	{
		this.ads.ShowBanner2();
		Ads_Script.DontShowOnLoad2 = false;
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00011AAA File Offset: 0x0000FEAA
	public bool IsInterReady()
	{
		return this.ads.isInterReady();
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00011AB7 File Offset: 0x0000FEB7
	public void ShowFullScreenAd()
	{
		if (GlobalValues.removeAds != 1)
		{
			if (this.IsInterReady())
			{
				this.Show_Interstitial_Ads();
			}
			else if (this.ads.checkIsVideoLoaded())
			{
				this.Show_Video_Ads();
			}
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00011AF5 File Offset: 0x0000FEF5
	public bool ShowRewardedVideoAd()
	{
		if (this.ads.checkIsVideoLoaded())
		{
			this.ads.ShowRewardBasedVideo();
			return true;
		}
		if (this.IsInterReady())
		{
			Ads_Script.rewardInterstitalAd = true;
			this.ads.ShowInterstitial();
		}
		return true;
	}

	// Token: 0x04000238 RID: 568
	public GoogleMobileAdsDemoScript ads;

	// Token: 0x04000239 RID: 569
	public static bool Need_Interstitial_Ads;

	// Token: 0x0400023A RID: 570
	public static bool Need_video_Ads;

	// Token: 0x0400023B RID: 571
	public static bool Hide_Banner;

	// Token: 0x0400023C RID: 572
	public static bool Banner_Shown;

	// Token: 0x0400023D RID: 573
	public static bool IsBanner_Loaded;

	// Token: 0x0400023E RID: 574
	public static bool Interstitial_Shown;

	// Token: 0x0400023F RID: 575
	public static bool Reward_Video_Shown;

	// Token: 0x04000240 RID: 576
	public static bool DontShowOnLoad1;

	// Token: 0x04000241 RID: 577
	public static bool DontShowOnLoad2 = true;

	// Token: 0x04000242 RID: 578
	public bool isBannerVisible1;

	// Token: 0x04000243 RID: 579
	public bool isBannerVisible2;

	// Token: 0x04000244 RID: 580
	public static Ads_Script current;

	// Token: 0x04000245 RID: 581
	public bool bannerIsOnScreen;

	// Token: 0x04000246 RID: 582
	public static bool rewardInterstitalAd;
}
