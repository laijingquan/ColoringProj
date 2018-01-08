using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class ScreenshotManager : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000106 RID: 262 RVA: 0x00009464 File Offset: 0x00007864
	// (remove) Token: 0x06000107 RID: 263 RVA: 0x00009498 File Offset: 0x00007898
	public static event Action<Texture2D> OnScreenshotTaken;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000108 RID: 264 RVA: 0x000094CC File Offset: 0x000078CC
	// (remove) Token: 0x06000109 RID: 265 RVA: 0x00009500 File Offset: 0x00007900
	public static event Action<string> OnScreenshotSaved;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x0600010A RID: 266 RVA: 0x00009534 File Offset: 0x00007934
	// (remove) Token: 0x0600010B RID: 267 RVA: 0x00009568 File Offset: 0x00007968
	public static event Action<string> OnImageSaved;

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600010C RID: 268 RVA: 0x0000959C File Offset: 0x0000799C
	public static ScreenshotManager Instance
	{
		get
		{
			if (ScreenshotManager.instance == null)
			{
				ScreenshotManager.go = new GameObject();
				ScreenshotManager.go.name = "ScreenshotManager";
				ScreenshotManager.instance = ScreenshotManager.go.AddComponent<ScreenshotManager>();
				if (Application.platform == RuntimePlatform.Android)
				{
					ScreenshotManager.obj = new AndroidJavaClass("com.secondfury.galleryscreenshot.MainActivity");
				}
			}
			return ScreenshotManager.instance;
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00009601 File Offset: 0x00007A01
	private void Awake()
	{
		if (ScreenshotManager.instance != null && ScreenshotManager.instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00009630 File Offset: 0x00007A30
	public static void SaveScreenshot(string fileName, string albumName = "MyScreenshots", string fileType = "jpg", Rect screenArea = default(Rect))
	{
		if (screenArea == default(Rect))
		{
			screenArea = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		}
		ScreenshotManager.Instance.StartCoroutine(ScreenshotManager.Instance.GrabScreenshot(fileName, albumName, fileType, screenArea));
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00009688 File Offset: 0x00007A88
	private IEnumerator GrabScreenshot(string fileName, string albumName, string fileType, Rect screenArea)
	{
		yield return new WaitForEndOfFrame();
		Texture2D texture = new Texture2D((int)screenArea.width, (int)screenArea.height, TextureFormat.RGB24, false);
		texture.ReadPixels(screenArea, 0, 0);
		texture.Apply();
		byte[] bytes;
		string fileExt;
		if (fileType == "png")
		{
			bytes = texture.EncodeToPNG();
			fileExt = ".png";
		}
		else
		{
			bytes = texture.EncodeToJPG();
			fileExt = ".jpg";
		}
		if (ScreenshotManager.OnScreenshotTaken != null)
		{
			ScreenshotManager.OnScreenshotTaken(texture);
		}
		else
		{
			UnityEngine.Object.Destroy(texture);
		}
		string date = DateTime.Now.ToString("hh-mm-ss_dd-MM-yy");
		string screenshotFilename = fileName + "_ADIL" + fileExt;
		string path = Application.persistentDataPath + "/" + screenshotFilename;
		if (Application.platform == RuntimePlatform.Android)
		{
			string path2 = Path.Combine(albumName, screenshotFilename);
			path = Path.Combine(Application.persistentDataPath, path2);
			string directoryName = Path.GetDirectoryName(path);
			Directory.CreateDirectory(directoryName);
		}
		ScreenshotManager.Instance.StartCoroutine(ScreenshotManager.Instance.Save(bytes, fileName, path, ScreenshotManager.ImageType.SCREENSHOT));
		yield break;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x000096BC File Offset: 0x00007ABC
	public static void SaveAssets(Texture2D texture, string fileName, string filePath, string fileType = "png")
	{
		ScreenshotManager.Instance.Awake();
		filePath += "/Assets";
		string str;
		if (fileType.ToLower().Contains("png"))
		{
			str = ".png";
		}
		else
		{
			str = ".jpg";
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
		try
		{
			directoryInfo.GetFiles();
		}
		catch
		{
			MonoBehaviour.print("Directry SucessFully Created");
			directoryInfo.Create();
		}
		string path = filePath + "/" + fileName + str;
		ScreenshotManager.SaveTextureToFile(path, texture);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00009754 File Offset: 0x00007B54
	public static void SaveImage(Texture2D texture, string fileName, string category, string fileType = "png")
	{
		string str = fileName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
		ScreenshotManager.Instance.Awake();
		string text;
		if (fileType == "png")
		{
			byte[] array = texture.EncodeToPNG();
			text = ".png";
		}
		else
		{
			byte[] array = texture.EncodeToJPG();
			text = ".jpg";
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/MyWork");
		try
		{
			directoryInfo.GetFiles();
		}
		catch
		{
			MonoBehaviour.print("Directry Catch SucessFully Created");
			directoryInfo.Create();
		}
		string text2 = Application.persistentDataPath + "/MyWork/" + str + text;
		Debug.LogWarning("Image Saved To Path =" + text2);
		ScreenshotManager.SaveTextureToFile(text2, texture);
		ScreenshotManager.DeleteTempImage(fileName + "temp" + text);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00009840 File Offset: 0x00007C40
	public static void SaveTempImageInspiration(Texture2D texture, string fileName, string fileType = "png")
	{
		fileName = fileName.Replace(" ", string.Empty);
		ScreenshotManager.Instance.Awake();
		string str;
		if (fileType == "png")
		{
			byte[] array = texture.EncodeToPNG();
			str = ".png";
		}
		else
		{
			byte[] array = texture.EncodeToJPG();
			str = ".jpg";
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/InspirationTemp");
		try
		{
			directoryInfo.GetFiles();
		}
		catch
		{
			MonoBehaviour.print("Directry SucessFully Created");
			directoryInfo.Create();
		}
		string text = Application.persistentDataPath + "/InspirationTemp/" + fileName + str;
		Debug.LogWarning("Inspiration Saved To Path =" + text);
		ScreenshotManager.SaveTextureToFile(text, texture);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00009908 File Offset: 0x00007D08
	public static void SaveImageGallery(Texture2D texture, string fileName, string fileType = "png")
	{
		string text = fileName + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
		text.Replace(" ", string.Empty);
		ScreenshotManager.Instance.Awake();
		byte[] bytes;
		string text2;
		if (fileType == "png")
		{
			bytes = texture.EncodeToPNG();
			text2 = ".png";
		}
		else
		{
			bytes = texture.EncodeToJPG();
			text2 = ".jpg";
		}
		string text3 = Application.productName.Replace(" ", string.Empty).ToLower();
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/" + text3);
		try
		{
			directoryInfo.GetFiles();
		}
		catch
		{
			MonoBehaviour.print("Directry Catch SucessFully Created");
			directoryInfo.Create();
		}
		string text4 = string.Concat(new string[]
		{
			Application.persistentDataPath,
			"/",
			text3,
			"/",
			text,
			text2
		});
		Debug.LogWarning("Image Saved To Path =" + text4);
		ScreenshotManager.SaveTextureToFile(text4, texture);
		ScreenshotManager.Instance.StartCoroutine(ScreenshotManager.Instance.Save(bytes, text, text4, ScreenshotManager.ImageType.IMAGE));
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00009A4C File Offset: 0x00007E4C
	public static void SaveTempImage(Texture2D texture, string fileName, string fileType = "jpg")
	{
		fileName = fileName.Replace(" ", string.Empty);
		ScreenshotManager.Instance.Awake();
		string text;
		if (fileType == "png")
		{
			byte[] array = texture.EncodeToPNG();
			text = ".png";
		}
		else
		{
			byte[] array = texture.EncodeToJPG();
			text = ".jpg";
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/Temp");
		try
		{
			directoryInfo.GetFiles();
		}
		catch
		{
			MonoBehaviour.print("Directry SucessFully Created");
			directoryInfo.Create();
		}
		string path = string.Concat(new string[]
		{
			Application.persistentDataPath,
			"/Temp/",
			fileName,
			"temp",
			text
		});
		ScreenshotManager.SaveTextureToFile(path, texture);
		PaintingScript.textureSaving = false;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00009B24 File Offset: 0x00007F24
	private static void SaveTextureToFile(string path, Texture2D texture)
	{
		byte[] buffer = texture.EncodeToPNG();
		FileStream fileStream = File.Open(path, FileMode.Create);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		binaryWriter.Write(buffer);
		fileStream.Close();
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00009B54 File Offset: 0x00007F54
	private IEnumerator Save(byte[] bytes, string fileName, string path, ScreenshotManager.ImageType imageType)
	{
		int count = 0;
		ScreenshotManager.SaveStatus saved = ScreenshotManager.SaveStatus.NOTSAVED;
		if (Application.platform == RuntimePlatform.Android)
		{
			File.WriteAllBytes(path, bytes);
			while (saved == ScreenshotManager.SaveStatus.NOTSAVED)
			{
				count++;
				if (count > 30)
				{
					saved = ScreenshotManager.SaveStatus.TIMEOUT;
				}
				else
				{
					saved = (ScreenshotManager.SaveStatus)ScreenshotManager.obj.CallStatic<int>("addImageToGallery", new object[]
					{
						path
					});
				}
				yield return ScreenshotManager.Instance.StartCoroutine(ScreenshotManager.Instance.Wait(0.5f));
			}
		}
		if (saved != ScreenshotManager.SaveStatus.DENIED)
		{
			if (saved == ScreenshotManager.SaveStatus.TIMEOUT)
			{
				path = "TIMEOUT";
			}
		}
		else
		{
			path = "DENIED";
		}
		if (imageType != ScreenshotManager.ImageType.IMAGE)
		{
			if (imageType == ScreenshotManager.ImageType.SCREENSHOT)
			{
				if (ScreenshotManager.OnScreenshotSaved != null)
				{
					ScreenshotManager.OnScreenshotSaved(path);
				}
			}
		}
		else if (ScreenshotManager.OnImageSaved != null)
		{
			ScreenshotManager.OnImageSaved(path);
		}
		yield break;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00009B80 File Offset: 0x00007F80
	private static void DeleteTempImage(string Name = "")
	{
		string text = Application.persistentDataPath + "/Temp/";
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/Temp/");
		try
		{
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				if (Name.Equals(files[i].Name))
				{
					Debug.Log("Temp image deleted sucessfully at path " + Name);
					files[i].Delete();
					break;
				}
			}
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00009C20 File Offset: 0x00008020
	private IEnumerator Wait(float delay)
	{
		float pauseTarget = Time.realtimeSinceStartup + delay;
		while (Time.realtimeSinceStartup < pauseTarget)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x040000E4 RID: 228
	private static ScreenshotManager instance;

	// Token: 0x040000E5 RID: 229
	private static GameObject go;

	// Token: 0x040000E6 RID: 230
	private static AndroidJavaClass obj;

	// Token: 0x02000027 RID: 39
	private enum ImageType
	{
		// Token: 0x040000E8 RID: 232
		IMAGE,
		// Token: 0x040000E9 RID: 233
		SCREENSHOT
	}

	// Token: 0x02000028 RID: 40
	private enum SaveStatus
	{
		// Token: 0x040000EB RID: 235
		NOTSAVED,
		// Token: 0x040000EC RID: 236
		SAVED,
		// Token: 0x040000ED RID: 237
		DENIED,
		// Token: 0x040000EE RID: 238
		TIMEOUT
	}
}
