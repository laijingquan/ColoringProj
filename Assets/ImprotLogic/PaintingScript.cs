using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200000D RID: 13
public class PaintingScript : MonoBehaviour
{
	// Token: 0x0600003B RID: 59 RVA: 0x00003932 File Offset: 0x00001D32
	private void Awake()
	{
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003934 File Offset: 0x00001D34
	private void OnEnable()
	{
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003938 File Offset: 0x00001D38
	private void Start()
	{
		if (UnityEngine.Object.FindObjectOfType<testPan>())
		{
			this.panning_script = UnityEngine.Object.FindObjectOfType<testPan>();
		}
		this.fillRenderer = this.ImageBoard.transform.GetChild(1).GetComponent<SpriteRenderer>();
		PaintingScript.textureSaving = false;
		this.FillSprite = this.fillRenderer.sprite;
		this.PatterSprite = this.patternRenderer.sprite;
		this.fillTexture = this.FillSprite.texture;
		this.patternTexture = this.PatterSprite.texture;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000039C8 File Offset: 0x00001DC8
	private void loadTempImage()
	{
		string str = string.Concat(new object[]
		{
			Application.persistentDataPath,
			"/Temp/",
			HomeScript.selectedimage,
			"greytemp.png"
		});
		base.StartCoroutine(this.loadImage(new WWW("file://" + str), HomeScript.selectedimage + "greytemp.png", this.fillTexture));
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003A40 File Offset: 0x00001E40
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (Input.touchCount > 0)
			{
				this.TDownPos = Input.GetTouch(0).position;
			}
			Vector2 vector = Input.mousePosition;
			Color color;
			bool spritePixelColorUnderMousePointer = this.GetSpritePixelColorUnderMousePointer(this.fillRenderer, out color, out vector);
			this.preDrag = vector;
			this.enablePainting = true;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.ondownpaint = true;
			if (Input.touchCount > 0)
			{
				this.TUpPos = Input.GetTouch(0).position;
				this.TouchDeltaPos = Vector2.Distance(this.TDownPos, this.TUpPos);
				Debug.Log("Delta Pos: " + this.TouchDeltaPos);
				if (!this.BrushEnable)
				{
					this.DoPaint();
				}
				this.enablePainting = false;
			}
		}
		if (this.BrushEnable)
		{
			this.DoBrushPaint();
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003B32 File Offset: 0x00001F32
	private void LateUpdate()
	{
		if (Input.GetMouseButtonUp(0))
		{
			this.ondownpaint = true;
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003B46 File Offset: 0x00001F46
	public void saveMyProgress()
	{
		if (!PaintingScript.textureSaving)
		{
			base.StartCoroutine(this.saveTempImage());
			PaintingScript.textureSaving = true;
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003B68 File Offset: 0x00001F68
	private void DoPaint()
	{
		if (!this.enablePainting)
		{
			return;
		}
		if (this.ondownpaint && this.TouchDeltaPos < 20f)
		{
			this.TouchDeltaPos = 2f;
			Vector2 p = Input.mousePosition;
			Color color;
			if (this.GetSpritePixelColorUnderMousePointer(this.fillRenderer, out color, out p))
			{
				if (EventSystem.current.currentSelectedGameObject == null)
				{
					this.Brush(p, this.preDrag);
					this.preDrag = p;
				}
				else if (!EventSystem.current.currentSelectedGameObject.tag.Equals("ColorButton"))
				{
					this.Brush(p, this.preDrag);
					this.preDrag = p;
				}
			}
			else
			{
				MonoBehaviour.print("Error getting color.");
			}
			this.ondownpaint = false;
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003C40 File Offset: 0x00002040
	private void DoBrushPaint()
	{
		if (Input.touchCount == 1)
		{
			Vector2 position = Input.GetTouch(0).position;
			Color color;
			if (this.GetSpritePixelColorUnderMousePointer(this.fillRenderer, out color, out position))
			{
				if (EventSystem.current.currentSelectedGameObject == null)
				{
					this.Brush(position, this.preDrag);
					this.preDrag = position;
				}
				else if (!EventSystem.current.currentSelectedGameObject.tag.Equals("ColorButton"))
				{
					this.Brush(position, this.preDrag);
					this.preDrag = position;
				}
			}
			else
			{
				MonoBehaviour.print("Error getting color.");
			}
			this.ondownpaint = false;
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003CF4 File Offset: 0x000020F4
	public void ToggleBrsuh(Image icon)
	{
		this.BrushEnable = !this.BrushEnable;
		this.panning_script.enabled = !this.BrushEnable;
		if (this.BrushEnable)
		{
			icon.sprite = this.brushes[1];
		}
		else
		{
			icon.sprite = this.brushes[0];
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003D50 File Offset: 0x00002150
	public void ToggleMenu(Image icon)
	{
		if (this.MenuPanel.transform.localScale.y == 0f)
		{
			base.StartCoroutine(this.AnimatePanelOpen());
			icon.sprite = this.arrows[1];
		}
		else
		{
			base.StartCoroutine(this.AnimatePanelClose());
			icon.sprite = this.arrows[0];
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003DBC File Offset: 0x000021BC
	private IEnumerator AnimatePanelOpen()
	{
		yield return new WaitForSeconds(0.01f);
		this.scale = this.MenuPanel.transform.localScale;
		this.scale.y = this.scale.y + 0.1f;
		this.MenuPanel.transform.localScale = this.scale;
		if (this.MenuPanel.transform.localScale.y < 1f)
		{
			base.StartCoroutine(this.AnimatePanelOpen());
		}
		else
		{
			this.scale.y = 1f;
			this.MenuPanel.transform.localScale = this.scale;
		}
		yield break;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003DD8 File Offset: 0x000021D8
	private IEnumerator AnimatePanelClose()
	{
		yield return new WaitForSeconds(0.01f);
		this.scale = this.MenuPanel.transform.localScale;
		this.scale.y = this.scale.y - 0.1f;
		this.MenuPanel.transform.localScale = this.scale;
		if (this.MenuPanel.transform.localScale.y > 0f)
		{
			base.StartCoroutine(this.AnimatePanelClose());
		}
		else
		{
			this.scale.y = 0f;
			this.MenuPanel.transform.localScale = this.scale;
		}
		yield break;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003DF3 File Offset: 0x000021F3
	public void shareImage()
	{
		UnityEngine.Object.FindObjectOfType<CommonScript>().ShareImage(this.fillTexture);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003E05 File Offset: 0x00002205
	public void SaveImageToGallery()
	{
		base.StartCoroutine(this.SaveToGallery());
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003E14 File Offset: 0x00002214
	private IEnumerator SaveToGallery()
	{
		yield return this.fillTexture;
		ScreenshotManager.SaveImageGallery(this.fillTexture, HomeScript.selectedimage + "PixelArt", "png");
		yield break;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003E30 File Offset: 0x00002230
	private IEnumerator saveTempImage()
	{
		yield return this.fillTexture;
		string ImageName = HomeScript.selectedimage + "grey";
		ScreenshotManager.SaveTempImage(this.fillTexture, ImageName, "png");
		Debug.LogError("Saving Image");
		SceneManager.LoadScene(1);
		yield break;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003E4C File Offset: 0x0000224C
	private void Brush(Vector2 p1, Vector2 p2)
	{
		Drawing.NumSamples = this.AntiAlias;
		if (p2 == Vector2.zero)
		{
			p2 = p1;
		}
		this.from = p1;
		this.from += Vector2.one;
		this._x = this.from.x / 1f;
		this._y = this.from.y / 1f;
		this._x = (float)Mathf.CeilToInt(this._x);
		this._y = (float)Mathf.CeilToInt(this._y);
		this.from.x = this._x * 1f;
		this.from.y = this._y * 1f;
		this.tempIndex = (int)((this.from.y - 1f) * (float)this.fillTexture.width + this.from.x - 1f);
		if (textwritten.TextsObj[this.tempIndex].GetComponent<Text>().IsActive())
		{
			Drawing.PaintOutBoundryPatterns(this.fillTexture, this.patternTexture, p1, p2, this.brushRadious, 1f);
			this.fillTexture.Apply();
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003F94 File Offset: 0x00002394
	public bool GetSpritePixelColorUnderMousePointer(SpriteRenderer spriteRenderer, out Color color, out Vector2 position)
	{
		color = default(Color);
		position = Vector2.zero;
		Camera main = Camera.main;
		Vector2 v = Input.mousePosition;
		Vector2 v2 = main.ScreenToViewportPoint(v);
		if (v2.x < 0f || v2.x > 1f || v2.y < 0f || v2.y > 1f)
		{
			return false;
		}
		Ray ray = main.ViewportPointToRay(v2);
		return this.IntersectsSprite(spriteRenderer, ray, out color, out position);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00004034 File Offset: 0x00002434
	private bool IntersectsSprite(SpriteRenderer spriteRenderer, Ray ray, out Color color, out Vector2 position)
	{
		color = default(Color);
		position = Vector2.zero;
		if (spriteRenderer == null)
		{
			return false;
		}
		Sprite sprite = spriteRenderer.sprite;
		if (sprite == null)
		{
			return false;
		}
		Texture2D texture = sprite.texture;
		if (texture == null)
		{
			return false;
		}
		if (sprite.packed && sprite.packingMode == SpritePackingMode.Tight)
		{
			Debug.LogError("SpritePackingMode.Tight atlas packing is not supported!");
			return false;
		}
		Plane plane = new Plane(base.transform.forward, base.transform.position);
		float d;
		if (!plane.Raycast(ray, out d))
		{
			return false;
		}
		Vector3 vector = spriteRenderer.worldToLocalMatrix.MultiplyPoint3x4(ray.origin + ray.direction * d);
		Rect textureRect = sprite.textureRect;
		float pixelsPerUnit = sprite.pixelsPerUnit;
		float num = (float)texture.width * 0.5f;
		float num2 = (float)texture.height * 0.5f;
		int num3 = (int)(vector.x * pixelsPerUnit + num);
		int num4 = (int)(vector.y * pixelsPerUnit + num2);
		if (num3 < 0 || (float)num3 < textureRect.x || num3 >= Mathf.FloorToInt(textureRect.xMax))
		{
			return false;
		}
		if (num4 < 0 || (float)num4 < textureRect.y || num4 >= Mathf.FloorToInt(textureRect.yMax))
		{
			return false;
		}
		color = texture.GetPixel(num3, num4);
		position = new Vector2((float)num3, (float)num4);
		return true;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000041CA File Offset: 0x000025CA
	public void SaveTextureToFile(string filename)
	{
		File.WriteAllBytes(filename, this.fillTexture.EncodeToPNG());
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000041E0 File Offset: 0x000025E0
	private IEnumerator loadImage(WWW www, string spriteName, Texture2D tex)
	{
		yield return www;
		if (www.text != null && !www.text.Equals(string.Empty))
		{
			Sprite sprite = Sprite.Create(www.texture, new Rect(0f, 0f, (float)www.texture.width, (float)www.texture.height), new Vector2(1f, 1f));
			this.fillTexture.SetPixels32(sprite.texture.GetPixels32());
			this.fillTexture.Apply();
			UnityEngine.Object.FindObjectOfType<textwritten>().GreyCheckFun();
			Debug.LogError("|||||||||Saved image found");
		}
		else
		{
			this.fillTexture = this.FillSprite.texture;
		}
		yield break;
	}

	// Token: 0x0400002B RID: 43
	public SpriteRenderer fillRenderer;

	// Token: 0x0400002C RID: 44
	public SpriteRenderer patternRenderer;

	// Token: 0x0400002D RID: 45
	private Sprite FillSprite;

	// Token: 0x0400002E RID: 46
	private Sprite PatterSprite;

	// Token: 0x0400002F RID: 47
	private Texture2D fillTexture;

	// Token: 0x04000030 RID: 48
	private Texture2D patternTexture;

	// Token: 0x04000031 RID: 49
	private Vector2 preDrag;

	// Token: 0x04000032 RID: 50
	public float brushRadious;

	// Token: 0x04000033 RID: 51
	private Drawing.Samples AntiAlias = Drawing.Samples.Samples32;

	// Token: 0x04000034 RID: 52
	private bool enablePainting;

	// Token: 0x04000035 RID: 53
	private bool ondownpaint = true;

	// Token: 0x04000036 RID: 54
	private Vector2 from;

	// Token: 0x04000037 RID: 55
	private float _x;

	// Token: 0x04000038 RID: 56
	private float _y;

	// Token: 0x04000039 RID: 57
	private int tempIndex;

	// Token: 0x0400003A RID: 58
	private float TouchDeltaPos = 2f;

	// Token: 0x0400003B RID: 59
	private Vector2 TDownPos = new Vector2(0f, 0f);

	// Token: 0x0400003C RID: 60
	private Vector2 TUpPos = new Vector2(0f, 0f);

	// Token: 0x0400003D RID: 61
	public static bool textureSaving;

	// Token: 0x0400003E RID: 62
	public GameObject ImageBoard;

	// Token: 0x0400003F RID: 63
	public bool BrushEnable;

	// Token: 0x04000040 RID: 64
	private testPan panning_script;

	// Token: 0x04000041 RID: 65
	public Sprite[] brushes;

	// Token: 0x04000042 RID: 66
	public Sprite[] arrows;

	// Token: 0x04000043 RID: 67
	public GameObject MenuPanel;

	// Token: 0x04000044 RID: 68
	private Vector3 scale;
}
