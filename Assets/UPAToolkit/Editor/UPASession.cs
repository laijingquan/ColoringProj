//-----------------------------------------------------------------
// This class hosts utility methods for handling session information.
//-----------------------------------------------------------------

using UnityEngine;
using UnityEditor;
using System.IO;

public class UPASession {

    /// <summary>
    /// 创建图片数据有俩种方式，导入图片或者数据生成UPAImage,或者新建一个UIAImage
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
	public static UPAImage CreateImage (int w, int h) {
		string path = EditorUtility.SaveFilePanel ("Create UPAImage",
		                                           "Assets/", "Pixel Image.asset", "asset");
		if (path == "") {
			return null;
		}
		
		path = FileUtil.GetProjectRelativePath(path);//截取当前项目路径
		
		UPAImage img = ScriptableObject.CreateInstance<UPAImage>();
		AssetDatabase.CreateAsset (img, path);
		
		AssetDatabase.SaveAssets();
		
		img.Init(w, h);//新建一个空的UPAImage
		EditorUtility.SetDirty(img);
		UPAEditorWindow.CurrentImg = img;
		
		EditorPrefs.SetString ("currentImgPath", AssetDatabase.GetAssetPath (img));

        if (UPAEditorWindow.window != null) 
            UPAEditorWindow.window.Repaint();
        else
            UPAEditorWindow.Init();

		img.gridSpacing = 10 - Mathf.Abs (img.width - img.height)/100f;
		return img;
	}

	public static UPAImage OpenImage ()
    {
        //打开操作系统的文件框来选择导入的图片或者.asset数据
		string path = EditorUtility.OpenFilePanel(
			"Find an Image (.asset | .png | .jpg)",
			"Assets/",
			"Image Files;*.asset;*.jpg;*.png");
		
		if (path.Length != 0)
        {
			// Check if the loaded file is an Asset or Image
            //如果是数据，直接反序列化即可
			if (path.EndsWith(".asset"))
            {
				path = FileUtil.GetProjectRelativePath(path);
				UPAImage img = AssetDatabase.LoadAssetAtPath(path, typeof(UPAImage)) as UPAImage;//*.asset数据本身就是序列化的UPAImage
				EditorPrefs.SetString ("currentImgPath", path);
				return img;
			}
            //如果是图片,那么需要根据图片来构造UPAImage数据结构
			else
			{
				// Load Texture from file
				Texture2D tex = LoadImageFromFile(path);
				// Create a new Image with textures dimensions
				UPAImage img = CreateImage(tex.width, tex.height);
				// Set pixel colors
                if(img.layers!=null&&img.layers.Count>0)
                {
                    img.layers[0].tex = tex;
                    img.layers[0].tex.filterMode = FilterMode.Point;
                    img.layers[0].tex.Apply();
                    for (int x = 0; x < img.width; x++)
                    {
                        for (int y = 0; y < img.height; y++)
                        {
                            //map逻辑上是一个一维数组,可用二维的方式来访问。
                            //一个图像按照左到右,上到下和map数组对应。
                            //这样就可以根据屏幕的触摸坐标来直接获取map数组对应的像素
                            img.layers[0].map[x + y * tex.width] = tex.GetPixel(x, tex.height - 1 - y);
                        }
                    }
                }
			}
		}
		
		return null;
	}

	public static Texture2D LoadImageFromFile (string path) {
		Texture2D tex = null;
		byte[] fileData;
		if (File.Exists(path))     {
			fileData = File.ReadAllBytes(path);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}
	
	public static UPAImage OpenImageByAsset (UPAImage img) {

		if (img == null) {
			Debug.LogWarning ("Image is null. Returning null.");
			EditorPrefs.SetString ("currentImgPath", "");
			return null;
		}

		string path = AssetDatabase.GetAssetPath (img);
		EditorPrefs.SetString ("currentImgPath", path);
		
		return img;
	}

	public static UPAImage OpenImageAtPath (string path) {
		if (path.Length != 0) {
			UPAImage img = AssetDatabase.LoadAssetAtPath(path, typeof(UPAImage)) as UPAImage;

			if (img == null) {
				EditorPrefs.SetString ("currentImgPath", "");
				return null;
			}

			EditorPrefs.SetString ("currentImgPath", path);
			return img;
		}
		
		return null;
	}

	public static bool ExportImage (UPAImage img, TextureType type, TextureExtension extension) {
		string path = EditorUtility.SaveFilePanel(
			"Export image as " + extension.ToString(),
			"Assets/",
			img.name + "." + extension.ToString().ToLower(),
			extension.ToString().ToLower());
		
		if (path.Length == 0)
			return false;
		
		byte[] bytes;
		if (extension == TextureExtension.PNG) {
			// Encode texture into PNG
			bytes = img.GetFinalImage(true).EncodeToPNG();
		} else {
			// Encode texture into JPG
			
			#if UNITY_4_2
			bytes = img.GetFinalImage(true).EncodeToPNG();
			#elif UNITY_4_3
			bytes = img.GetFinalImage(true).EncodeToPNG();
			#elif UNITY_4_5
			bytes = img.GetFinalImage(true).EncodeToJPG();
			#else
			bytes = img.GetFinalImage(true).EncodeToJPG();
			#endif
		}
		
		path = FileUtil.GetProjectRelativePath(path);
		
		//Write to a file in the project folder
		File.WriteAllBytes(path, bytes);
		AssetDatabase.Refresh();
		
		TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter; 
		
		if (type == TextureType.texture)
			texImp.textureType = TextureImporterType.Image;
		else if (type == TextureType.sprite) {
			texImp.textureType = TextureImporterType.Sprite;

			#if UNITY_4_2
			texImp.spritePixelsToUnits = 10;
			#elif UNITY_4_3
			texImp.spritePixelsToUnits = 10;
			#elif UNITY_4_5
			texImp.spritePixelsToUnits = 10;
			#else
			texImp.spritePixelsPerUnit = 10;
			#endif
		}
		
		texImp.filterMode = FilterMode.Point;
		texImp.textureFormat = TextureImporterFormat.AutomaticTruecolor;
		
		AssetDatabase.ImportAsset(path); 
		
		return true;
	}
}
