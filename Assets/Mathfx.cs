using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class Mathfx : MonoBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x0000262D File Offset: 0x00000A2D
	public static float Hermite(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, value * value * (3f - 2f * value));
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002647 File Offset: 0x00000A47
	public static float Sinerp(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, Mathf.Sin(value * 3.14159274f * 0.5f));
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002662 File Offset: 0x00000A62
	public static float Coserp(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, 1f - Mathf.Cos(value * 3.14159274f * 0.5f));
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002684 File Offset: 0x00000A84
	public static float Berp(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * 3.14159274f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
		return start + (end - start) * value;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000026E8 File Offset: 0x00000AE8
	public static float SmoothStep(float x, float min, float max)
	{
		x = Mathf.Clamp(x, min, max);
		float num = (x - min) / (max - min);
		float num2 = (x - min) / (max - min);
		return -2f * num * num * num + 3f * num2 * num2;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002724 File Offset: 0x00000B24
	public static float Lerp(float start, float end, float value)
	{
		return (1f - value) * start + value * end;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002734 File Offset: 0x00000B34
	public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 vector = Vector3.Normalize(lineEnd - lineStart);
		float d = Vector3.Dot(point - lineStart, vector) / Vector3.Dot(vector, vector);
		return lineStart + d * vector;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002774 File Offset: 0x00000B74
	public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
	{
		Vector3 vector = lineEnd - lineStart;
		Vector3 vector2 = Vector3.Normalize(vector);
		float value = Vector3.Dot(point - lineStart, vector2) / Vector3.Dot(vector2, vector2);
		return lineStart + Mathf.Clamp(value, 0f, Vector3.Magnitude(vector)) * vector2;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000027C4 File Offset: 0x00000BC4
	public static Vector2 NearestPointStrict(Vector2 lineStart, Vector2 lineEnd, Vector2 point)
	{
		Vector2 p = lineEnd - lineStart;
		Vector2 vector = Mathfx.Normalize(p);
		float value = Vector2.Dot(point - lineStart, vector) / Vector2.Dot(vector, vector);
		return lineStart + Mathf.Clamp(value, 0f, p.magnitude) * vector;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002814 File Offset: 0x00000C14
	public static float Bounce(float x)
	{
		return Mathf.Abs(Mathf.Sin(6.28f * (x + 1f) * (x + 1f)) * (1f - x));
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000283D File Offset: 0x00000C3D
	public static bool Approx(float val, float about, float range)
	{
		return Mathf.Abs(val - about) < range;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000284C File Offset: 0x00000C4C
	public static bool Approx(Vector3 val, Vector3 about, float range)
	{
		return (val - about).sqrMagnitude < range * range;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000286D File Offset: 0x00000C6D
	public static float GaussFalloff(float distance, float inRadius)
	{
		return Mathf.Clamp01(Mathf.Pow(360f, -Mathf.Pow(distance / inRadius, 2.5f) - 0.01f));
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002894 File Offset: 0x00000C94
	public static float Clerp(float start, float end, float value)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float result;
		if (end - start < -num3)
		{
			float num4 = (num2 - start + end) * value;
			result = start + num4;
		}
		else if (end - start > num3)
		{
			float num4 = -(num2 - end + start) * value;
			result = start + num4;
		}
		else
		{
			result = start + (end - start) * value;
		}
		return result;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000290C File Offset: 0x00000D0C
	public static Vector2 RotateVector(Vector2 vector, float rad)
	{
		rad *= 0.0174532924f;
		Vector2 result = new Vector2(vector.x * Mathf.Cos(rad) - vector.y * Mathf.Sin(rad), vector.x * Mathf.Sin(rad) + vector.y * Mathf.Cos(rad));
		return result;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002964 File Offset: 0x00000D64
	public static Vector2 IntersectPoint(Vector2 start1, Vector2 start2, Vector2 dir1, Vector2 dir2)
	{
		if (dir1.x == dir2.x)
		{
			return Vector2.zero;
		}
		float num = dir1.y / dir1.x;
		float num2 = dir2.y / dir2.x;
		if (num == num2)
		{
			return Vector2.zero;
		}
		Vector2 vector = new Vector2(num, start1.y - start1.x * num);
		Vector2 vector2 = new Vector2(num2, start2.y - start2.x * num2);
		float num3 = vector2.y - vector.y;
		float num4 = vector.x - vector2.x;
		float num5 = num3 / num4;
		float y = vector.x * num5 + vector.y;
		return new Vector2(num5, y);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002A30 File Offset: 0x00000E30
	public static Vector2 ThreePointCircle(Vector2 a1, Vector2 a2, Vector2 a3)
	{
		Vector2 vector = a2 - a1;
		vector /= 2f;
		Vector2 start = a1 + vector;
		vector = Mathfx.RotateVector(vector, 90f);
		Vector2 dir = vector;
		vector = a3 - a2;
		vector /= 2f;
		Vector2 start2 = a2 + vector;
		vector = Mathfx.RotateVector(vector, 90f);
		Vector2 dir2 = vector;
		return Mathfx.IntersectPoint(start, start2, dir, dir2);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002AA0 File Offset: 0x00000EA0
	public static Vector2 CubicBezier(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	{
		return Vector2.zero;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002AA8 File Offset: 0x00000EA8
	public static Vector2 NearestPointOnBezier(Vector2 p, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	{
		float num = float.PositiveInfinity;
		float num2 = 0f;
		Vector2 result = Vector2.zero;
		for (float num3 = 0f; num3 < 1f; num3 += 0.01f)
		{
			Vector2 vector = Mathfx.CubicBezier(num3, p0, p1, p2, p3);
			float sqrMagnitude = (p - vector).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				num2 = num3;
				result = vector;
			}
		}
		float num4 = Mathf.Clamp01(num2 - 0.01f);
		float num5 = Mathf.Clamp01(num2 + 0.01f);
		for (float num6 = num4; num6 < num5; num6 += 0.001f)
		{
			Vector2 vector2 = Mathfx.CubicBezier(num6, p0, p1, p2, p3);
			float sqrMagnitude2 = (p - vector2).sqrMagnitude;
			if (sqrMagnitude2 < num)
			{
				num = sqrMagnitude2;
				result = vector2;
			}
		}
		return result;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002B80 File Offset: 0x00000F80
	public static Vector2 NearestPointOnCircle(Vector2 p, Vector2 center, float w)
	{
		Vector2 vector = p - center;
		vector = Mathfx.Normalize(vector);
		vector *= w;
		return center + vector;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002BAC File Offset: 0x00000FAC
	public static Vector2 Normalize(Vector2 p)
	{
		float magnitude = p.magnitude;
		return p / magnitude;
	}
}
