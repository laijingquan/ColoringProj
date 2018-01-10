using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class NewTexture2D : MonoBehaviour {

    public bool original;
    public bool painting;
	// Use this for initialization
	void Start ()
    {
        if(original)
        {
            var tex = LoadImageFromFile("F:\\LaiProj\\ColoringProj\\Assets\\Resources\\Grayscale\\1Pic.png");
            tex.filterMode = FilterMode.Point;
            GetComponent<RawImage>().texture = tex;
        }

        if(painting)
        {
            GetComponent<RawImage>().texture = CreateFillTex(24,22);
        }
        GetComponent<RawImage>().SetNativeSize();
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public static Texture2D CreateFillTex(int w,int h)
    {
        Texture2D tx = new Texture2D(w,h);
        for(int i =0; i< w;i++)
        {
            for (int j=0;j<h;j++)
            {
                tx.SetPixel(i, j, new Color(0,0,0,0));
            }
        }
        tx.filterMode = FilterMode.Point;
        tx.Apply();
        return tx;
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
