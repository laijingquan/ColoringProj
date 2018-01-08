using System;
using System.Collections;
using System.Collections.Generic;
using EnhancedScrollerDemos.SuperSimpleDemo;
using EnhancedUI.EnhancedScroller;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000077 RID: 119
public class HomeScript : MonoBehaviour
{
	// Token: 0x06000468 RID: 1128 RVA: 0x000131C4 File Offset: 0x000115C4
	private void Awake()
	{
		if (HomeScript.loadAllImages)
		{
			for (int i = 0; i < HomeScript.images_Count; i++)
			{
				int num = i + 1;
				HomeScript.images[i] = (Resources.Load("Grayscale/" + num + "PicGrey", typeof(Sprite)) as Sprite);
			}
			HomeScript.loadAllImages = false;
		}
		else
		{
			this.loadSingleTempImage();
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00013238 File Offset: 0x00011638
	private void Start()
	{
		PlayerPrefs.SetInt("ShowTutorial", 0);
		this.demo = UnityEngine.Object.FindObjectOfType<SimpleDemo>();
		if (!HomeScript.is_adobject)
		{
			UnityEngine.Object.DontDestroyOnLoad(UnityEngine.Object.Instantiate<GameObject>(this.ads));
			HomeScript.is_adobject = true;
		}
		this.demo.LoadLargeData();
		this.loadTempImage();
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001328C File Offset: 0x0001168C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !this.MoreappsPanel.activeInHierarchy && this.MainPanel.activeInHierarchy)
		{
			Application.Quit();
		}
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x000132C0 File Offset: 0x000116C0
	private void checkLocks()
	{
		for (int i = 20; i < HomeScript.images.Length; i++)
		{
			if (PlayerPrefs.GetInt("Pic" + i) == 1)
			{
				this.locks[i - 2].SetActive(false);
			}
		}
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00013311 File Offset: 0x00011711
	public void playclick(GameObject ob)
	{
		this.vScroller.JumpToDataIndex(0, 0f, 0f, true, EnhancedScroller.TweenType.immediate, 0f, null);
		ob.SetActive(false);
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00013338 File Offset: 0x00011738
	public void backclick(GameObject go)
	{
		go.SetActive(true);
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00013341 File Offset: 0x00011741
	public void backToHome(GameObject shop)
	{
		shop.SetActive(false);
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001334C File Offset: 0x0001174C
	public void ImageSelection()
	{
		this.tempLock = EventSystem.current.currentSelectedGameObject;
		int num = int.Parse(this.tempLock.name);
		HomeScript.selectedimage = num;
		Debug.Log("Selected Image is: " + HomeScript.selectedimage);
		if (HomeScript.selectedimage < 0)
		{
			this.openExternalAppsLinks(HomeScript.selectedimage + 4);
			this.custom_ads.switchAds(HomeScript.selectedimage);
			this.demo.LoadLargeData();
		}
		else
		{
			if (num >= 50)
			{
				this.index = num;
				if (PlayerPrefs.GetInt("Pic" + num) == 0)
				{
					this.watchVideoAd();
				}
				else
				{
					HomeScript.AdsCount++;
					if (HomeScript.AdsCount == 3)
					{
						HomeScript.AdsCount = 0;
						Ads_Script.current.ShowFullScreenAd();
					}
					if (HomeScript.currentIndex < HomeScript.selectedimage)
					{
						this.LoadAfterApply = true;
						this.loadSingleTempImage();
					}
					else
					{
						this.loadingPanel.SetActive(true);
						SceneManager.LoadScene(2);
					}
				}
			}
			else
			{
				HomeScript.AdsCount++;
				if (HomeScript.AdsCount == 3)
				{
					HomeScript.AdsCount = 0;
					Ads_Script.current.ShowFullScreenAd();
				}
				if (HomeScript.currentIndex < HomeScript.selectedimage)
				{
					this.LoadAfterApply = true;
					this.loadSingleTempImage();
				}
				else
				{
					this.loadingPanel.SetActive(true);
					SceneManager.LoadScene(2);
				}
			}
			Debug.Log("LocksCount: " + this.locks.Length);
			GameAnalytics.NewDesignEvent("Image: " + HomeScript.selectedimage);
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x000134F3 File Offset: 0x000118F3
	public void rewardedEvent(bool isdone)
	{
		if (isdone)
		{
			this.RewardVideoAd();
		}
		GoogleMobileAdsDemoScript.VideoDone -= this.rewardedEvent;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00013518 File Offset: 0x00011918
	public void watchVideoAd()
	{
		if (Ads_Script.current.ads.checkIsVideoLoaded() || Ads_Script.current.IsInterReady())
		{
			GoogleMobileAdsDemoScript.VideoDone += this.rewardedEvent;
			Ads_Script.current.ShowRewardedVideoAd();
			GameAnalytics.NewDesignEvent("VidoeAd");
		}
		else
		{
			this.MessagePanel.SetActive(true);
			base.StartCoroutine(this.hideMessage());
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001358C File Offset: 0x0001198C
	private IEnumerator hideMessage()
	{
		yield return new WaitForSeconds(1.5f);
		this.MessagePanel.SetActive(false);
		yield break;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x000135A7 File Offset: 0x000119A7
	public void RewardVideoAd()
	{
		PlayerPrefs.SetInt("Pic" + this.index, 1);
		this.tempLock.transform.GetChild(1).gameObject.SetActive(false);
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x000135E0 File Offset: 0x000119E0
	public void ShopSetting(GameObject pannel)
	{
		pannel.SetActive(true);
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x000135EC File Offset: 0x000119EC
	private void loadTempImage()
	{
		if (HomeScript.currentIndex < HomeScript.images.Length)
		{
			this.currentImage.sprite = HomeScript.images[HomeScript.currentIndex];
			this.fillTexture = this.currentImage.sprite.texture;
			string str = string.Concat(new object[]
			{
				Application.persistentDataPath,
				"/Temp/",
				HomeScript.currentIndex,
				"greytemp.png"
			});
			base.StartCoroutine(this.loadImage(new WWW("file://" + str), HomeScript.currentIndex + "greytemp.png", this.fillTexture));
			this.number.text = HomeScript.currentIndex + string.Empty;
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x000136C0 File Offset: 0x00011AC0
	private void loadSingleTempImage()
	{
		this.currentSingleImage.sprite = HomeScript.images[HomeScript.selectedimage];
		this.singlefillTexture = this.currentSingleImage.sprite.texture;
		string str = string.Concat(new object[]
		{
			Application.persistentDataPath,
			"/Temp/",
			HomeScript.selectedimage,
			"greytemp.png"
		});
		base.StartCoroutine(this.loadSingleImage(new WWW("file://" + str), HomeScript.selectedimage + "greytemp.png", this.singlefillTexture));
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00013764 File Offset: 0x00011B64
	private IEnumerator loadImage(WWW www, string spriteName, Texture2D tex)
	{
		yield return www;
		if (www.text != null && !www.text.Equals(string.Empty))
		{
			Debug.Log("file: " + spriteName);
			Sprite sprite = Sprite.Create(www.texture, new Rect(0f, 0f, (float)www.texture.width, (float)www.texture.height), new Vector2(1f, 1f));
			this.fillTexture.SetPixels32(sprite.texture.GetPixels32());
			this.fillTexture.Apply();
			HomeScript.currentIndex++;
			if (HomeScript.currentIndex < HomeScript.images.Length)
			{
				this.loadTempImage();
			}
			else
			{
				this.locks = new GameObject[HomeScript.images.Length];
			}
			Debug.LogError("|||||||||Saved image found");
		}
		else
		{
			HomeScript.currentIndex++;
			if (HomeScript.currentIndex < HomeScript.images.Length)
			{
				this.loadTempImage();
			}
			else
			{
				this.locks = new GameObject[HomeScript.images.Length];
			}
		}
		yield break;
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00013790 File Offset: 0x00011B90
	private IEnumerator loadSingleImage(WWW www, string spriteName, Texture2D tex)
	{
		yield return www;
		if (www.text != null && !www.text.Equals(string.Empty))
		{
			Debug.Log("Single file: " + spriteName);
			Sprite sprite = Sprite.Create(www.texture, new Rect(0f, 0f, (float)www.texture.width, (float)www.texture.height), new Vector2(1f, 1f));
			this.singlefillTexture.SetPixels32(sprite.texture.GetPixels32());
			this.singlefillTexture.Apply();
			Debug.LogError("|||||||||Saved image found");
			if (this.LoadAfterApply)
			{
				this.loadingPanel.SetActive(true);
				SceneManager.LoadScene(2);
			}
		}
		else if (this.LoadAfterApply)
		{
			this.loadingPanel.SetActive(true);
			SceneManager.LoadScene(2);
		}
		yield break;
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x000137B9 File Offset: 0x00011BB9
	public void hideMoreApps()
	{
		this.MoreappsPanel.SetActive(false);
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x000137C7 File Offset: 0x00011BC7
	public void showMoreApps()
	{
		this.MoreappsPanel.SetActive(true);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x000137D5 File Offset: 0x00011BD5
	public void openExternalAppsLinks(int appIndex)
	{
		Application.OpenURL("market://details?id=" + this.applinks[appIndex]);
		GameAnalytics.NewDesignEvent("ExternalApp:" + this.applinks[appIndex]);
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001380D File Offset: 0x00011C0D
	public void JumpToSpecificCell(int jumpDataIndex)
	{
		this.vScroller.JumpToDataIndex(jumpDataIndex, -0.5f, 0f, true, EnhancedScroller.TweenType.immediate, 0f, null);
		this.MainPanel.SetActive(false);
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00013839 File Offset: 0x00011C39
	public void Show_Tutorial()
	{
		this.TutorialPanel.SetActive(true);
	}

	// Token: 0x04000271 RID: 625
	public Text number;

	// Token: 0x04000272 RID: 626
	public List<string> applinks = new List<string>();

	// Token: 0x04000273 RID: 627
	public List<Sprite> appIcons = new List<Sprite>();

	// Token: 0x04000274 RID: 628
	public GameObject MoreappsPanel;

	// Token: 0x04000275 RID: 629
	public GameObject MainPanel;

	// Token: 0x04000276 RID: 630
	public GameObject ads;

	// Token: 0x04000277 RID: 631
	private static bool is_adobject;

	// Token: 0x04000278 RID: 632
	public SpriteRenderer currentImage;

	// Token: 0x04000279 RID: 633
	public SpriteRenderer currentSingleImage;

	// Token: 0x0400027A RID: 634
	private Texture2D fillTexture;

	// Token: 0x0400027B RID: 635
	private Texture2D singlefillTexture;

	// Token: 0x0400027C RID: 636
	private static int currentIndex = 0;

	// Token: 0x0400027D RID: 637
	public static int selectedimage = 0;

	// Token: 0x0400027E RID: 638
	private int index;

	// Token: 0x0400027F RID: 639
	public GameObject MessagePanel;

	// Token: 0x04000280 RID: 640
	public GameObject[] locks;

	// Token: 0x04000281 RID: 641
	private GameObject tempLock;

	// Token: 0x04000282 RID: 642
	private SimpleDemo demo;

	// Token: 0x04000283 RID: 643
	public static int images_Count = 945;

	// Token: 0x04000284 RID: 644
	public static Sprite[] images = new Sprite[HomeScript.images_Count];

	// Token: 0x04000285 RID: 645
	public static bool loadAllImages = true;

	// Token: 0x04000286 RID: 646
	public static int AdsCount = 0;

	// Token: 0x04000287 RID: 647
	public EnhancedScroller vScroller;

	// Token: 0x04000288 RID: 648
	public CustomAds custom_ads;

	// Token: 0x04000289 RID: 649
	private bool LoadAfterApply;

	// Token: 0x0400028A RID: 650
	public GameObject loadingPanel;

	// Token: 0x0400028B RID: 651
	public GameObject TutorialPanel;
}
