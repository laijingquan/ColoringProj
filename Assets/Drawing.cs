using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000007 RID: 7
public class Drawing
{
	// Token: 0x06000010 RID: 16 RVA: 0x00002350 File Offset: 0x00000750
	public static Texture2D PaintOutBoundryPatterns(Texture2D texture, Texture2D paternText, Vector2 from, Vector2 to, float rad, float hardness)
	{
		rad = 1f;
		from += Vector2.one;
		float num = from.x / rad;
		float num2 = from.y / rad;
		num = (float)Mathf.CeilToInt(num);
		num2 = (float)Mathf.CeilToInt(num2);
		from.x = num * rad;
		from.y = num2 * rad;
		to = from;
		float num3 = rad;
		float num4 = Mathf.Clamp(Mathf.Min(from.y, to.y) - num3, 0f, (float)texture.height);
		float num5 = Mathf.Clamp(Mathf.Min(from.x, to.x) - num3, 0f, (float)texture.width);
		float num6 = Mathf.Clamp(Mathf.Max(from.y, to.y) + num3, 0f, (float)texture.height);
		float num7 = Mathf.Clamp(Mathf.Max(from.x, to.x) + num3, 0f, (float)texture.width);
		float num8 = rad;
		float num9 = rad;
		float num10 = (rad + 1f) * (rad + 1f);
		Color[] pixels = texture.GetPixels((int)num5, (int)num4, (int)num8, (int)num9, 0);
		Vector2 b = new Vector2(num5, num4);
		Color[] pixels2 = paternText.GetPixels((int)b.x, (int)b.y, (int)num8, (int)num9, 0);
		for (int i = 0; i < (int)num9; i++)
		{
			for (int j = 0; j < (int)num8; j++)
			{
				Vector2 a = new Vector2((float)j, (float)i) + b;
				Vector2 vector = a + new Vector2(0.5f, 0.5f);
				Color color;
				if (pixels[i * (int)num8 + j].a >= 0.5f)
				{
					color = textwritten.FinalColor;
					int index = (int)((from.y - 1f) * (float)texture.width + from.x - 1f);
					if (textwritten.TextsObj[index].GetComponent<Text>().text == textwritten.FinalTxtIndex + string.Empty)
					{
						textwritten.TextsObj[index].GetComponent<Text>().enabled = false;
						textwritten.correctColors++;
						EventManager.CheckImage();
					}
					else
					{
						textwritten.TextsObj[index].GetComponent<Text>().enabled = true;
					}
				}
				else
				{
					color = pixels[i * (int)num8 + j];
				}
				pixels[i * (int)num8 + j] = color;
			}
		}
		texture.SetPixels((int)b.x, (int)b.y, (int)num8, (int)num9, pixels, 0);
		return texture;
	}

	// Token: 0x04000013 RID: 19
	public static Drawing.Samples NumSamples = Drawing.Samples.Samples4;

	// Token: 0x02000008 RID: 8
	public enum Samples
	{
		// Token: 0x04000015 RID: 21
		None,
		// Token: 0x04000016 RID: 22
		Samples2,
		// Token: 0x04000017 RID: 23
		Samples4,
		// Token: 0x04000018 RID: 24
		Samples8,
		// Token: 0x04000019 RID: 25
		Samples16,
		// Token: 0x0400001A RID: 26
		Samples32,
		// Token: 0x0400001B RID: 27
		RotatedDisc
	}
}
