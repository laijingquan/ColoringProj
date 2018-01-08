using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000086 RID: 134
public static class ExtensionMethods
{
	// Token: 0x060005A5 RID: 1445 RVA: 0x000209CE File Offset: 0x0001EDCE
	public static bool IsNull<T>(this T o)
	{
		return o == null;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x000209DC File Offset: 0x0001EDDC
	public static bool IsUrl(this string source)
	{
		Uri uri;
		return !string.IsNullOrEmpty(source) && Uri.TryCreate(source, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00020A30 File Offset: 0x0001EE30
	public static void SetTexture2D(this Image image, Texture2D texture)
	{
		if (!image.IsNull<Image>())
		{
			image.overrideSprite = ((!(texture == null)) ? Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f)) : null);
		}
	}
}
