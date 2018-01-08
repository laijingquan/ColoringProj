using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class CreatePatern : MonoBehaviour
{
	// Token: 0x06000038 RID: 56 RVA: 0x00003679 File Offset: 0x00001A79
	private void Start()
	{
		this.patternSprite = Resources.Load<Sprite>("Pattern");
		base.StartCoroutine(this.CreateTexture());
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00003698 File Offset: 0x00001A98
	private IEnumerator CreateTexture()
	{
		int SingleTexture_X = 0;
		int SingleTexture_Y = 0;
		Color32[] clr = this.patternSprite.texture.GetPixels32();
		Texture2D finalPattern_texture = this.finalPattern.sprite.texture;
		Color32[] finalPatternColor = finalPattern_texture.GetPixels32();
		Vector2 sizeSpr = this.patternSprite.rect.size;
		Vector2 size = this.finalPattern.sprite.rect.size;
		int num = 0;
		while ((float)num < size.y)
		{
			int num2 = 0;
			while ((float)num2 < size.x)
			{
				finalPatternColor[num * (int)size.x + num2] = clr[SingleTexture_Y * (int)sizeSpr.y + SingleTexture_X];
				if (SingleTexture_X >= (int)sizeSpr.x - 1)
				{
					SingleTexture_X = 0;
				}
				else
				{
					SingleTexture_X++;
				}
				num2++;
			}
			if (SingleTexture_Y >= (int)sizeSpr.y - 1)
			{
				SingleTexture_Y = 0;
			}
			else
			{
				SingleTexture_Y++;
			}
			num++;
		}
		yield return finalPatternColor;
		finalPattern_texture.SetPixels32(finalPatternColor);
		finalPattern_texture.Apply();
		yield break;
	}

	// Token: 0x04000029 RID: 41
	public SpriteRenderer finalPattern;

	// Token: 0x0400002A RID: 42
	private Sprite patternSprite;
}
