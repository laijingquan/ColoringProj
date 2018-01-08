using System;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class GoogleMobileAdsDemoScript : MonoBehaviour
{
	// Token: 0x14000055 RID: 85
	// (add) Token: 0x06000434 RID: 1076 RVA: 0x00012A0C File Offset: 0x00010E0C
	// (remove) Token: 0x06000435 RID: 1077 RVA: 0x00012A40 File Offset: 0x00010E40
	public static event GoogleMobileAdsDemoScript.VideoWatched VideoDone;

	// Token: 0x14000056 RID: 86
	// (add) Token: 0x06000436 RID: 1078 RVA: 0x00012A74 File Offset: 0x00010E74
	// (remove) Token: 0x06000437 RID: 1079 RVA: 0x00012AA8 File Offset: 0x00010EA8
	public static event GoogleMobileAdsDemoScript.BannerDelegate BannerChangedEvent;

	// Token: 0x1700005A RID: 90
	// (set) Token: 0x06000438 RID: 1080 RVA: 0x00012ADC File Offset: 0x00010EDC
	public static string OutputMessage
	{
		set
		{
			GoogleMobileAdsDemoScript.outputMessage = value;
		}
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00012AE4 File Offset: 0x00010EE4
	private void Start()
	{
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
		this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
		this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
		this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
		this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
		this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
		this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
		this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
		this.RequestRewardBasedVideo();
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00012BA4 File Offset: 0x00010FA4
	public void RequestBanner1()
	{
		if (this.bannerView1 != null)
		{
			this.bannerView1.Hide();
		}
		string adUnitId = "ca-app-pub-4127594339869165/2690473731";
		this.bannerView1 = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		this.bannerView1.OnAdLoaded += this.HandleAdLoaded1;
		this.bannerView1.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		this.bannerView1.OnAdLoaded += this.HandleAdOpened1;
		this.bannerView1.OnAdClosed += this.HandleAdClosed1;
		this.bannerView1.OnAdLeavingApplication += this.HandleAdLeftApplication;
		this.bannerView1.LoadAd(this.createAdRequest());
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00012C64 File Offset: 0x00011064
	public void RequestBanner2()
	{
		if (this.bannerView2 != null)
		{
			this.bannerView2.Hide();
		}
		string adUnitId = "ca-app-pub-4127594339869165/2690473731";
		this.bannerView2 = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		this.bannerView2.OnAdLoaded += this.HandleAdLoaded2;
		this.bannerView2.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		this.bannerView2.OnAdLoaded += this.HandleAdOpened2;
		this.bannerView2.OnAdClosed += this.HandleAdClosed2;
		this.bannerView2.OnAdLeavingApplication += this.HandleAdLeftApplication;
		this.bannerView2.LoadAd(this.createAdRequest());
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00012D23 File Offset: 0x00011123
	public void ShowBanner1()
	{
		if (this.bannerView1 != null)
		{
			this.bannerView1.Show();
		}
		Ads_Script.Banner_Shown = true;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00012D41 File Offset: 0x00011141
	public void ShowBanner2()
	{
		if (this.bannerView2 != null)
		{
			this.bannerView2.Show();
		}
		Ads_Script.Banner_Shown = true;
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00012D5F File Offset: 0x0001115F
	public void HideBanner1()
	{
		if (this.bannerView1 != null)
		{
			this.bannerView1.Hide();
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00012D77 File Offset: 0x00011177
	public void HideBanner2()
	{
		if (this.bannerView2 != null)
		{
			this.bannerView2.Hide();
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00012D90 File Offset: 0x00011190
	public void RequestInterstitial()
	{
		string adUnitId = "ca-app-pub-4127594339869165/4167206931";
		this.interstitial = new InterstitialAd(adUnitId);
		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
		this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;
		this.interstitial.LoadAd(this.createAdRequest());
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00012E33 File Offset: 0x00011233
	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder().Build();
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00012E40 File Offset: 0x00011240
	public void RequestRewardBasedVideo()
	{
		string adUnitId = "ca-app-pub-4127594339869165/1339013004";
		this.rewardBasedVideo.LoadAd(this.createAdRequest(), adUnitId);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00012E65 File Offset: 0x00011265
	public bool isInterReady()
	{
		return this.interstitial != null && this.interstitial.IsLoaded();
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00012E7F File Offset: 0x0001127F
	public void ShowInterstitial()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
		else
		{
			MonoBehaviour.print("Interstitial is not ready yet.");
		}
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00012EAB File Offset: 0x000112AB
	public bool checkInter()
	{
		return this.interstitial.IsLoaded();
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00012EB8 File Offset: 0x000112B8
	public bool checkIsVideoLoaded()
	{
		return this.rewardBasedVideo.IsLoaded();
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00012EC5 File Offset: 0x000112C5
	public void ShowRewardBasedVideo()
	{
		if (this.rewardBasedVideo.IsLoaded())
		{
			this.rewardBasedVideo.Show();
		}
		else
		{
			MonoBehaviour.print("Reward based video ad is not ready yet.");
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00012EF1 File Offset: 0x000112F1
	public void HandleAdLoaded1(object sender, EventArgs args)
	{
		Ads_Script.IsBanner_Loaded = true;
		if (Ads_Script.DontShowOnLoad1 && this.bannerView1 != null)
		{
			this.HideBanner1();
		}
		MonoBehaviour.print("HandleAdLoaded event received.");
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00012F23 File Offset: 0x00011323
	public void HandleAdLoaded2(object sender, EventArgs args)
	{
		Ads_Script.IsBanner_Loaded = true;
		if (Ads_Script.DontShowOnLoad2 && this.bannerView2 != null)
		{
			this.HideBanner2();
		}
		MonoBehaviour.print("HandleAdLoaded event received.");
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00012F55 File Offset: 0x00011355
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00012F6C File Offset: 0x0001136C
	public void HandleAdOpened1(object sender, EventArgs args)
	{
		if (GoogleMobileAdsDemoScript.BannerChangedEvent != null)
		{
			GoogleMobileAdsDemoScript.BannerChangedEvent(true);
		}
		Ads_Script.current.isBannerVisible1 = true;
		MonoBehaviour.print("HandleAdOpened event received");
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00012F98 File Offset: 0x00011398
	public void HandleAdOpened2(object sender, EventArgs args)
	{
		if (GoogleMobileAdsDemoScript.BannerChangedEvent != null)
		{
			GoogleMobileAdsDemoScript.BannerChangedEvent(true);
		}
		Ads_Script.current.isBannerVisible2 = true;
		MonoBehaviour.print("HandleAdOpened event received");
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00012FC4 File Offset: 0x000113C4
	private void HandleAdClosing(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosing event received");
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00012FD0 File Offset: 0x000113D0
	public void HandleAdClosed1(object sender, EventArgs args)
	{
		Ads_Script.current.isBannerVisible1 = false;
		if (GoogleMobileAdsDemoScript.BannerChangedEvent != null)
		{
			GoogleMobileAdsDemoScript.BannerChangedEvent(false);
		}
		MonoBehaviour.print("HandleAdClosed event received");
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00012FFC File Offset: 0x000113FC
	public void HandleAdClosed2(object sender, EventArgs args)
	{
		Ads_Script.current.isBannerVisible2 = false;
		if (GoogleMobileAdsDemoScript.BannerChangedEvent != null)
		{
			GoogleMobileAdsDemoScript.BannerChangedEvent(false);
		}
		MonoBehaviour.print("HandleAdClosed event received");
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x00013028 File Offset: 0x00011428
	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		if (GoogleMobileAdsDemoScript.BannerChangedEvent != null)
		{
			GoogleMobileAdsDemoScript.BannerChangedEvent(false);
		}
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x00013049 File Offset: 0x00011449
	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLoaded event received.");
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00013055 File Offset: 0x00011455
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0001306C File Offset: 0x0001146C
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialOpened event received");
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00013078 File Offset: 0x00011478
	private void HandleInterstitialClosing(object sender, EventArgs args)
	{
		Ads_Script.Interstitial_Shown = true;
		MonoBehaviour.print("HandleInterstitialClosing event received");
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0001308A File Offset: 0x0001148A
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialClosed event received");
		if (Ads_Script.rewardInterstitalAd)
		{
			Ads_Script.Interstitial_Shown = false;
			if (GoogleMobileAdsDemoScript.VideoDone != null)
			{
				GoogleMobileAdsDemoScript.VideoDone(true);
			}
		}
		Ads_Script.Interstitial_Shown = true;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x000130C1 File Offset: 0x000114C1
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x000130CD File Offset: 0x000114CD
	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received.");
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x000130D9 File Offset: 0x000114D9
	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x000130F0 File Offset: 0x000114F0
	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x000130FC File Offset: 0x000114FC
	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00013108 File Offset: 0x00011508
	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		Ads_Script.Reward_Video_Shown = true;
		if (GoogleMobileAdsDemoScript.VideoDone != null)
		{
			GoogleMobileAdsDemoScript.VideoDone(false);
		}
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x00013130 File Offset: 0x00011530
	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		if (GoogleMobileAdsDemoScript.VideoDone != null)
		{
			GoogleMobileAdsDemoScript.VideoDone(true);
		}
		Debug.Log("Rewarded..");
		string type = args.Type;
		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + args.Amount.ToString() + " " + type);
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0001318C File Offset: 0x0001158C
	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

	// Token: 0x04000269 RID: 617
	private BannerView bannerView1;

	// Token: 0x0400026A RID: 618
	private BannerView bannerView2;

	// Token: 0x0400026B RID: 619
	private InterstitialAd interstitial;

	// Token: 0x0400026C RID: 620
	private RewardBasedVideoAd rewardBasedVideo;

	// Token: 0x0400026D RID: 621
	private float deltaTime;

	// Token: 0x0400026E RID: 622
	private static string outputMessage = string.Empty;

	// Token: 0x02000075 RID: 117
	// (Invoke) Token: 0x06000460 RID: 1120
	public delegate void VideoWatched(bool status);

	// Token: 0x02000076 RID: 118
	// (Invoke) Token: 0x06000464 RID: 1124
	public delegate void BannerDelegate(bool status);
}
