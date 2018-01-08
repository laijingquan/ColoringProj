using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class CheckFacebookAppInstall
{
	// Token: 0x060003E3 RID: 995 RVA: 0x00011B44 File Offset: 0x0000FF44
	public static bool checkPackageAppIsPresent(string package)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
		AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getInstalledPackages", new object[]
		{
			0
		});
		int num = androidJavaObject2.Call<int>("size", new object[0]);
		for (int i = 0; i < num; i++)
		{
			AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("get", new object[]
			{
				i
			});
			string text = androidJavaObject3.Get<string>("packageName");
			if (text.CompareTo(package) == 0)
			{
				return true;
			}
		}
		return false;
	}
}
