using System;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	// Token: 0x0200005B RID: 91
	internal class Utils
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000EEE0 File Offset: 0x0000D2E0
		public static Texture2D GetTexture2DFromByteArray(byte[] img)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			if (!texture2D.LoadImage(img))
			{
				throw new InvalidOperationException("Could not load custom native template\n                        image asset as texture");
			}
			return texture2D;
		}
	}
}
