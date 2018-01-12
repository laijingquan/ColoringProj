using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[Serializable]
public struct HSBColor
{
	// Token: 0x06000028 RID: 40 RVA: 0x00002BC8 File Offset: 0x00000FC8
	public HSBColor(float h, float s, float b, float a)
	{
		this.h = h;
		this.s = s;
		this.b = b;
		this.a = a;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002BE7 File Offset: 0x00000FE7
	public HSBColor(float h, float s, float b)
	{
		this.h = h;
		this.s = s;
		this.b = b;
		this.a = 1f;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002C0C File Offset: 0x0000100C
	public HSBColor(Color col)
	{
		HSBColor hsbcolor = HSBColor.FromColor(col);
		this.h = hsbcolor.h;
		this.s = hsbcolor.s;
		this.b = hsbcolor.b;
		this.a = hsbcolor.a;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002C54 File Offset: 0x00001054
	public static HSBColor FromColor(Color color)
	{
		HSBColor result = new HSBColor(0f, 0f, 0f, color.a);
		float r = color.r;
		float g = color.g;
		float num = color.b;
		float num2 = Mathf.Max(r, Mathf.Max(g, num));
		if (num2 <= 0f)
		{
			return result;
		}
		float num3 = Mathf.Min(r, Mathf.Min(g, num));
		float num4 = num2 - num3;
		if (num2 > num3)
		{
			if (g == num2)
			{
				result.h = (num - r) / num4 * 60f + 120f;
			}
			else if (num == num2)
			{
				result.h = (r - g) / num4 * 60f + 240f;
			}
			else if (num > g)
			{
				result.h = (g - num) / num4 * 60f + 360f;
			}
			else
			{
				result.h = (g - num) / num4 * 60f;
			}
			if (result.h < 0f)
			{
				result.h += 360f;
			}
		}
		else
		{
			result.h = 0f;
		}
		result.h *= 0.00277777785f;
		result.s = num4 / num2 * 1f;
		result.b = num2;
		return result;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002DBC File Offset: 0x000011BC
	public static Color ToColor(HSBColor hsbColor)
	{
		float value = hsbColor.b;
		float value2 = hsbColor.b;
		float value3 = hsbColor.b;
		if (hsbColor.s != 0f)
		{
			float num = hsbColor.b;
			float num2 = hsbColor.b * hsbColor.s;
			float num3 = hsbColor.b - num2;
			float num4 = hsbColor.h * 360f;
			if (num4 < 60f)
			{
				value = num;
				value2 = num4 * num2 / 60f + num3;
				value3 = num3;
			}
			else if (num4 < 120f)
			{
				value = -(num4 - 120f) * num2 / 60f + num3;
				value2 = num;
				value3 = num3;
			}
			else if (num4 < 180f)
			{
				value = num3;
				value2 = num;
				value3 = (num4 - 120f) * num2 / 60f + num3;
			}
			else if (num4 < 240f)
			{
				value = num3;
				value2 = -(num4 - 240f) * num2 / 60f + num3;
				value3 = num;
			}
			else if (num4 < 300f)
			{
				value = (num4 - 240f) * num2 / 60f + num3;
				value2 = num3;
				value3 = num;
			}
			else if (num4 <= 360f)
			{
				value = num;
				value2 = num3;
				value3 = -(num4 - 360f) * num2 / 60f + num3;
			}
			else
			{
				value = 0f;
				value2 = 0f;
				value3 = 0f;
			}
		}
		return new Color(Mathf.Clamp01(value), Mathf.Clamp01(value2), Mathf.Clamp01(value3), hsbColor.a);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002F55 File Offset: 0x00001355
	public Color ToColor()
	{
		return HSBColor.ToColor(this);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002F64 File Offset: 0x00001364
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"H:",
			this.h,
			" S:",
			this.s,
			" B:",
			this.b
		});
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002FC0 File Offset: 0x000013C0
	public static HSBColor Lerp(HSBColor a, HSBColor b, float t)
	{
		float num;
		for (num = Mathf.LerpAngle(a.h * 360f, b.h * 360f, t); num < 0f; num += 360f)
		{
		}
		while (num > 360f)
		{
			num -= 360f;
		}
		return new HSBColor(num / 360f, Mathf.Lerp(a.s, b.s, t), Mathf.Lerp(a.b, b.b, t), Mathf.Lerp(a.a, b.a, t));
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003068 File Offset: 0x00001468
	public static void Test()
	{
		HSBColor hsbcolor = new HSBColor(Color.red);
		Debug.Log("red: " + hsbcolor);
		hsbcolor = new HSBColor(Color.green);
		Debug.Log("green: " + hsbcolor);
		hsbcolor = new HSBColor(Color.blue);
		Debug.Log("blue: " + hsbcolor);
		hsbcolor = new HSBColor(Color.grey);
		Debug.Log("grey: " + hsbcolor);
		hsbcolor = new HSBColor(Color.white);
		Debug.Log("white: " + hsbcolor);
		hsbcolor = new HSBColor(new Color(0.4f, 1f, 0.84f, 1f));
		Debug.Log("0.4, 1f, 0.84: " + hsbcolor);
		Debug.Log("164,82,84   .... 0.643137f, 0.321568f, 0.329411f  :" + HSBColor.ToColor(new HSBColor(new Color(0.643137f, 0.321568f, 0.329411f))));
	}

	// Token: 0x0400001C RID: 28
	public float h;

	// Token: 0x0400001D RID: 29
	public float s;

	// Token: 0x0400001E RID: 30
	public float b;

	// Token: 0x0400001F RID: 31
	public float a;
}
