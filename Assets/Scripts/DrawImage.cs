using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class DrawImage : EditorWindow {

    static Texture2D tex;
    static Rect rect;
    [MenuItem("Window/test")]
    static void Init()
    {
        var window = EditorWindow.GetWindow(typeof(DrawImage));
        //打开操作系统的文件框来选择导入的图片或者.asset数据
        string path = EditorUtility.OpenFilePanel(
            "Find an Image (.asset | .png | .jpg)",
            "Assets/",
            "Image Files;*.asset;*.jpg;*.png");

        tex = LoadImageFromFile(path);
        tex.filterMode = FilterMode.Point;
        rect = new Rect(0, 0, tex.width, tex.height);
    }

    public void OnGUI()
    {
        Texture2D bg = new Texture2D(1, 1);
        bg.SetPixel(0, 0, Color.clear);
        bg.Apply();
        EditorGUI.DrawTextureTransparent(rect, bg);
        GUI.DrawTexture(rect, tex);
    }

    public static Texture2D LoadImageFromFile(string path)
    {
        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
