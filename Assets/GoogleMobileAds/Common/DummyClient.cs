using System;
using System.Reflection;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000054 RID: 84
	internal class DummyClient : IBannerClient, IInterstitialClient, IRewardBasedVideoAdClient, IAdLoaderClient, INativeExpressAdClient
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000E88D File Offset: 0x0000CC8D
		public DummyClient()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060002B7 RID: 695 RVA: 0x0000E8B0 File Offset: 0x0000CCB0
		// (remove) Token: 0x060002B8 RID: 696 RVA: 0x0000E8E8 File Offset: 0x0000CCE8
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060002B9 RID: 697 RVA: 0x0000E920 File Offset: 0x0000CD20
		// (remove) Token: 0x060002BA RID: 698 RVA: 0x0000E958 File Offset: 0x0000CD58
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060002BB RID: 699 RVA: 0x0000E990 File Offset: 0x0000CD90
		// (remove) Token: 0x060002BC RID: 700 RVA: 0x0000E9C8 File Offset: 0x0000CDC8
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060002BD RID: 701 RVA: 0x0000EA00 File Offset: 0x0000CE00
		// (remove) Token: 0x060002BE RID: 702 RVA: 0x0000EA38 File Offset: 0x0000CE38
		public event EventHandler<EventArgs> OnAdStarted;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060002BF RID: 703 RVA: 0x0000EA70 File Offset: 0x0000CE70
		// (remove) Token: 0x060002C0 RID: 704 RVA: 0x0000EAA8 File Offset: 0x0000CEA8
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060002C1 RID: 705 RVA: 0x0000EAE0 File Offset: 0x0000CEE0
		// (remove) Token: 0x060002C2 RID: 706 RVA: 0x0000EB18 File Offset: 0x0000CF18
		public event EventHandler<Reward> OnAdRewarded;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060002C3 RID: 707 RVA: 0x0000EB50 File Offset: 0x0000CF50
		// (remove) Token: 0x060002C4 RID: 708 RVA: 0x0000EB88 File Offset: 0x0000CF88
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060002C5 RID: 709 RVA: 0x0000EBC0 File Offset: 0x0000CFC0
		// (remove) Token: 0x060002C6 RID: 710 RVA: 0x0000EBF8 File Offset: 0x0000CFF8
		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000EC2E File Offset: 0x0000D02E
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000EC4E File Offset: 0x0000D04E
		public string UserId
		{
			get
			{
				Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
				return "UserId";
			}
			set
			{
				Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000EC69 File Offset: 0x0000D069
		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000EC84 File Offset: 0x0000D084
		public void CreateBannerView(string adUnitId, AdSize adSize, int positionX, int positionY)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000EC9F File Offset: 0x0000D09F
		public void LoadAd(AdRequest request)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000ECBA File Offset: 0x0000D0BA
		public void ShowBannerView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000ECD5 File Offset: 0x0000D0D5
		public void HideBannerView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000ECF0 File Offset: 0x0000D0F0
		public void DestroyBannerView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000ED0B File Offset: 0x0000D10B
		public void CreateInterstitialAd(string adUnitId)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000ED26 File Offset: 0x0000D126
		public bool IsLoaded()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
			return true;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000ED42 File Offset: 0x0000D142
		public void ShowInterstitial()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000ED5D File Offset: 0x0000D15D
		public void DestroyInterstitial()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000ED78 File Offset: 0x0000D178
		public void CreateRewardBasedVideoAd()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000ED93 File Offset: 0x0000D193
		public void SetUserId(string userId)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000EDAE File Offset: 0x0000D1AE
		public void LoadAd(AdRequest request, string adUnitId)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000EDC9 File Offset: 0x0000D1C9
		public void DestroyRewardBasedVideoAd()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000EDE4 File Offset: 0x0000D1E4
		public void ShowRewardBasedVideoAd()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000EDFF File Offset: 0x0000D1FF
		public void CreateAdLoader(AdLoader.Builder builder)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000EE1A File Offset: 0x0000D21A
		public void Load(AdRequest request)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000EE35 File Offset: 0x0000D235
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000EE50 File Offset: 0x0000D250
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, int positionX, int positionY)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000EE6B File Offset: 0x0000D26B
		public void SetAdSize(AdSize adSize)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000EE86 File Offset: 0x0000D286
		public void ShowNativeExpressAdView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000EEA1 File Offset: 0x0000D2A1
		public void HideNativeExpressAdView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000EEBC File Offset: 0x0000D2BC
		public void DestroyNativeExpressAdView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}
	}
}
