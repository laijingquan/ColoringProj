using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000025 RID: 37
public class DemoScript : MonoBehaviour
{
	// Token: 0x060000FE RID: 254 RVA: 0x00009298 File Offset: 0x00007698
	private void OnEnable()
	{
		ScreenshotManager.OnScreenshotTaken += this.ScreenshotTaken;
		ScreenshotManager.OnScreenshotSaved += this.ScreenshotSaved;
		ScreenshotManager.OnImageSaved += this.ImageSaved;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000092CD File Offset: 0x000076CD
	private void OnDisable()
	{
		ScreenshotManager.OnScreenshotTaken -= this.ScreenshotTaken;
		ScreenshotManager.OnScreenshotSaved -= this.ScreenshotSaved;
		ScreenshotManager.OnImageSaved -= this.ImageSaved;
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00009304 File Offset: 0x00007704
	public void OnSaveScreenshotPress()
	{
		ScreenshotManager.SaveScreenshot("MyScreenshot", "ScreenshotApp", "jpeg", default(Rect));
		if (this.hideGUI)
		{
			this.ui.alpha = 0f;
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00009349 File Offset: 0x00007749
	public void OnSaveImagePress()
	{
		ScreenshotManager.SaveImage(this.texture, "MyImage", "png", "png");
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00009368 File Offset: 0x00007768
	private void ScreenshotTaken(Texture2D image)
	{
		Text text = this.console;
		text.text += "\nScreenshot has been taken and is now saving...";
		this.screenshot.sprite = Sprite.Create(image, new Rect(0f, 0f, (float)image.width, (float)image.height), new Vector2(0.5f, 0.5f));
		this.screenshot.color = Color.white;
		this.ui.alpha = 1f;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x000093ED File Offset: 0x000077ED
	private void ScreenshotSaved(string path)
	{
		Text text = this.console;
		text.text = text.text + "\nScreenshot finished saving to " + path;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000940C File Offset: 0x0000780C
	private void ImageSaved(string path)
	{
		Text text = this.console;
		string text2 = text.text;
		text.text = string.Concat(new string[]
		{
			text2,
			"\n",
			this.texture.name,
			" finished saving to ",
			path
		});
	}

	// Token: 0x040000DC RID: 220
	public bool hideGUI;

	// Token: 0x040000DD RID: 221
	public Texture2D texture;

	// Token: 0x040000DE RID: 222
	public Text console;

	// Token: 0x040000DF RID: 223
	public CanvasGroup ui;

	// Token: 0x040000E0 RID: 224
	public Image screenshot;
}
