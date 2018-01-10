using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputTexture : MonoBehaviour {

    public Canvas canvas;
    public Image image;
    public bool test;
    public int VoxNumberTex;
	// Use this for initialization
	void Start ()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        image = GetComponent<Image>();
        //for(int i =0; i < 100;i++)
        //{
        //    Paint(new Vector2(i, 0));
        //}
        //Paint(new Vector2(0, 0));
        //Paint(new Vector2(100, 11));
        NewVoxelTexture();
    }
	
    void Paint(Vector2 pos)
    {
        if(image==null)
            image = GetComponent<Image>();
        if (image == null) return;
        var tex = image.mainTexture as Texture2D;
        image.SetNativeSize();
        tex.SetPixel((int)pos.x, (int)pos.y, Color.red);
        tex.Apply();
    }

    void NewVoxelTexture()
    {
        if (image == null)
            image = GetComponent<Image>();
        if (image == null) return;
        string name = "Grayscale/" + VoxNumberTex + "PicGrey";
        var tex = Resources.Load(name) as Texture2D;
        var newTex = new Texture2D(tex.width, tex.height);
        newTex.filterMode = FilterMode.Point;
        newTex.SetPixels(tex.GetPixels());
        image.SetTexture2D(newTex);
        newTex.Apply();
        image.SetNativeSize();
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
               // Debug.Log(EventSystem.current.name);
            }
            else
            {
                Debug.Log("没有点击到");
            }
            Debug.LogFormat("屏幕坐标的位置{0},{1}",Input.mousePosition.x,Input.mousePosition.y);
            var pos = Input.mousePosition;
            if (image == null)
            {
                Debug.Log("没有对应的image组件");
                return;
            }
            //if (image != null)
            //{
            //    var screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, image.transform.position);
            //    Debug.LogFormat("点击图片转换为屏幕坐标{0},{1}", screenPos.x, screenPos.y);
            //}
            Vector3 worldPos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out worldPos))
            {
                Debug.LogFormat("图片的世界坐标{0},{1}", worldPos.x, worldPos.y);
                var pixel = new Vector2(worldPos.x -image.transform.position.x+ image.rectTransform.rect.width/2,worldPos.y-image.transform.position.y+ image.rectTransform.rect.height / 2);
                Paint(pixel);
                Debug.LogFormat("像素位置：{0}，{1}", pixel.x, pixel.y);
                //image.transform.position = worldPos;
            }

            //Vector2 localPos;
            //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, image.transform.position, canvas.worldCamera, out localPos))
            //{
            //    image.rectTransform.anchoredPosition = localPos;
            //    var r = image.rectTransform.rect;
            //}
        }
    }
}
