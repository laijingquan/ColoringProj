using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000011 RID: 17
public class textwritten : MonoBehaviour
{
	// Token: 0x0600005F RID: 95 RVA: 0x00005245 File Offset: 0x00003645
	private void OnEnable()
	{
		EventManager.CorrectColor += this.OnImageComplete;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00005258 File Offset: 0x00003658
	private void OnDisable()
	{
		EventManager.CorrectColor -= this.OnImageComplete;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000526B File Offset: 0x0000366B
	private void Awake()
	{
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00005270 File Offset: 0x00003670
	private void Start()
	{
		textwritten.correctColors = 0;
		textwritten.TotalColorsPixels = 0;
		this.spr = this.ImageBoard.transform.GetChild(0).GetComponent<SpriteRenderer>();
		this.Greyspr = this.ImageBoard.transform.GetChild(1).GetComponent<SpriteRenderer>();
		textwritten.TextsObj.Clear();
		this.rgb.Clear();
		this.tex = this.spr.sprite.texture;
		textwritten.tempwidth = (float)this.tex.width;
		textwritten.tempheight = (float)this.tex.height;
		if (textwritten.tempwidth > textwritten.tempheight)
		{
			textwritten.meshsize = textwritten.tempwidth;
		}
		else
		{
			textwritten.meshsize = textwritten.tempheight;
		}
		for (int i = 0; i < this.tex.height; i++)
		{
			for (int j = 0; j < this.tex.width; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.textObj);
                gameObject.transform.SetParent(this.textObj.transform.parent);
				textwritten.TextsObj.Add(gameObject);
				gameObject.GetComponent<Text>().text = string.Empty;
				if (this.tex.GetPixel(j, i).a >= 0.5f)
				{
					if (this.rgb.Count == 0)
					{
						this.CurrentRGB = new Vector3(this.tex.GetPixel(j, i).r, this.tex.GetPixel(j, i).g, this.tex.GetPixel(j, i).b);
						this.CurrentRGB.x = (float)Math.Round((double)this.CurrentRGB.x, 1);
						this.CurrentRGB.y = (float)Math.Round((double)this.CurrentRGB.y, 1);
						this.CurrentRGB.z = (float)Math.Round((double)this.CurrentRGB.z, 1);
						this.rgb.Add(this.CurrentRGB);
						gameObject.GetComponent<Text>().text = this.rgb.IndexOf(this.CurrentRGB) + string.Empty;
						gameObject.name = "Text" + this.rgb.IndexOf(this.CurrentRGB);
						if ((double)((this.CurrentRGB.x * 299f + this.CurrentRGB.y * 587f + this.CurrentRGB.z * 114f) / 1000f) > 0.5)
						{
							gameObject.GetComponentInChildren<Text>().color = Color.black;
							this.textColors.Add(Color.black);
						}
						else
						{
							gameObject.GetComponentInChildren<Text>().color = Color.white;
							this.textColors.Add(Color.white);
						}
					}
					else
					{
						this.CurrentRGB = new Vector3(this.tex.GetPixel(j, i).r, this.tex.GetPixel(j, i).g, this.tex.GetPixel(j, i).b);
						this.CurrentRGB.x = (float)Math.Round((double)this.CurrentRGB.x, 1);
						this.CurrentRGB.y = (float)Math.Round((double)this.CurrentRGB.y, 1);
						this.CurrentRGB.z = (float)Math.Round((double)this.CurrentRGB.z, 1);
						if (this.rgb.Contains(this.CurrentRGB))
						{
							gameObject.GetComponent<Text>().text = this.rgb.IndexOf(this.CurrentRGB) + string.Empty;
							gameObject.name = "Text" + this.rgb.IndexOf(this.CurrentRGB);
							if ((double)((this.CurrentRGB.x * 299f + this.CurrentRGB.y * 587f + this.CurrentRGB.z * 114f) / 1000f) > 0.5)
							{
								gameObject.GetComponentInChildren<Text>().color = Color.black;
								this.textColors.Add(Color.black);
							}
							else
							{
								gameObject.GetComponentInChildren<Text>().color = Color.white;
								this.textColors.Add(Color.white);
							}
						}
						else
						{
							this.CurrentRGB.x = (float)Math.Round((double)this.CurrentRGB.x, 1);
							this.CurrentRGB.y = (float)Math.Round((double)this.CurrentRGB.y, 1);
							this.CurrentRGB.z = (float)Math.Round((double)this.CurrentRGB.z, 1);
							this.rgb.Add(this.CurrentRGB);
							gameObject.GetComponent<Text>().text = this.rgb.IndexOf(this.CurrentRGB) + string.Empty;
							gameObject.name = "Text" + this.rgb.IndexOf(this.CurrentRGB);
							if ((double)((this.CurrentRGB.x * 299f + this.CurrentRGB.y * 587f + this.CurrentRGB.z * 114f) / 1000f) > 0.5)
							{
								gameObject.GetComponentInChildren<Text>().color = Color.black;
								this.textColors.Add(Color.black);
							}
							else
							{
								gameObject.GetComponentInChildren<Text>().color = Color.white;
								this.textColors.Add(Color.white);
							}
						}
					}
				}
				else
				{
					this.textColors.Add(Color.white);
				}
				gameObject.transform.localPosition = new Vector2((float)j, (float)i) - new Vector2((float)(this.tex.width / 2), (float)(this.tex.height / 2));
				gameObject.SetActive(true);
			}
		}
		for (int k = 0; k < this.rgb.Count; k++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.ColorButton);
            gameObject2.transform.SetParent(this.ColorButton.transform.parent);
			gameObject2.name = string.Empty + k;
			gameObject2.tag = "ColorButton";
			if ((double)((this.rgb[k].x * 299f + this.rgb[k].y * 587f + this.rgb[k].z * 114f) / 1000f) > 0.5)
			{
				gameObject2.GetComponentInChildren<Text>().color = Color.black;
			}
			else
			{
				gameObject2.GetComponentInChildren<Text>().color = Color.white;
			}
			gameObject2.GetComponentInChildren<Text>().text = k + string.Empty;
			gameObject2.GetComponent<Image>().color = new Color(this.rgb[k].x, this.rgb[k].y, this.rgb[k].z);
			gameObject2.SetActive(true);
			this.ColorButtons.Add(gameObject2);
		}
		this.ColorButton.transform.parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)this.rgb.Count * 130f);
		textwritten.FinalColor = new Color(this.rgb[0].x, this.rgb[0].y, this.rgb[0].z);
		this.GreyCheckFun();
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00005AB0 File Offset: 0x00003EB0
	public void OnPaletClick()
	{
		this.ResetcolorButtonSizes();
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		currentSelectedGameObject.transform.localScale = Vector3.one * 0.75f;
		textwritten.FinalColor = currentSelectedGameObject.GetComponent<Image>().color;
		textwritten.FinalTxtIndex = int.Parse(currentSelectedGameObject.name);
		for (int i = 0; i < this.tex.width * this.tex.height; i++)
		{
			Text component = textwritten.TextsObj[i].GetComponent<Text>();
			component.color = this.textColors[i];
			if (textwritten.TextsObj[i].name == "Text" + currentSelectedGameObject.name)
			{
				textwritten.TextsObj[i].GetComponent<Text>().color = Color.green;
			}
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00005B97 File Offset: 0x00003F97
	public void backtohome()
	{
		UnityEngine.Object.FindObjectOfType<PaintingScript>().saveMyProgress();
		Ads_Script.current.ShowFullScreenAd();
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00005BB0 File Offset: 0x00003FB0
	private void ResetcolorButtonSizes()
	{
		foreach (GameObject gameObject in this.ColorButtons)
		{
			gameObject.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00005C18 File Offset: 0x00004018
	public void GreyCheckFun()
	{
		this.grey = this.Greyspr.sprite.texture;
		for (int i = 0; i < this.grey.height; i++)
		{
			for (int j = 0; j < this.grey.width; j++)
			{
				if (this.grey.GetPixel(j, i).a >= 0.5f)
				{
					textwritten.TotalColorsPixels++;
					int index = i * this.grey.width + j;
					Vector3 item = new Vector3(this.grey.GetPixel(j, i).r, this.grey.GetPixel(j, i).g, this.grey.GetPixel(j, i).b);
					item.x = (float)Math.Round((double)item.x, 1);
					item.y = (float)Math.Round((double)item.y, 1);
					item.z = (float)Math.Round((double)item.z, 1);
					if (this.rgb.Contains(item))
					{
						string value = this.rgb.IndexOf(item) + string.Empty;
						if (textwritten.TextsObj[index].GetComponent<Text>().text.Equals(value))
						{
							textwritten.correctColors++;
							textwritten.TextsObj[index].GetComponent<Text>().enabled = false;
						}
						else
						{
							textwritten.TextsObj[index].GetComponent<Text>().enabled = true;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00005DC7 File Offset: 0x000041C7
	public void hideCompletionPanel()
	{
		this.ImageCompletePanel.SetActive(false);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00005DD5 File Offset: 0x000041D5
	private void OnImageComplete()
	{
		if (textwritten.correctColors == textwritten.TotalColorsPixels)
		{
			this.ImageCompletePanel.SetActive(true);
			this.partciles.SetActive(true);
		}
	}

	// Token: 0x04000054 RID: 84
	public GameObject ColorButton;

	// Token: 0x04000055 RID: 85
	public RectTransform ColorButtonParent;

	// Token: 0x04000056 RID: 86
	public static Color FinalColor;

	// Token: 0x04000057 RID: 87
	public static int FinalTxtIndex = 0;

	// Token: 0x04000058 RID: 88
	private Texture2D tex;

	// Token: 0x04000059 RID: 89
	private Texture2D grey;

	// Token: 0x0400005A RID: 90
	public GameObject textObj;

	// Token: 0x0400005B RID: 91
	public SpriteRenderer spr;

	// Token: 0x0400005C RID: 92
	public SpriteRenderer Greyspr;

	// Token: 0x0400005D RID: 93
	public List<Vector3> rgb = new List<Vector3>();

	// Token: 0x0400005E RID: 94
	private Vector3 CurrentRGB;

	// Token: 0x0400005F RID: 95
	private bool found;

	// Token: 0x04000060 RID: 96
	public static List<GameObject> TextsObj = new List<GameObject>();

	// Token: 0x04000061 RID: 97
	private List<Color> textColors = new List<Color>();

	// Token: 0x04000062 RID: 98
	private List<GameObject> ColorButtons = new List<GameObject>();

	// Token: 0x04000063 RID: 99
	public static float meshsize = 10f;

	// Token: 0x04000064 RID: 100
	public static float tempwidth;

	// Token: 0x04000065 RID: 101
	public static float tempheight;

	// Token: 0x04000066 RID: 102
	public GameObject ImageBoard;

	// Token: 0x04000067 RID: 103
	public static int correctColors = 0;

	// Token: 0x04000068 RID: 104
	public static int TotalColorsPixels = 0;

	// Token: 0x04000069 RID: 105
	public GameObject ImageCompletePanel;

	// Token: 0x0400006A RID: 106
	public GameObject partciles;
}
