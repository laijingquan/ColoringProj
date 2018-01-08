using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class CommonScript : MonoBehaviour
{
	// Token: 0x060003E5 RID: 997 RVA: 0x00011C02 File Offset: 0x00010002
	public void RateUs()
	{
		Application.OpenURL(GlobalValues.GoogleRateUs);
		PlayerPrefs.SetInt("AppRated", 1);
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00011C19 File Offset: 0x00010019
	public void LikeUs()
	{
		if (CheckFacebookAppInstall.checkPackageAppIsPresent("com.facebook.katana"))
		{
			Application.OpenURL(GlobalValues.FacebookApp);
		}
		else if (!CheckFacebookAppInstall.checkPackageAppIsPresent("com.facebook.katana"))
		{
			Application.OpenURL(GlobalValues.FacebookWeb);
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00011C54 File Offset: 0x00010054
	public void ShareImage(Texture2D texture)
	{
		string googleRateUs = GlobalValues.GoogleRateUs;
		string str = "The world is my canvas and i create my reality.";
		try
		{
			Debug.Log("check1");
			byte[] bytes = texture.EncodeToPNG();
			string text = Path.Combine(Application.persistentDataPath, DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
			File.WriteAllBytes(text, texture.EncodeToPNG());
			File.WriteAllBytes(text, bytes);
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]);
			androidJavaObject.Call<AndroidJavaObject>("setAction", new object[]
			{
				androidJavaClass.GetStatic<string>("ACTION_SEND")
			});
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("parse", new object[]
			{
				"file://" + text
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_STREAM"),
				androidJavaObject2
			});
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
			{
				"text/plain"
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
				string.Empty + str + "\n" + googleRateUs
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
				"SUBJECT"
			});
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
			{
				"image/jpeg"
			});
			AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass3.GetStatic<AndroidJavaObject>("currentActivity");
			@static.Call("startActivity", new object[]
			{
				androidJavaObject
			});
		}
		catch
		{
			Debug.Log("check2");
		}
		finally
		{
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00011E60 File Offset: 0x00010260
	public void Share()
	{
		iOSBridge.TakeScreenShot();
		iOSBridge.AddSharing("The world is my canvas and i create my reality.", "https://itunes.apple.com/us/app/numbers-sandbox-coloring-book/id1315587796?ls=1&mt=8");
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00011E76 File Offset: 0x00010276
	public void ScreenshotSharing()
	{
		base.StartCoroutine(this.doSharing(true));
		base.StartCoroutine(this.showRatePanel());
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00011E94 File Offset: 0x00010294
	private IEnumerator showRatePanel()
	{
		yield return new WaitForSeconds(1.5f);
		if (this.RatePanel && PlayerPrefs.GetInt("AppRated") == 0)
		{
			this.RatePanel.SetActive(true);
		}
		yield break;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00011EAF File Offset: 0x000102AF
	public void androidScreenshot()
	{
		base.StartCoroutine(this.doSharing(true));
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00011EBF File Offset: 0x000102BF
	public void TakeScreenShotandSave()
	{
		Application.CaptureScreenshot("SS");
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00011ECC File Offset: 0x000102CC
	private IEnumerator doSharing(bool takeScreenShot)
	{
		if (takeScreenShot)
		{
			this.TakeScreenShotandSave();
		}
		yield return new WaitForSeconds(0.1f);
		string path = Path.Combine(Application.persistentDataPath, "SS");
		yield return new WaitForSeconds(0.2f);
		WWW loadedImage = new WWW("file://" + path);
		yield return !loadedImage.isDone;
		if (loadedImage.bytes.Length > 0)
		{
			Texture2D texture = loadedImage.texture;
			this.ShareImage(texture);
		}
		yield break;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00011EEE File Offset: 0x000102EE
	public void AdsRemovedSuccess()
	{
		GlobalValues.removeAds = 1;
	}

	// Token: 0x04000247 RID: 583
	public GameObject RatePanel;
}
