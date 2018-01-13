using UnityEngine;
using System.Collections;

public class SpriteRender_Point : MonoBehaviour {

    public int TextureID;
    public Texture2D tex;
    private void Awake()
    {
        tex = Resources.Load("Grayscale/" + TextureID + "PicGrey") as Texture2D;
        tex.filterMode = FilterMode.Point;
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0f, 0f),1);
    }
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
