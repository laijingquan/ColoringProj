using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000E RID: 14
public static class TextureExtension
{
	// Token: 0x06000052 RID: 82 RVA: 0x000047B4 File Offset: 0x00002BB4
	public static void FloodFillArea(this Texture2D aTex, int aX, int aY, Color aFillColor)
	{
		int width = aTex.width;
		int height = aTex.height;
		Color[] pixels = aTex.GetPixels();
		Color color = pixels[aX + aY * width];
		if (color == Color.black)
		{
			return;
		}
		Queue<TextureExtension.Point> queue = new Queue<TextureExtension.Point>();
		queue.Enqueue(new TextureExtension.Point(aX, aY));
		while (queue.Count > 0)
		{
			TextureExtension.Point point = queue.Dequeue();
			for (int i = (int)point.x; i < width; i++)
			{
				Color lhs = pixels[i + (int)point.y * width];
				if (lhs != color || lhs == aFillColor)
				{
					break;
				}
				pixels[i + (int)point.y * width] = aFillColor;
				if ((int)(point.y + 1) < height)
				{
					lhs = pixels[i + (int)point.y * width + width];
					if (lhs == color && lhs != aFillColor)
					{
						queue.Enqueue(new TextureExtension.Point(i, (int)(point.y + 1)));
					}
				}
				if (point.y - 1 >= 0)
				{
					lhs = pixels[i + (int)point.y * width - width];
					if (lhs == color && lhs != aFillColor)
					{
						queue.Enqueue(new TextureExtension.Point(i, (int)(point.y - 1)));
					}
				}
			}
			for (int j = (int)(point.x - 1); j >= 0; j--)
			{
				Color lhs2 = pixels[j + (int)point.y * width];
				if (lhs2 != color || lhs2 == aFillColor)
				{
					break;
				}
				pixels[j + (int)point.y * width] = aFillColor;
				if ((int)(point.y + 1) < height)
				{
					lhs2 = pixels[j + (int)point.y * width + width];
					if (lhs2 == color && lhs2 != aFillColor)
					{
						queue.Enqueue(new TextureExtension.Point(j, (int)(point.y + 1)));
					}
				}
				if (point.y - 1 >= 0)
				{
					lhs2 = pixels[j + (int)point.y * width - width];
					if (lhs2 == color && lhs2 != aFillColor)
					{
						queue.Enqueue(new TextureExtension.Point(j, (int)(point.y - 1)));
					}
				}
			}
		}
		aTex.SetPixels(pixels);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004A78 File Offset: 0x00002E78
	public static void FloodFillBorder(this Texture2D aTex, int aX, int aY, Color aFillColor, Color aBorderColor)
	{
		int width = aTex.width;
		int height = aTex.height;
		Color[] pixels = aTex.GetPixels();
		byte[] array = new byte[pixels.Length];
		Queue<TextureExtension.Point> queue = new Queue<TextureExtension.Point>();
		queue.Enqueue(new TextureExtension.Point(aX, aY));
		while (queue.Count > 0)
		{
			TextureExtension.Point point = queue.Dequeue();
			for (int i = (int)point.x; i < width; i++)
			{
				if (array[i + (int)point.y * width] > 0 || pixels[i + (int)point.y * width] == aBorderColor)
				{
					break;
				}
				pixels[i + (int)point.y * width] = aFillColor;
				array[i + (int)point.y * width] = 1;
				if ((int)(point.y + 1) < height && array[i + (int)point.y * width + width] == 0 && pixels[i + (int)point.y * width + width] != aBorderColor)
				{
					queue.Enqueue(new TextureExtension.Point(i, (int)(point.y + 1)));
				}
				if (point.y - 1 >= 0 && array[i + (int)point.y * width - width] == 0 && pixels[i + (int)point.y * width - width] != aBorderColor)
				{
					queue.Enqueue(new TextureExtension.Point(i, (int)(point.y - 1)));
				}
			}
			for (int j = (int)(point.x - 1); j >= 0; j--)
			{
				if (array[j + (int)point.y * width] > 0 || pixels[j + (int)point.y * width] == aBorderColor)
				{
					break;
				}
				pixels[j + (int)point.y * width] = aFillColor;
				array[j + (int)point.y * width] = 1;
				if ((int)(point.y + 1) < height && array[j + (int)point.y * width + width] == 0 && pixels[j + (int)point.y * width + width] != aBorderColor)
				{
					queue.Enqueue(new TextureExtension.Point(j, (int)(point.y + 1)));
				}
				if (point.y - 1 >= 0 && array[j + (int)point.y * width - width] == 0 && pixels[j + (int)point.y * width - width] != aBorderColor)
				{
					queue.Enqueue(new TextureExtension.Point(j, (int)(point.y - 1)));
				}
			}
		}
		aTex.SetPixels(pixels);
	}

	// Token: 0x0200000F RID: 15
	public struct Point
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00004D5E File Offset: 0x0000315E
		public Point(short aX, short aY)
		{
			this.x = aX;
			this.y = aY;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004D6E File Offset: 0x0000316E
		public Point(int aX, int aY)
		{
			this = new TextureExtension.Point((short)aX, (short)aY);
		}

		// Token: 0x04000045 RID: 69
		public short x;

		// Token: 0x04000046 RID: 70
		public short y;
	}
}
