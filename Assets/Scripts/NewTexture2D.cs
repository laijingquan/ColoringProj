using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class NewTexture2D : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        var tex = LoadImageFromFile("F:\\LaiProj\\ColoringProj\\Assets\\Resources\\Grayscale\\1Pic.png");
        tex.filterMode = FilterMode.Point;
        GetComponent<RawImage>().texture = tex;
	}
	
	// Update is called once per frame
	void Update () {
	
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
