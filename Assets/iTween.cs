using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class iTween : MonoBehaviour
{
	// Token: 0x06000482 RID: 1154 RVA: 0x00013CCE File Offset: 0x000120CE
	private iTween(Hashtable h)
	{
		this.tweenArguments = h;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00013CDD File Offset: 0x000120DD
	public static void Init(GameObject target)
	{
		iTween.MoveBy(target, Vector3.zero, 0f);
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00013CF0 File Offset: 0x000120F0
	public static void CameraFadeFrom(float amount, float time)
	{
		if (iTween.cameraFade)
		{
			iTween.CameraFadeFrom(iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}
		else
		{
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00013D4D File Offset: 0x0001214D
	public static void CameraFadeFrom(Hashtable args)
	{
		if (iTween.cameraFade)
		{
			iTween.ColorFrom(iTween.cameraFade, args);
		}
		else
		{
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00013D78 File Offset: 0x00012178
	public static void CameraFadeTo(float amount, float time)
	{
		if (iTween.cameraFade)
		{
			iTween.CameraFadeTo(iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}
		else
		{
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00013DD5 File Offset: 0x000121D5
	public static void CameraFadeTo(Hashtable args)
	{
		if (iTween.cameraFade)
		{
			iTween.ColorTo(iTween.cameraFade, args);
		}
		else
		{
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00013E00 File Offset: 0x00012200
	public static void ValueTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("onupdate") || !args.Contains("from") || !args.Contains("to"))
		{
			Debug.LogError("iTween Error: ValueTo() requires an 'onupdate' callback function and a 'from' and 'to' property.  The supplied 'onupdate' callback must accept a single argument that is the same type as the supplied 'from' and 'to' properties!");
			return;
		}
		args["type"] = "value";
		if (args["from"].GetType() == typeof(Vector2))
		{
			args["method"] = "vector2";
		}
		else if (args["from"].GetType() == typeof(Vector3))
		{
			args["method"] = "vector3";
		}
		else if (args["from"].GetType() == typeof(Rect))
		{
			args["method"] = "rect";
		}
		else if (args["from"].GetType() == typeof(float))
		{
			args["method"] = "float";
		}
		else
		{
			if (args["from"].GetType() != typeof(Color))
			{
				Debug.LogError("iTween Error: ValueTo() only works with interpolating Vector3s, Vector2s, floats, ints, Rects and Colors!");
				return;
			}
			args["method"] = "color";
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		iTween.Launch(target, args);
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00013F98 File Offset: 0x00012398
	public static void FadeFrom(GameObject target, float alpha, float time)
	{
		iTween.FadeFrom(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00013FCD File Offset: 0x000123CD
	public static void FadeFrom(GameObject target, Hashtable args)
	{
		iTween.ColorFrom(target, args);
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00013FD6 File Offset: 0x000123D6
	public static void FadeTo(GameObject target, float alpha, float time)
	{
		iTween.FadeTo(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0001400B File Offset: 0x0001240B
	public static void FadeTo(GameObject target, Hashtable args)
	{
		iTween.ColorTo(target, args);
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00014014 File Offset: 0x00012414
	public static void ColorFrom(GameObject target, Color color, float time)
	{
		iTween.ColorFrom(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0001404C File Offset: 0x0001244C
	public static void ColorFrom(GameObject target, Hashtable args)
	{
		Color color = default(Color);
		Color color2 = default(Color);
		args = iTween.CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					Hashtable hashtable = (Hashtable)args.Clone();
					hashtable["ischild"] = true;
					iTween.ColorFrom(transform.gameObject, hashtable);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		if (target.GetComponent<GUITexture>())
		{
			color = (color2 = target.GetComponent<GUITexture>().color);
		}
		else if (target.GetComponent<GUIText>())
		{
			color = (color2 = target.GetComponent<GUIText>().material.color);
		}
		else if (target.GetComponent<Renderer>())
		{
			color = (color2 = target.GetComponent<Renderer>().material.color);
		}
		else if (target.GetComponent<Light>())
		{
			color = (color2 = target.GetComponent<Light>().color);
		}
		if (args.Contains("color"))
		{
			color = (Color)args["color"];
		}
		else
		{
			if (args.Contains("r"))
			{
				color.r = (float)args["r"];
			}
			if (args.Contains("g"))
			{
				color.g = (float)args["g"];
			}
			if (args.Contains("b"))
			{
				color.b = (float)args["b"];
			}
			if (args.Contains("a"))
			{
				color.a = (float)args["a"];
			}
		}
		if (args.Contains("amount"))
		{
			color.a = (float)args["amount"];
			args.Remove("amount");
		}
		else if (args.Contains("alpha"))
		{
			color.a = (float)args["alpha"];
			args.Remove("alpha");
		}
		if (target.GetComponent<GUITexture>())
		{
			target.GetComponent<GUITexture>().color = color;
		}
		else if (target.GetComponent<GUIText>())
		{
			target.GetComponent<GUIText>().material.color = color;
		}
		else if (target.GetComponent<Renderer>())
		{
			target.GetComponent<Renderer>().material.color = color;
		}
		else if (target.GetComponent<Light>())
		{
			target.GetComponent<Light>().color = color;
		}
		args["color"] = color2;
		args["type"] = "color";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x000143B4 File Offset: 0x000127B4
	public static void ColorTo(GameObject target, Color color, float time)
	{
		iTween.ColorTo(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x000143EC File Offset: 0x000127EC
	public static void ColorTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					Hashtable hashtable = (Hashtable)args.Clone();
					hashtable["ischild"] = true;
					iTween.ColorTo(transform.gameObject, hashtable);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args["type"] = "color";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x000144EC File Offset: 0x000128EC
	public static void AudioFrom(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioFrom(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00014540 File Offset: 0x00012940
	public static void AudioFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		AudioSource audioSource;
		if (args.Contains("audiosource"))
		{
			audioSource = (AudioSource)args["audiosource"];
		}
		else
		{
			if (!target.GetComponent<AudioSource>())
			{
				Debug.LogError("iTween Error: AudioFrom requires an AudioSource.");
				return;
			}
			audioSource = target.GetComponent<AudioSource>();
		}
		Vector2 vector;
		Vector2 vector2;
		vector.x = (vector2.x = audioSource.volume);
		vector.y = (vector2.y = audioSource.pitch);
		if (args.Contains("volume"))
		{
			vector2.x = (float)args["volume"];
		}
		if (args.Contains("pitch"))
		{
			vector2.y = (float)args["pitch"];
		}
		audioSource.volume = vector2.x;
		audioSource.pitch = vector2.y;
		args["volume"] = vector.x;
		args["pitch"] = vector.y;
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args["type"] = "audio";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x000146B0 File Offset: 0x00012AB0
	public static void AudioTo(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioTo(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00014704 File Offset: 0x00012B04
	public static void AudioTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args["type"] = "audio";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00014762 File Offset: 0x00012B62
	public static void Stab(GameObject target, AudioClip audioclip, float delay)
	{
		iTween.Stab(target, iTween.Hash(new object[]
		{
			"audioclip",
			audioclip,
			"delay",
			delay
		}));
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00014792 File Offset: 0x00012B92
	public static void Stab(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "stab";
		iTween.Launch(target, args);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x000147B3 File Offset: 0x00012BB3
	public static void LookFrom(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookFrom(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x000147E8 File Offset: 0x00012BE8
	public static void LookFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3 eulerAngles = target.transform.eulerAngles;
		if (args["looktarget"].GetType() == typeof(Transform))
		{
			Transform transform = target.transform;
			Transform target2 = (Transform)args["looktarget"];
			Vector3? vector = (Vector3?)args["up"];
			transform.LookAt(target2, (vector == null) ? iTween.Defaults.up : vector.Value);
		}
		else if (args["looktarget"].GetType() == typeof(Vector3))
		{
			Transform transform2 = target.transform;
			Vector3 worldPosition = (Vector3)args["looktarget"];
			Vector3? vector2 = (Vector3?)args["up"];
			transform2.LookAt(worldPosition, (vector2 == null) ? iTween.Defaults.up : vector2.Value);
		}
		if (args.Contains("axis"))
		{
			Vector3 eulerAngles2 = target.transform.eulerAngles;
			string text = (string)args["axis"];
			if (text != null)
			{
				if (!(text == "x"))
				{
					if (!(text == "y"))
					{
						if (text == "z")
						{
							eulerAngles2.x = eulerAngles.x;
							eulerAngles2.y = eulerAngles.y;
						}
					}
					else
					{
						eulerAngles2.x = eulerAngles.x;
						eulerAngles2.z = eulerAngles.z;
					}
				}
				else
				{
					eulerAngles2.y = eulerAngles.y;
					eulerAngles2.z = eulerAngles.z;
				}
			}
			target.transform.eulerAngles = eulerAngles2;
		}
		args["rotation"] = eulerAngles;
		args["type"] = "rotate";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x000149F2 File Offset: 0x00012DF2
	public static void LookTo(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookTo(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00014A28 File Offset: 0x00012E28
	public static void LookTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("looktarget") && args["looktarget"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["looktarget"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		}
		args["type"] = "look";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00014B25 File Offset: 0x00012F25
	public static void MoveTo(GameObject target, Vector3 position, float time)
	{
		iTween.MoveTo(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00014B5C File Offset: 0x00012F5C
	public static void MoveTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["position"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "move";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00014C9B File Offset: 0x0001309B
	public static void MoveFrom(GameObject target, Vector3 position, float time)
	{
		iTween.MoveFrom(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00014CD0 File Offset: 0x000130D0
	public static void MoveFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		if (args.Contains("path"))
		{
			Vector3[] array2;
			if (args["path"].GetType() == typeof(Vector3[]))
			{
				Vector3[] array = (Vector3[])args["path"];
				array2 = new Vector3[array.Length];
				Array.Copy(array, array2, array.Length);
			}
			else
			{
				Transform[] array3 = (Transform[])args["path"];
				array2 = new Vector3[array3.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array2[i] = array3[i].position;
				}
			}
			if (array2[array2.Length - 1] != target.transform.position)
			{
				Vector3[] array4 = new Vector3[array2.Length + 1];
				Array.Copy(array2, array4, array2.Length);
				if (flag)
				{
					array4[array4.Length - 1] = target.transform.localPosition;
					target.transform.localPosition = array4[0];
				}
				else
				{
					array4[array4.Length - 1] = target.transform.position;
					target.transform.position = array4[0];
				}
				args["path"] = array4;
			}
			else
			{
				if (flag)
				{
					target.transform.localPosition = array2[0];
				}
				else
				{
					target.transform.position = array2[0];
				}
				args["path"] = array2;
			}
		}
		else
		{
			Vector3 vector2;
			Vector3 vector;
			if (flag)
			{
				vector = (vector2 = target.transform.localPosition);
			}
			else
			{
				vector = (vector2 = target.transform.position);
			}
			if (args.Contains("position"))
			{
				if (args["position"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["position"];
					vector = transform.position;
				}
				else if (args["position"].GetType() == typeof(Vector3))
				{
					vector = (Vector3)args["position"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					vector.x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					vector.y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					vector.z = (float)args["z"];
				}
			}
			if (flag)
			{
				target.transform.localPosition = vector;
			}
			else
			{
				target.transform.position = vector;
			}
			args["position"] = vector2;
		}
		args["type"] = "move";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0001503C File Offset: 0x0001343C
	public static void MoveAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.MoveAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00015071 File Offset: 0x00013471
	public static void MoveAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "move";
		args["method"] = "add";
		iTween.Launch(target, args);
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x000150A2 File Offset: 0x000134A2
	public static void MoveBy(GameObject target, Vector3 amount, float time)
	{
		iTween.MoveBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000150D7 File Offset: 0x000134D7
	public static void MoveBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "move";
		args["method"] = "by";
		iTween.Launch(target, args);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00015108 File Offset: 0x00013508
	public static void ScaleTo(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleTo(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00015140 File Offset: 0x00013540
	public static void ScaleTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("scale") && args["scale"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["scale"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "scale";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0001527F File Offset: 0x0001367F
	public static void ScaleFrom(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleFrom(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x000152B4 File Offset: 0x000136B4
	public static void ScaleFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3 localScale2;
		Vector3 localScale = localScale2 = target.transform.localScale;
		if (args.Contains("scale"))
		{
			if (args["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["scale"];
				localScale = transform.localScale;
			}
			else if (args["scale"].GetType() == typeof(Vector3))
			{
				localScale = (Vector3)args["scale"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				localScale.x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				localScale.y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				localScale.z = (float)args["z"];
			}
		}
		target.transform.localScale = localScale;
		args["scale"] = localScale2;
		args["type"] = "scale";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00015411 File Offset: 0x00013811
	public static void ScaleAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.ScaleAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00015446 File Offset: 0x00013846
	public static void ScaleAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "scale";
		args["method"] = "add";
		iTween.Launch(target, args);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00015477 File Offset: 0x00013877
	public static void ScaleBy(GameObject target, Vector3 amount, float time)
	{
		iTween.ScaleBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x000154AC File Offset: 0x000138AC
	public static void ScaleBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "scale";
		args["method"] = "by";
		iTween.Launch(target, args);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x000154DD File Offset: 0x000138DD
	public static void RotateTo(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateTo(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00015514 File Offset: 0x00013914
	public static void RotateTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("rotation") && args["rotation"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["rotation"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "rotate";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00015653 File Offset: 0x00013A53
	public static void RotateFrom(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateFrom(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00015688 File Offset: 0x00013A88
	public static void RotateFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		Vector3 vector2;
		Vector3 vector;
		if (flag)
		{
			vector = (vector2 = target.transform.localEulerAngles);
		}
		else
		{
			vector = (vector2 = target.transform.eulerAngles);
		}
		if (args.Contains("rotation"))
		{
			if (args["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["rotation"];
				vector = transform.eulerAngles;
			}
			else if (args["rotation"].GetType() == typeof(Vector3))
			{
				vector = (Vector3)args["rotation"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				vector.x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				vector.y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				vector.z = (float)args["z"];
			}
		}
		if (flag)
		{
			target.transform.localEulerAngles = vector;
		}
		else
		{
			target.transform.eulerAngles = vector;
		}
		args["rotation"] = vector2;
		args["type"] = "rotate";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00015841 File Offset: 0x00013C41
	public static void RotateAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.RotateAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00015876 File Offset: 0x00013C76
	public static void RotateAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "rotate";
		args["method"] = "add";
		iTween.Launch(target, args);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x000158A7 File Offset: 0x00013CA7
	public static void RotateBy(GameObject target, Vector3 amount, float time)
	{
		iTween.RotateBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x000158DC File Offset: 0x00013CDC
	public static void RotateBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "rotate";
		args["method"] = "by";
		iTween.Launch(target, args);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0001590D File Offset: 0x00013D0D
	public static void ShakePosition(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakePosition(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00015942 File Offset: 0x00013D42
	public static void ShakePosition(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "position";
		iTween.Launch(target, args);
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00015973 File Offset: 0x00013D73
	public static void ShakeScale(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakeScale(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x000159A8 File Offset: 0x00013DA8
	public static void ShakeScale(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "scale";
		iTween.Launch(target, args);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000159D9 File Offset: 0x00013DD9
	public static void ShakeRotation(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakeRotation(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00015A0E File Offset: 0x00013E0E
	public static void ShakeRotation(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "rotation";
		iTween.Launch(target, args);
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00015A3F File Offset: 0x00013E3F
	public static void PunchPosition(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchPosition(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00015A74 File Offset: 0x00013E74
	public static void PunchPosition(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "position";
		args["easetype"] = iTween.EaseType.punch;
		iTween.Launch(target, args);
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00015AC2 File Offset: 0x00013EC2
	public static void PunchRotation(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchRotation(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00015AF8 File Offset: 0x00013EF8
	public static void PunchRotation(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "rotation";
		args["easetype"] = iTween.EaseType.punch;
		iTween.Launch(target, args);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00015B46 File Offset: 0x00013F46
	public static void PunchScale(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchScale(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00015B7C File Offset: 0x00013F7C
	public static void PunchScale(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "scale";
		args["easetype"] = iTween.EaseType.punch;
		iTween.Launch(target, args);
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00015BCC File Offset: 0x00013FCC
	private void GenerateTargets()
	{
		string text = this.type;
		switch (text)
		{
		case "value":
		{
			string text2 = this.method;
			if (text2 != null)
			{
				if (!(text2 == "float"))
				{
					if (!(text2 == "vector2"))
					{
						if (!(text2 == "vector3"))
						{
							if (!(text2 == "color"))
							{
								if (text2 == "rect")
								{
									this.GenerateRectTargets();
									this.apply = new iTween.ApplyTween(this.ApplyRectTargets);
								}
							}
							else
							{
								this.GenerateColorTargets();
								this.apply = new iTween.ApplyTween(this.ApplyColorTargets);
							}
						}
						else
						{
							this.GenerateVector3Targets();
							this.apply = new iTween.ApplyTween(this.ApplyVector3Targets);
						}
					}
					else
					{
						this.GenerateVector2Targets();
						this.apply = new iTween.ApplyTween(this.ApplyVector2Targets);
					}
				}
				else
				{
					this.GenerateFloatTargets();
					this.apply = new iTween.ApplyTween(this.ApplyFloatTargets);
				}
			}
			break;
		}
		case "color":
		{
			string text3 = this.method;
			if (text3 != null)
			{
				if (text3 == "to")
				{
					this.GenerateColorToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyColorToTargets);
				}
			}
			break;
		}
		case "audio":
		{
			string text4 = this.method;
			if (text4 != null)
			{
				if (text4 == "to")
				{
					this.GenerateAudioToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyAudioToTargets);
				}
			}
			break;
		}
		case "move":
		{
			string text5 = this.method;
			if (text5 != null)
			{
				if (!(text5 == "to"))
				{
					if (text5 == "by" || text5 == "add")
					{
						this.GenerateMoveByTargets();
						this.apply = new iTween.ApplyTween(this.ApplyMoveByTargets);
					}
				}
				else if (this.tweenArguments.Contains("path"))
				{
					this.GenerateMoveToPathTargets();
					this.apply = new iTween.ApplyTween(this.ApplyMoveToPathTargets);
				}
				else
				{
					this.GenerateMoveToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyMoveToTargets);
				}
			}
			break;
		}
		case "scale":
		{
			string text6 = this.method;
			if (text6 != null)
			{
				if (!(text6 == "to"))
				{
					if (!(text6 == "by"))
					{
						if (text6 == "add")
						{
							this.GenerateScaleAddTargets();
							this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
						}
					}
					else
					{
						this.GenerateScaleByTargets();
						this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
					}
				}
				else
				{
					this.GenerateScaleToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
				}
			}
			break;
		}
		case "rotate":
		{
			string text7 = this.method;
			if (text7 != null)
			{
				if (!(text7 == "to"))
				{
					if (!(text7 == "add"))
					{
						if (text7 == "by")
						{
							this.GenerateRotateByTargets();
							this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
						}
					}
					else
					{
						this.GenerateRotateAddTargets();
						this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
					}
				}
				else
				{
					this.GenerateRotateToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyRotateToTargets);
				}
			}
			break;
		}
		case "shake":
		{
			string text8 = this.method;
			if (text8 != null)
			{
				if (!(text8 == "position"))
				{
					if (!(text8 == "scale"))
					{
						if (text8 == "rotation")
						{
							this.GenerateShakeRotationTargets();
							this.apply = new iTween.ApplyTween(this.ApplyShakeRotationTargets);
						}
					}
					else
					{
						this.GenerateShakeScaleTargets();
						this.apply = new iTween.ApplyTween(this.ApplyShakeScaleTargets);
					}
				}
				else
				{
					this.GenerateShakePositionTargets();
					this.apply = new iTween.ApplyTween(this.ApplyShakePositionTargets);
				}
			}
			break;
		}
		case "punch":
		{
			string text9 = this.method;
			if (text9 != null)
			{
				if (!(text9 == "position"))
				{
					if (!(text9 == "rotation"))
					{
						if (text9 == "scale")
						{
							this.GeneratePunchScaleTargets();
							this.apply = new iTween.ApplyTween(this.ApplyPunchScaleTargets);
						}
					}
					else
					{
						this.GeneratePunchRotationTargets();
						this.apply = new iTween.ApplyTween(this.ApplyPunchRotationTargets);
					}
				}
				else
				{
					this.GeneratePunchPositionTargets();
					this.apply = new iTween.ApplyTween(this.ApplyPunchPositionTargets);
				}
			}
			break;
		}
		case "look":
		{
			string text10 = this.method;
			if (text10 != null)
			{
				if (text10 == "to")
				{
					this.GenerateLookToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyLookToTargets);
				}
			}
			break;
		}
		case "stab":
			this.GenerateStabTargets();
			this.apply = new iTween.ApplyTween(this.ApplyStabTargets);
			break;
		}
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x000161EC File Offset: 0x000145EC
	private void GenerateRectTargets()
	{
		this.rects = new Rect[3];
		this.rects[0] = (Rect)this.tweenArguments["from"];
		this.rects[1] = (Rect)this.tweenArguments["to"];
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00016254 File Offset: 0x00014654
	private void GenerateColorTargets()
	{
		this.colors = new Color[1, 3];
		this.colors[0, 0] = (Color)this.tweenArguments["from"];
		this.colors[0, 1] = (Color)this.tweenArguments["to"];
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x000162BC File Offset: 0x000146BC
	private void GenerateVector3Targets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (Vector3)this.tweenArguments["from"];
		this.vector3s[1] = (Vector3)this.tweenArguments["to"];
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00016380 File Offset: 0x00014780
	private void GenerateVector2Targets()
	{
		this.vector2s = new Vector2[3];
		this.vector2s[0] = (Vector2)this.tweenArguments["from"];
		this.vector2s[1] = (Vector2)this.tweenArguments["to"];
		if (this.tweenArguments.Contains("speed"))
		{
			Vector3 a = new Vector3(this.vector2s[0].x, this.vector2s[0].y, 0f);
			Vector3 b = new Vector3(this.vector2s[1].x, this.vector2s[1].y, 0f);
			float num = Math.Abs(Vector3.Distance(a, b));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00016480 File Offset: 0x00014880
	private void GenerateFloatTargets()
	{
		this.floats = new float[3];
		this.floats[0] = (float)this.tweenArguments["from"];
		this.floats[1] = (float)this.tweenArguments["to"];
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(this.floats[0] - this.floats[1]);
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x0001651C File Offset: 0x0001491C
	private void GenerateColorToTargets()
	{
		if (base.GetComponent<GUITexture>())
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<GUITexture>().color);
		}
		else if (base.GetComponent<GUIText>())
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<GUIText>().material.color);
		}
		else if (base.GetComponent<Renderer>())
		{
			this.colors = new Color[base.GetComponent<Renderer>().materials.Length, 3];
			for (int i = 0; i < base.GetComponent<Renderer>().materials.Length; i++)
			{
				this.colors[i, 0] = base.GetComponent<Renderer>().materials[i].GetColor(this.namedcolorvalue.ToString());
				this.colors[i, 1] = base.GetComponent<Renderer>().materials[i].GetColor(this.namedcolorvalue.ToString());
			}
		}
		else if (base.GetComponent<Light>())
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<Light>().color);
		}
		else
		{
			this.colors = new Color[1, 3];
		}
		if (this.tweenArguments.Contains("color"))
		{
			for (int j = 0; j < this.colors.GetLength(0); j++)
			{
				this.colors[j, 1] = (Color)this.tweenArguments["color"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("r"))
			{
				for (int k = 0; k < this.colors.GetLength(0); k++)
				{
					this.colors[k, 1].r = (float)this.tweenArguments["r"];
				}
			}
			if (this.tweenArguments.Contains("g"))
			{
				for (int l = 0; l < this.colors.GetLength(0); l++)
				{
					this.colors[l, 1].g = (float)this.tweenArguments["g"];
				}
			}
			if (this.tweenArguments.Contains("b"))
			{
				for (int m = 0; m < this.colors.GetLength(0); m++)
				{
					this.colors[m, 1].b = (float)this.tweenArguments["b"];
				}
			}
			if (this.tweenArguments.Contains("a"))
			{
				for (int n = 0; n < this.colors.GetLength(0); n++)
				{
					this.colors[n, 1].a = (float)this.tweenArguments["a"];
				}
			}
		}
		if (this.tweenArguments.Contains("amount"))
		{
			for (int num = 0; num < this.colors.GetLength(0); num++)
			{
				this.colors[num, 1].a = (float)this.tweenArguments["amount"];
			}
		}
		else if (this.tweenArguments.Contains("alpha"))
		{
			for (int num2 = 0; num2 < this.colors.GetLength(0); num2++)
			{
				this.colors[num2, 1].a = (float)this.tweenArguments["alpha"];
			}
		}
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00016980 File Offset: 0x00014D80
	private void GenerateAudioToTargets()
	{
		this.vector2s = new Vector2[3];
		if (this.tweenArguments.Contains("audiosource"))
		{
			this.audioSource = (AudioSource)this.tweenArguments["audiosource"];
		}
		else if (base.GetComponent<AudioSource>())
		{
			this.audioSource = base.GetComponent<AudioSource>();
		}
		else
		{
			Debug.LogError("iTween Error: AudioTo requires an AudioSource.");
			this.Dispose();
		}
		this.vector2s[0] = (this.vector2s[1] = new Vector2(this.audioSource.volume, this.audioSource.pitch));
		if (this.tweenArguments.Contains("volume"))
		{
			this.vector2s[1].x = (float)this.tweenArguments["volume"];
		}
		if (this.tweenArguments.Contains("pitch"))
		{
			this.vector2s[1].y = (float)this.tweenArguments["pitch"];
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00016AB8 File Offset: 0x00014EB8
	private void GenerateStabTargets()
	{
		if (this.tweenArguments.Contains("audiosource"))
		{
			this.audioSource = (AudioSource)this.tweenArguments["audiosource"];
		}
		else if (base.GetComponent<AudioSource>())
		{
			this.audioSource = base.GetComponent<AudioSource>();
		}
		else
		{
			base.gameObject.AddComponent<AudioSource>();
			this.audioSource = base.GetComponent<AudioSource>();
			this.audioSource.playOnAwake = false;
		}
		this.audioSource.clip = (AudioClip)this.tweenArguments["audioclip"];
		if (this.tweenArguments.Contains("pitch"))
		{
			this.audioSource.pitch = (float)this.tweenArguments["pitch"];
		}
		if (this.tweenArguments.Contains("volume"))
		{
			this.audioSource.volume = (float)this.tweenArguments["volume"];
		}
		this.time = this.audioSource.clip.length / this.audioSource.pitch;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00016BEC File Offset: 0x00014FEC
	private void GenerateLookToTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.eulerAngles;
		if (this.tweenArguments.Contains("looktarget"))
		{
			if (this.tweenArguments["looktarget"].GetType() == typeof(Transform))
			{
				Transform transform = this.thisTransform;
				Transform target = (Transform)this.tweenArguments["looktarget"];
				Vector3? vector = (Vector3?)this.tweenArguments["up"];
				transform.LookAt(target, (vector == null) ? iTween.Defaults.up : vector.Value);
			}
			else if (this.tweenArguments["looktarget"].GetType() == typeof(Vector3))
			{
				Transform transform2 = this.thisTransform;
				Vector3 worldPosition = (Vector3)this.tweenArguments["looktarget"];
				Vector3? vector2 = (Vector3?)this.tweenArguments["up"];
				transform2.LookAt(worldPosition, (vector2 == null) ? iTween.Defaults.up : vector2.Value);
			}
		}
		else
		{
			Debug.LogError("iTween Error: LookTo needs a 'looktarget' property!");
			this.Dispose();
		}
		this.vector3s[1] = this.thisTransform.eulerAngles;
		this.thisTransform.eulerAngles = this.vector3s[0];
		if (this.tweenArguments.Contains("axis"))
		{
			string text = (string)this.tweenArguments["axis"];
			if (text != null)
			{
				if (!(text == "x"))
				{
					if (!(text == "y"))
					{
						if (text == "z")
						{
							this.vector3s[1].x = this.vector3s[0].x;
							this.vector3s[1].y = this.vector3s[0].y;
						}
					}
					else
					{
						this.vector3s[1].x = this.vector3s[0].x;
						this.vector3s[1].z = this.vector3s[0].z;
					}
				}
				else
				{
					this.vector3s[1].y = this.vector3s[0].y;
					this.vector3s[1].z = this.vector3s[0].z;
				}
			}
		}
		this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00016FB4 File Offset: 0x000153B4
	private void GenerateMoveToPathTargets()
	{
		Vector3[] array2;
		if (this.tweenArguments["path"].GetType() == typeof(Vector3[]))
		{
			Vector3[] array = (Vector3[])this.tweenArguments["path"];
			if (array.Length == 1)
			{
				Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
				this.Dispose();
			}
			array2 = new Vector3[array.Length];
			Array.Copy(array, array2, array.Length);
		}
		else
		{
			Transform[] array3 = (Transform[])this.tweenArguments["path"];
			if (array3.Length == 1)
			{
				Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
				this.Dispose();
			}
			array2 = new Vector3[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array2[i] = array3[i].position;
			}
		}
		bool flag;
		int num;
		if (this.thisTransform.position != array2[0])
		{
			if (!this.tweenArguments.Contains("movetopath") || (bool)this.tweenArguments["movetopath"])
			{
				flag = true;
				num = 3;
			}
			else
			{
				flag = false;
				num = 2;
			}
		}
		else
		{
			flag = false;
			num = 2;
		}
		this.vector3s = new Vector3[array2.Length + num];
		if (flag)
		{
			this.vector3s[1] = this.thisTransform.position;
			num = 2;
		}
		else
		{
			num = 1;
		}
		Array.Copy(array2, 0, this.vector3s, num, array2.Length);
		this.vector3s[0] = this.vector3s[1] + (this.vector3s[1] - this.vector3s[2]);
		this.vector3s[this.vector3s.Length - 1] = this.vector3s[this.vector3s.Length - 2] + (this.vector3s[this.vector3s.Length - 2] - this.vector3s[this.vector3s.Length - 3]);
		if (this.vector3s[1] == this.vector3s[this.vector3s.Length - 2])
		{
			Vector3[] array4 = new Vector3[this.vector3s.Length];
			Array.Copy(this.vector3s, array4, this.vector3s.Length);
			array4[0] = array4[array4.Length - 3];
			array4[array4.Length - 1] = array4[2];
			this.vector3s = new Vector3[array4.Length];
			Array.Copy(array4, this.vector3s, array4.Length);
		}
		this.path = new iTween.CRSpline(this.vector3s);
		if (this.tweenArguments.Contains("speed"))
		{
			float num2 = iTween.PathLength(this.vector3s);
			this.time = num2 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00017314 File Offset: 0x00015714
	private void GenerateMoveToTargets()
	{
		this.vector3s = new Vector3[3];
		if (this.isLocal)
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localPosition);
		}
		else
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.position);
		}
		if (this.tweenArguments.Contains("position"))
		{
			if (this.tweenArguments["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments["position"];
				this.vector3s[1] = transform.position;
			}
			else if (this.tweenArguments["position"].GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["position"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
		{
			this.tweenArguments["looktarget"] = this.vector3s[1];
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x000175BC File Offset: 0x000159BC
	private void GenerateMoveByTargets()
	{
		this.vector3s = new Vector3[6];
		this.vector3s[4] = this.thisTransform.eulerAngles;
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.position));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = this.vector3s[0] + (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = this.vector3s[0].x + (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = this.vector3s[0].y + (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = this.vector3s[0].z + (float)this.tweenArguments["z"];
			}
		}
		this.thisTransform.Translate(this.vector3s[1], this.space);
		this.vector3s[5] = this.thisTransform.position;
		this.thisTransform.position = this.vector3s[0];
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
		{
			this.tweenArguments["looktarget"] = this.vector3s[1];
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00017880 File Offset: 0x00015C80
	private void GenerateScaleToTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localScale);
		if (this.tweenArguments.Contains("scale"))
		{
			if (this.tweenArguments["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments["scale"];
				this.vector3s[1] = transform.localScale;
			}
			else if (this.tweenArguments["scale"].GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["scale"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00017A94 File Offset: 0x00015E94
	private void GenerateScaleByTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localScale);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = Vector3.Scale(this.vector3s[1], (Vector3)this.tweenArguments["amount"]);
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x * (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y * (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z * (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00017C58 File Offset: 0x00016058
	private void GenerateScaleAddTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localScale);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x + (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00017E14 File Offset: 0x00016214
	private void GenerateRotateToTargets()
	{
		this.vector3s = new Vector3[3];
		if (this.isLocal)
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localEulerAngles);
		}
		else
		{
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.eulerAngles);
		}
		if (this.tweenArguments.Contains("rotation"))
		{
			if (this.tweenArguments["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments["rotation"];
				this.vector3s[1] = transform.eulerAngles;
			}
			else if (this.tweenArguments["rotation"].GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["rotation"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
		this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00018104 File Offset: 0x00016504
	private void GenerateRotateAddTargets()
	{
		this.vector3s = new Vector3[5];
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.eulerAngles));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x + (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x000182D4 File Offset: 0x000166D4
	private void GenerateRotateByTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.eulerAngles));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += Vector3.Scale((Vector3)this.tweenArguments["amount"], new Vector3(360f, 360f, 360f));
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x + 360f * (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y + 360f * (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z + 360f * (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x000184CC File Offset: 0x000168CC
	private void GenerateShakePositionTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[3] = this.thisTransform.eulerAngles;
		this.vector3s[0] = this.thisTransform.position;
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00018610 File Offset: 0x00016A10
	private void GenerateShakeScaleTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.localScale;
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00018738 File Offset: 0x00016B38
	private void GenerateShakeRotationTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.eulerAngles;
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00018860 File Offset: 0x00016C60
	private void GeneratePunchPositionTargets()
	{
		this.vector3s = new Vector3[5];
		this.vector3s[4] = this.thisTransform.eulerAngles;
		this.vector3s[0] = this.thisTransform.position;
		this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x000189CC File Offset: 0x00016DCC
	private void GeneratePunchRotationTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[0] = this.thisTransform.eulerAngles;
		this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00018B1C File Offset: 0x00016F1C
	private void GeneratePunchScaleTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.thisTransform.localScale;
		this.vector3s[1] = Vector3.zero;
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00018C58 File Offset: 0x00017058
	private void ApplyRectTargets()
	{
		this.rects[2].x = this.ease(this.rects[0].x, this.rects[1].x, this.percentage);
		this.rects[2].y = this.ease(this.rects[0].y, this.rects[1].y, this.percentage);
		this.rects[2].width = this.ease(this.rects[0].width, this.rects[1].width, this.percentage);
		this.rects[2].height = this.ease(this.rects[0].height, this.rects[1].height, this.percentage);
		this.tweenArguments["onupdateparams"] = this.rects[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.rects[1];
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00018DD4 File Offset: 0x000171D4
	private void ApplyColorTargets()
	{
		this.colors[0, 2].r = this.ease(this.colors[0, 0].r, this.colors[0, 1].r, this.percentage);
		this.colors[0, 2].g = this.ease(this.colors[0, 0].g, this.colors[0, 1].g, this.percentage);
		this.colors[0, 2].b = this.ease(this.colors[0, 0].b, this.colors[0, 1].b, this.percentage);
		this.colors[0, 2].a = this.ease(this.colors[0, 0].a, this.colors[0, 1].a, this.percentage);
		this.tweenArguments["onupdateparams"] = this.colors[0, 2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.colors[0, 1];
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00018F54 File Offset: 0x00017354
	private void ApplyVector3Targets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.tweenArguments["onupdateparams"] = this.vector3s[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.vector3s[1];
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0001908C File Offset: 0x0001748C
	private void ApplyVector2Targets()
	{
		this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
		this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
		this.tweenArguments["onupdateparams"] = this.vector2s[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.vector2s[1];
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00019180 File Offset: 0x00017580
	private void ApplyFloatTargets()
	{
		this.floats[2] = this.ease(this.floats[0], this.floats[1], this.percentage);
		this.tweenArguments["onupdateparams"] = this.floats[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.floats[1];
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00019200 File Offset: 0x00017600
	private void ApplyColorToTargets()
	{
		for (int i = 0; i < this.colors.GetLength(0); i++)
		{
			this.colors[i, 2].r = this.ease(this.colors[i, 0].r, this.colors[i, 1].r, this.percentage);
			this.colors[i, 2].g = this.ease(this.colors[i, 0].g, this.colors[i, 1].g, this.percentage);
			this.colors[i, 2].b = this.ease(this.colors[i, 0].b, this.colors[i, 1].b, this.percentage);
			this.colors[i, 2].a = this.ease(this.colors[i, 0].a, this.colors[i, 1].a, this.percentage);
		}
		if (base.GetComponent<GUITexture>())
		{
			base.GetComponent<GUITexture>().color = this.colors[0, 2];
		}
		else if (base.GetComponent<GUIText>())
		{
			base.GetComponent<GUIText>().material.color = this.colors[0, 2];
		}
		else if (base.GetComponent<Renderer>())
		{
			for (int j = 0; j < this.colors.GetLength(0); j++)
			{
				base.GetComponent<Renderer>().materials[j].SetColor(this.namedcolorvalue.ToString(), this.colors[j, 2]);
			}
		}
		else if (base.GetComponent<Light>())
		{
			base.GetComponent<Light>().color = this.colors[0, 2];
		}
		if (this.percentage == 1f)
		{
			if (base.GetComponent<GUITexture>())
			{
				base.GetComponent<GUITexture>().color = this.colors[0, 1];
			}
			else if (base.GetComponent<GUIText>())
			{
				base.GetComponent<GUIText>().material.color = this.colors[0, 1];
			}
			else if (base.GetComponent<Renderer>())
			{
				for (int k = 0; k < this.colors.GetLength(0); k++)
				{
					base.GetComponent<Renderer>().materials[k].SetColor(this.namedcolorvalue.ToString(), this.colors[k, 1]);
				}
			}
			else if (base.GetComponent<Light>())
			{
				base.GetComponent<Light>().color = this.colors[0, 1];
			}
		}
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00019528 File Offset: 0x00017928
	private void ApplyAudioToTargets()
	{
		this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
		this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
		this.audioSource.volume = this.vector2s[2].x;
		this.audioSource.pitch = this.vector2s[2].y;
		if (this.percentage == 1f)
		{
			this.audioSource.volume = this.vector2s[1].x;
			this.audioSource.pitch = this.vector2s[1].y;
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0001963D File Offset: 0x00017A3D
	private void ApplyStabTargets()
	{
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00019640 File Offset: 0x00017A40
	private void ApplyMoveToPathTargets()
	{
		this.preUpdate = this.thisTransform.position;
		float value = this.ease(0f, 1f, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.localPosition = this.path.Interp(Mathf.Clamp(value, 0f, 1f));
		}
		else
		{
			this.thisTransform.position = this.path.Interp(Mathf.Clamp(value, 0f, 1f));
		}
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
		{
			float num;
			if (this.tweenArguments.Contains("lookahead"))
			{
				num = (float)this.tweenArguments["lookahead"];
			}
			else
			{
				num = iTween.Defaults.lookAhead;
			}
			float value2 = this.ease(0f, 1f, Mathf.Min(1f, this.percentage + num));
			this.tweenArguments["looktarget"] = this.path.Interp(Mathf.Clamp(value2, 0f, 1f));
		}
		this.postUpdate = this.thisTransform.position;
		if (this.physics)
		{
			this.thisTransform.position = this.preUpdate;
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x000197D4 File Offset: 0x00017BD4
	private void ApplyMoveToTargets()
	{
		this.preUpdate = this.thisTransform.position;
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.localPosition = this.vector3s[2];
		}
		else
		{
			this.thisTransform.position = this.vector3s[2];
		}
		if (this.percentage == 1f)
		{
			if (this.isLocal)
			{
				this.thisTransform.localPosition = this.vector3s[1];
			}
			else
			{
				this.thisTransform.position = this.vector3s[1];
			}
		}
		this.postUpdate = this.thisTransform.position;
		if (this.physics)
		{
			this.thisTransform.position = this.preUpdate;
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0001999C File Offset: 0x00017D9C
	private void ApplyMoveByTargets()
	{
		this.preUpdate = this.thisTransform.position;
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.thisTransform.eulerAngles;
			this.thisTransform.eulerAngles = this.vector3s[4];
		}
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.thisTransform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.thisTransform.eulerAngles = eulerAngles;
		}
		this.postUpdate = this.thisTransform.position;
		if (this.physics)
		{
			this.thisTransform.position = this.preUpdate;
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00019B84 File Offset: 0x00017F84
	private void ApplyScaleToTargets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.thisTransform.localScale = this.vector3s[2];
		if (this.percentage == 1f)
		{
			this.thisTransform.localScale = this.vector3s[1];
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00019CA8 File Offset: 0x000180A8
	private void ApplyLookToTargets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.localRotation = Quaternion.Euler(this.vector3s[2]);
		}
		else
		{
			this.thisTransform.rotation = Quaternion.Euler(this.vector3s[2]);
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00019DD4 File Offset: 0x000181D4
	private void ApplyRotateToTargets()
	{
		this.preUpdate = this.thisTransform.eulerAngles;
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.thisTransform.localRotation = Quaternion.Euler(this.vector3s[2]);
		}
		else
		{
			this.thisTransform.rotation = Quaternion.Euler(this.vector3s[2]);
		}
		if (this.percentage == 1f)
		{
			if (this.isLocal)
			{
				this.thisTransform.localRotation = Quaternion.Euler(this.vector3s[1]);
			}
			else
			{
				this.thisTransform.rotation = Quaternion.Euler(this.vector3s[1]);
			}
		}
		this.postUpdate = this.thisTransform.eulerAngles;
		if (this.physics)
		{
			this.thisTransform.eulerAngles = this.preUpdate;
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00019FB8 File Offset: 0x000183B8
	private void ApplyRotateAddTargets()
	{
		this.preUpdate = this.thisTransform.eulerAngles;
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.thisTransform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		this.postUpdate = this.thisTransform.eulerAngles;
		if (this.physics)
		{
			this.thisTransform.eulerAngles = this.preUpdate;
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0001A140 File Offset: 0x00018540
	private void ApplyShakePositionTargets()
	{
		if (this.isLocal)
		{
			this.preUpdate = this.thisTransform.localPosition;
		}
		else
		{
			this.preUpdate = this.thisTransform.position;
		}
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.thisTransform.eulerAngles;
			this.thisTransform.eulerAngles = this.vector3s[3];
		}
		if (this.percentage == 0f)
		{
			this.thisTransform.Translate(this.vector3s[1], this.space);
		}
		if (this.isLocal)
		{
			this.thisTransform.localPosition = this.vector3s[0];
		}
		else
		{
			this.thisTransform.position = this.vector3s[0];
		}
		float num = 1f - this.percentage;
		this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		if (this.isLocal)
		{
			this.thisTransform.localPosition += this.vector3s[2];
		}
		else
		{
			this.thisTransform.position += this.vector3s[2];
		}
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.thisTransform.eulerAngles = eulerAngles;
		}
		this.postUpdate = this.thisTransform.position;
		if (this.physics)
		{
			this.thisTransform.position = this.preUpdate;
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0001A3C0 File Offset: 0x000187C0
	private void ApplyShakeScaleTargets()
	{
		if (this.percentage == 0f)
		{
			this.thisTransform.localScale = this.vector3s[1];
		}
		this.thisTransform.localScale = this.vector3s[0];
		float num = 1f - this.percentage;
		this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		this.thisTransform.localScale += this.vector3s[2];
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0001A500 File Offset: 0x00018900
	private void ApplyShakeRotationTargets()
	{
		this.preUpdate = this.thisTransform.eulerAngles;
		if (this.percentage == 0f)
		{
			this.thisTransform.Rotate(this.vector3s[1], this.space);
		}
		this.thisTransform.eulerAngles = this.vector3s[0];
		float num = 1f - this.percentage;
		this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		this.thisTransform.Rotate(this.vector3s[2], this.space);
		this.postUpdate = this.thisTransform.eulerAngles;
		if (this.physics)
		{
			this.thisTransform.eulerAngles = this.preUpdate;
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0001A698 File Offset: 0x00018A98
	private void ApplyPunchPositionTargets()
	{
		this.preUpdate = this.thisTransform.position;
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.thisTransform.eulerAngles;
			this.thisTransform.eulerAngles = this.vector3s[4];
		}
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.thisTransform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.thisTransform.eulerAngles = eulerAngles;
		}
		this.postUpdate = this.thisTransform.position;
		if (this.physics)
		{
			this.thisTransform.position = this.preUpdate;
			base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0001A98C File Offset: 0x00018D8C
	private void ApplyPunchRotationTargets()
	{
		this.preUpdate = this.thisTransform.eulerAngles;
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.thisTransform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		this.postUpdate = this.thisTransform.eulerAngles;
		if (this.physics)
		{
			this.thisTransform.eulerAngles = this.preUpdate;
			base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0001AC20 File Offset: 0x00019020
	private void ApplyPunchScaleTargets()
	{
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.thisTransform.localScale = this.vector3s[0] + this.vector3s[2];
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0001AE38 File Offset: 0x00019238
	private IEnumerator TweenDelay()
	{
		this.delayStarted = Time.time;
		yield return new WaitForSeconds(this.delay);
		if (this.wasPaused)
		{
			this.wasPaused = false;
			this.TweenStart();
		}
		yield break;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0001AE54 File Offset: 0x00019254
	private void TweenStart()
	{
		this.CallBack("onstart");
		if (!this.loop)
		{
			this.ConflictCheck();
			this.GenerateTargets();
		}
		if (this.type == "stab")
		{
			this.audioSource.PlayOneShot(this.audioSource.clip);
		}
		if (this.type == "move" || this.type == "scale" || this.type == "rotate" || this.type == "punch" || this.type == "shake" || this.type == "curve" || this.type == "look")
		{
			this.EnableKinematic();
		}
		this.isRunning = true;
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0001AF50 File Offset: 0x00019350
	private IEnumerator TweenRestart()
	{
		if (this.delay > 0f)
		{
			this.delayStarted = Time.time;
			yield return new WaitForSeconds(this.delay);
		}
		this.loop = true;
		this.TweenStart();
		yield break;
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0001AF6B File Offset: 0x0001936B
	private void TweenUpdate()
	{
		this.apply();
		this.CallBack("onupdate");
		this.UpdatePercentage();
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0001AF8C File Offset: 0x0001938C
	private void TweenComplete()
	{
		this.isRunning = false;
		if (this.percentage > 0.5f)
		{
			this.percentage = 1f;
		}
		else
		{
			this.percentage = 0f;
		}
		this.apply();
		if (this.type == "value")
		{
			this.CallBack("onupdate");
		}
		if (this.loopType == iTween.LoopType.none)
		{
			this.Dispose();
		}
		else
		{
			this.TweenLoop();
		}
		this.CallBack("oncomplete");
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0001B020 File Offset: 0x00019420
	private void TweenLoop()
	{
		this.DisableKinematic();
		iTween.LoopType loopType = this.loopType;
		if (loopType != iTween.LoopType.loop)
		{
			if (loopType == iTween.LoopType.pingPong)
			{
				this.reverse = !this.reverse;
				this.runningTime = 0f;
				base.StartCoroutine("TweenRestart");
			}
		}
		else
		{
			this.percentage = 0f;
			this.runningTime = 0f;
			this.apply();
			base.StartCoroutine("TweenRestart");
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0001B0AC File Offset: 0x000194AC
	public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
	{
		Rect result = new Rect(iTween.FloatUpdate(currentValue.x, targetValue.x, speed), iTween.FloatUpdate(currentValue.y, targetValue.y, speed), iTween.FloatUpdate(currentValue.width, targetValue.width, speed), iTween.FloatUpdate(currentValue.height, targetValue.height, speed));
		return result;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0001B114 File Offset: 0x00019514
	public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
	{
		Vector3 a = targetValue - currentValue;
		currentValue += a * speed * Time.deltaTime;
		return currentValue;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0001B144 File Offset: 0x00019544
	public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
	{
		Vector2 a = targetValue - currentValue;
		currentValue += a * speed * Time.deltaTime;
		return currentValue;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0001B174 File Offset: 0x00019574
	public static float FloatUpdate(float currentValue, float targetValue, float speed)
	{
		float num = targetValue - currentValue;
		currentValue += num * speed * Time.deltaTime;
		return currentValue;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0001B193 File Offset: 0x00019593
	public static void FadeUpdate(GameObject target, Hashtable args)
	{
		args["a"] = args["alpha"];
		iTween.ColorUpdate(target, args);
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0001B1B2 File Offset: 0x000195B2
	public static void FadeUpdate(GameObject target, float alpha, float time)
	{
		iTween.FadeUpdate(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0001B1E8 File Offset: 0x000195E8
	public static void ColorUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Color[] array = new Color[4];
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.ColorUpdate(transform.gameObject, args);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		if (target.GetComponent<GUITexture>())
		{
			array[0] = (array[1] = target.GetComponent<GUITexture>().color);
		}
		else if (target.GetComponent<GUIText>())
		{
			array[0] = (array[1] = target.GetComponent<GUIText>().material.color);
		}
		else if (target.GetComponent<Renderer>())
		{
			array[0] = (array[1] = target.GetComponent<Renderer>().material.color);
		}
		else if (target.GetComponent<Light>())
		{
			array[0] = (array[1] = target.GetComponent<Light>().color);
		}
		if (args.Contains("color"))
		{
			array[1] = (Color)args["color"];
		}
		else
		{
			if (args.Contains("r"))
			{
				array[1].r = (float)args["r"];
			}
			if (args.Contains("g"))
			{
				array[1].g = (float)args["g"];
			}
			if (args.Contains("b"))
			{
				array[1].b = (float)args["b"];
			}
			if (args.Contains("a"))
			{
				array[1].a = (float)args["a"];
			}
		}
		array[3].r = Mathf.SmoothDamp(array[0].r, array[1].r, ref array[2].r, num);
		array[3].g = Mathf.SmoothDamp(array[0].g, array[1].g, ref array[2].g, num);
		array[3].b = Mathf.SmoothDamp(array[0].b, array[1].b, ref array[2].b, num);
		array[3].a = Mathf.SmoothDamp(array[0].a, array[1].a, ref array[2].a, num);
		if (target.GetComponent<GUITexture>())
		{
			target.GetComponent<GUITexture>().color = array[3];
		}
		else if (target.GetComponent<GUIText>())
		{
			target.GetComponent<GUIText>().material.color = array[3];
		}
		else if (target.GetComponent<Renderer>())
		{
			target.GetComponent<Renderer>().material.color = array[3];
		}
		else if (target.GetComponent<Light>())
		{
			target.GetComponent<Light>().color = array[3];
		}
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0001B624 File Offset: 0x00019A24
	public static void ColorUpdate(GameObject target, Color color, float time)
	{
		iTween.ColorUpdate(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0001B65C File Offset: 0x00019A5C
	public static void AudioUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector2[] array = new Vector2[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		AudioSource audioSource;
		if (args.Contains("audiosource"))
		{
			audioSource = (AudioSource)args["audiosource"];
		}
		else
		{
			if (!target.GetComponent<AudioSource>())
			{
				Debug.LogError("iTween Error: AudioUpdate requires an AudioSource.");
				return;
			}
			audioSource = target.GetComponent<AudioSource>();
		}
		array[0] = (array[1] = new Vector2(audioSource.volume, audioSource.pitch));
		if (args.Contains("volume"))
		{
			array[1].x = (float)args["volume"];
		}
		if (args.Contains("pitch"))
		{
			array[1].y = (float)args["pitch"];
		}
		array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
		audioSource.volume = array[3].x;
		audioSource.pitch = array[3].y;
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0001B80C File Offset: 0x00019C0C
	public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioUpdate(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0001B860 File Offset: 0x00019C60
	public static void RotateUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 eulerAngles = target.transform.eulerAngles;
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		if (flag)
		{
			array[0] = target.transform.localEulerAngles;
		}
		else
		{
			array[0] = target.transform.eulerAngles;
		}
		if (args.Contains("rotation"))
		{
			if (args["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["rotation"];
				array[1] = transform.eulerAngles;
			}
			else if (args["rotation"].GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args["rotation"];
			}
		}
		array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
		if (flag)
		{
			target.transform.localEulerAngles = array[3];
		}
		else
		{
			target.transform.eulerAngles = array[3];
		}
		if (target.GetComponent<Rigidbody>() != null)
		{
			Vector3 eulerAngles2 = target.transform.eulerAngles;
			target.transform.eulerAngles = eulerAngles;
			target.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(eulerAngles2));
		}
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0001BACB File Offset: 0x00019ECB
	public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateUpdate(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0001BB00 File Offset: 0x00019F00
	public static void ScaleUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		array[0] = (array[1] = target.transform.localScale);
		if (args.Contains("scale"))
		{
			if (args["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["scale"];
				array[1] = transform.localScale;
			}
			else if (args["scale"].GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args["scale"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				array[1].x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				array[1].y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				array[1].z = (float)args["z"];
			}
		}
		array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
		target.transform.localScale = array[3];
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0001BD49 File Offset: 0x0001A149
	public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleUpdate(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x0001BD80 File Offset: 0x0001A180
	public static void MoveUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 position = target.transform.position;
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		if (flag)
		{
			array[0] = (array[1] = target.transform.localPosition);
		}
		else
		{
			array[0] = (array[1] = target.transform.position);
		}
		if (args.Contains("position"))
		{
			if (args["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["position"];
				array[1] = transform.position;
			}
			else if (args["position"].GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args["position"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				array[1].x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				array[1].y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				array[1].z = (float)args["z"];
			}
		}
		array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
		if (args.Contains("orienttopath") && (bool)args["orienttopath"])
		{
			args["looktarget"] = array[3];
		}
		if (args.Contains("looktarget"))
		{
			iTween.LookUpdate(target, args);
		}
		if (flag)
		{
			target.transform.localPosition = array[3];
		}
		else
		{
			target.transform.position = array[3];
		}
		if (target.GetComponent<Rigidbody>() != null)
		{
			Vector3 position2 = target.transform.position;
			target.transform.position = position;
			target.GetComponent<Rigidbody>().MovePosition(position2);
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0001C0E9 File Offset: 0x0001A4E9
	public static void MoveUpdate(GameObject target, Vector3 position, float time)
	{
		iTween.MoveUpdate(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0001C120 File Offset: 0x0001A520
	public static void LookUpdate(GameObject target, Hashtable args)
	{
		iTween.CleanArgs(args);
		Vector3[] array = new Vector3[5];
		float num;
		if (args.Contains("looktime"))
		{
			num = (float)args["looktime"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else if (args.Contains("time"))
		{
			num = (float)args["time"] * 0.15f;
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		array[0] = target.transform.eulerAngles;
		if (args.Contains("looktarget"))
		{
			if (args["looktarget"].GetType() == typeof(Transform))
			{
				Transform transform = target.transform;
				Transform target2 = (Transform)args["looktarget"];
				Vector3? vector = (Vector3?)args["up"];
				transform.LookAt(target2, (vector == null) ? iTween.Defaults.up : vector.Value);
			}
			else if (args["looktarget"].GetType() == typeof(Vector3))
			{
				Transform transform2 = target.transform;
				Vector3 worldPosition = (Vector3)args["looktarget"];
				Vector3? vector2 = (Vector3?)args["up"];
				transform2.LookAt(worldPosition, (vector2 == null) ? iTween.Defaults.up : vector2.Value);
			}
			array[1] = target.transform.eulerAngles;
			target.transform.eulerAngles = array[0];
			array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
			target.transform.eulerAngles = array[3];
			if (args.Contains("axis"))
			{
				array[4] = target.transform.eulerAngles;
				string text = (string)args["axis"];
				if (text != null)
				{
					if (!(text == "x"))
					{
						if (!(text == "y"))
						{
							if (text == "z")
							{
								array[4].x = array[0].x;
								array[4].y = array[0].y;
							}
						}
						else
						{
							array[4].x = array[0].x;
							array[4].z = array[0].z;
						}
					}
					else
					{
						array[4].y = array[0].y;
						array[4].z = array[0].z;
					}
				}
				target.transform.eulerAngles = array[4];
			}
			return;
		}
		Debug.LogError("iTween Error: LookUpdate needs a 'looktarget' property!");
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0001C4C4 File Offset: 0x0001A8C4
	public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookUpdate(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x0001C4FC File Offset: 0x0001A8FC
	public static float PathLength(Transform[] path)
	{
		Vector3[] array = new Vector3[path.Length];
		float num = 0f;
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		Vector3[] pts = iTween.PathControlPointGenerator(array);
		Vector3 a = iTween.Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int j = 1; j <= num2; j++)
		{
			float t = (float)j / (float)num2;
			Vector3 vector = iTween.Interp(pts, t);
			num += Vector3.Distance(a, vector);
			a = vector;
		}
		return num;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0001C598 File Offset: 0x0001A998
	public static float PathLength(Vector3[] path)
	{
		float num = 0f;
		Vector3[] pts = iTween.PathControlPointGenerator(path);
		Vector3 a = iTween.Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int i = 1; i <= num2; i++)
		{
			float t = (float)i / (float)num2;
			Vector3 vector = iTween.Interp(pts, t);
			num += Vector3.Distance(a, vector);
			a = vector;
		}
		return num;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0001C5FC File Offset: 0x0001A9FC
	public static Texture2D CameraTexture(Color color)
	{
		Texture2D texture2D = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
		Color[] array = new Color[Screen.width * Screen.height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		texture2D.SetPixels(array);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0001C65B File Offset: 0x0001AA5B
	public static void PutOnPath(GameObject target, Vector3[] path, float percent)
	{
		target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0001C674 File Offset: 0x0001AA74
	public static void PutOnPath(Transform target, Vector3[] path, float percent)
	{
		target.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0001C688 File Offset: 0x0001AA88
	public static void PutOnPath(GameObject target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0001C6E0 File Offset: 0x0001AAE0
	public static void PutOnPath(Transform target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		target.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0001C730 File Offset: 0x0001AB30
	public static Vector3 PointOnPath(Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		return iTween.Interp(iTween.PathControlPointGenerator(array), percent);
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0001C77A File Offset: 0x0001AB7A
	public static void DrawLine(Vector3[] line)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0001C795 File Offset: 0x0001AB95
	public static void DrawLine(Vector3[] line, Color color)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, color, "gizmos");
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0001C7AC File Offset: 0x0001ABAC
	public static void DrawLine(Transform[] line)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0001C804 File Offset: 0x0001AC04
	public static void DrawLine(Transform[] line, Color color)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, color, "gizmos");
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0001C857 File Offset: 0x0001AC57
	public static void DrawLineGizmos(Vector3[] line)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0001C872 File Offset: 0x0001AC72
	public static void DrawLineGizmos(Vector3[] line, Color color)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, color, "gizmos");
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0001C88C File Offset: 0x0001AC8C
	public static void DrawLineGizmos(Transform[] line)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0001C8E4 File Offset: 0x0001ACE4
	public static void DrawLineGizmos(Transform[] line, Color color)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, color, "gizmos");
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0001C937 File Offset: 0x0001AD37
	public static void DrawLineHandles(Vector3[] line)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0001C952 File Offset: 0x0001AD52
	public static void DrawLineHandles(Vector3[] line, Color color)
	{
		if (line.Length > 0)
		{
			iTween.DrawLineHelper(line, color, "handles");
		}
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0001C96C File Offset: 0x0001AD6C
	public static void DrawLineHandles(Transform[] line)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0001C9C4 File Offset: 0x0001ADC4
	public static void DrawLineHandles(Transform[] line, Color color)
	{
		if (line.Length > 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, color, "handles");
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0001CA17 File Offset: 0x0001AE17
	public static Vector3 PointOnPath(Vector3[] path, float percent)
	{
		return iTween.Interp(iTween.PathControlPointGenerator(path), percent);
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0001CA25 File Offset: 0x0001AE25
	public static void DrawPath(Vector3[] path)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0001CA40 File Offset: 0x0001AE40
	public static void DrawPath(Vector3[] path, Color color)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, color, "gizmos");
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0001CA58 File Offset: 0x0001AE58
	public static void DrawPath(Transform[] path)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0001CAB0 File Offset: 0x0001AEB0
	public static void DrawPath(Transform[] path, Color color)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, color, "gizmos");
		}
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0001CB03 File Offset: 0x0001AF03
	public static void DrawPathGizmos(Vector3[] path)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0001CB1E File Offset: 0x0001AF1E
	public static void DrawPathGizmos(Vector3[] path, Color color)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, color, "gizmos");
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0001CB38 File Offset: 0x0001AF38
	public static void DrawPathGizmos(Transform[] path)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001CB90 File Offset: 0x0001AF90
	public static void DrawPathGizmos(Transform[] path, Color color)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, color, "gizmos");
		}
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0001CBE3 File Offset: 0x0001AFE3
	public static void DrawPathHandles(Vector3[] path)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0001CBFE File Offset: 0x0001AFFE
	public static void DrawPathHandles(Vector3[] path, Color color)
	{
		if (path.Length > 0)
		{
			iTween.DrawPathHelper(path, color, "handles");
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0001CC18 File Offset: 0x0001B018
	public static void DrawPathHandles(Transform[] path)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0001CC70 File Offset: 0x0001B070
	public static void DrawPathHandles(Transform[] path, Color color)
	{
		if (path.Length > 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, color, "handles");
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0001CCC4 File Offset: 0x0001B0C4
	public static void CameraFadeDepth(int depth)
	{
		if (iTween.cameraFade)
		{
			iTween.cameraFade.transform.position = new Vector3(iTween.cameraFade.transform.position.x, iTween.cameraFade.transform.position.y, (float)depth);
		}
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0001CD24 File Offset: 0x0001B124
	public static void CameraFadeDestroy()
	{
		if (iTween.cameraFade)
		{
			UnityEngine.Object.Destroy(iTween.cameraFade);
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0001CD3F File Offset: 0x0001B13F
	public static void CameraFadeSwap(Texture2D texture)
	{
		if (iTween.cameraFade)
		{
			iTween.cameraFade.GetComponent<GUITexture>().texture = texture;
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0001CD60 File Offset: 0x0001B160
	public static GameObject CameraFadeAdd(Texture2D texture, int depth)
	{
		if (iTween.cameraFade)
		{
			return null;
		}
		iTween.cameraFade = new GameObject("iTween Camera Fade");
		iTween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)depth);
		iTween.cameraFade.AddComponent<GUITexture>();
		iTween.cameraFade.GetComponent<GUITexture>().texture = texture;
		iTween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
		return iTween.cameraFade;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0001CDF8 File Offset: 0x0001B1F8
	public static GameObject CameraFadeAdd(Texture2D texture)
	{
		if (iTween.cameraFade)
		{
			return null;
		}
		iTween.cameraFade = new GameObject("iTween Camera Fade");
		iTween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)iTween.Defaults.cameraFadeDepth);
		iTween.cameraFade.AddComponent<GUITexture>();
		iTween.cameraFade.GetComponent<GUITexture>().texture = texture;
		iTween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
		return iTween.cameraFade;
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x0001CE94 File Offset: 0x0001B294
	public static GameObject CameraFadeAdd()
	{
		if (iTween.cameraFade)
		{
			return null;
		}
		iTween.cameraFade = new GameObject("iTween Camera Fade");
		iTween.cameraFade.transform.position = new Vector3(0.5f, 0.5f, (float)iTween.Defaults.cameraFadeDepth);
		iTween.cameraFade.AddComponent<GUITexture>();
		iTween.cameraFade.GetComponent<GUITexture>().texture = iTween.CameraTexture(Color.black);
		iTween.cameraFade.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
		return iTween.cameraFade;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0001CF38 File Offset: 0x0001B338
	public static void Resume(GameObject target)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			iTween.enabled = true;
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0001CF74 File Offset: 0x0001B374
	public static void Resume(GameObject target, bool includechildren)
	{
		iTween.Resume(target);
		if (includechildren)
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.Resume(transform.gameObject, true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0001CFEC File Offset: 0x0001B3EC
	public static void Resume(GameObject target, string type)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				iTween.enabled = true;
			}
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0001D060 File Offset: 0x0001B460
	public static void Resume(GameObject target, string type, bool includechildren)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				iTween.enabled = true;
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.Resume(transform.gameObject, type, true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x0001D140 File Offset: 0x0001B540
	public static void Resume()
	{
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			GameObject target = (GameObject)hashtable["target"];
			iTween.Resume(target);
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0001D18C File Offset: 0x0001B58C
	public static void Resume(string type)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			GameObject value = (GameObject)hashtable["target"];
			arrayList.Insert(arrayList.Count, value);
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			iTween.Resume((GameObject)arrayList[j], type);
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0001D214 File Offset: 0x0001B614
	public static void Pause(GameObject target)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			if (iTween.delay > 0f)
			{
				iTween.delay -= Time.time - iTween.delayStarted;
				iTween.StopCoroutine("TweenDelay");
			}
			iTween.isPaused = true;
			iTween.enabled = false;
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0001D28C File Offset: 0x0001B68C
	public static void Pause(GameObject target, bool includechildren)
	{
		iTween.Pause(target);
		if (includechildren)
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.Pause(transform.gameObject, true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0001D304 File Offset: 0x0001B704
	public static void Pause(GameObject target, string type)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				if (iTween.delay > 0f)
				{
					iTween.delay -= Time.time - iTween.delayStarted;
					iTween.StopCoroutine("TweenDelay");
				}
				iTween.isPaused = true;
				iTween.enabled = false;
			}
		}
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0001D3B4 File Offset: 0x0001B7B4
	public static void Pause(GameObject target, string type, bool includechildren)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				if (iTween.delay > 0f)
				{
					iTween.delay -= Time.time - iTween.delayStarted;
					iTween.StopCoroutine("TweenDelay");
				}
				iTween.isPaused = true;
				iTween.enabled = false;
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.Pause(transform.gameObject, type, true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0001D4D0 File Offset: 0x0001B8D0
	public static void Pause()
	{
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			GameObject target = (GameObject)hashtable["target"];
			iTween.Pause(target);
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x0001D51C File Offset: 0x0001B91C
	public static void Pause(string type)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			GameObject value = (GameObject)hashtable["target"];
			arrayList.Insert(arrayList.Count, value);
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			iTween.Pause((GameObject)arrayList[j], type);
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x0001D5A2 File Offset: 0x0001B9A2
	public static int Count()
	{
		return iTween.tweens.Count;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0001D5B0 File Offset: 0x0001B9B0
	public static int Count(string type)
	{
		int num = 0;
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			string text = (string)hashtable["type"] + (string)hashtable["method"];
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0001D638 File Offset: 0x0001BA38
	public static int Count(GameObject target)
	{
		Component[] components = target.GetComponents<iTween>();
		return components.Length;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0001D650 File Offset: 0x0001BA50
	public static int Count(GameObject target, string type)
	{
		int num = 0;
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0001D6CC File Offset: 0x0001BACC
	public static void Stop()
	{
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			GameObject target = (GameObject)hashtable["target"];
			iTween.Stop(target);
		}
		iTween.tweens.Clear();
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x0001D724 File Offset: 0x0001BB24
	public static void Stop(string type)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			GameObject value = (GameObject)hashtable["target"];
			arrayList.Insert(arrayList.Count, value);
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			iTween.Stop((GameObject)arrayList[j], type);
		}
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0001D7AC File Offset: 0x0001BBAC
	public static void StopByName(string name)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			GameObject value = (GameObject)hashtable["target"];
			arrayList.Insert(arrayList.Count, value);
		}
		for (int j = 0; j < arrayList.Count; j++)
		{
			iTween.StopByName((GameObject)arrayList[j], name);
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0001D834 File Offset: 0x0001BC34
	public static void Stop(GameObject target)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			iTween.Dispose();
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0001D870 File Offset: 0x0001BC70
	public static void Stop(GameObject target, bool includechildren)
	{
		iTween.Stop(target);
		if (includechildren)
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.Stop(transform.gameObject, true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0001D8E8 File Offset: 0x0001BCE8
	public static void Stop(GameObject target, string type)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				iTween.Dispose();
			}
		}
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0001D95C File Offset: 0x0001BD5C
	public static void StopByName(GameObject target, string name)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			if (iTween._name == name)
			{
				iTween.Dispose();
			}
		}
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0001D9A8 File Offset: 0x0001BDA8
	public static void Stop(GameObject target, string type, bool includechildren)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			string text = iTween.type + iTween.method;
			text = text.Substring(0, type.Length);
			if (text.ToLower() == type.ToLower())
			{
				iTween.Dispose();
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.Stop(transform.gameObject, type, true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0001DA88 File Offset: 0x0001BE88
	public static void StopByName(GameObject target, string name, bool includechildren)
	{
		Component[] components = target.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			if (iTween._name == name)
			{
				iTween.Dispose();
			}
		}
		if (includechildren)
		{
			IEnumerator enumerator = target.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					iTween.StopByName(transform.gameObject, name, true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0001DB40 File Offset: 0x0001BF40
	public static Hashtable Hash(params object[] args)
	{
		Hashtable hashtable = new Hashtable(args.Length / 2);
		if (args.Length % 2 != 0)
		{
			Debug.LogError("Tween Error: Hash requires an even number of arguments!");
			return null;
		}
		for (int i = 0; i < args.Length - 1; i += 2)
		{
			hashtable.Add(args[i], args[i + 1]);
		}
		return hashtable;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0001DB93 File Offset: 0x0001BF93
	private void Awake()
	{
		this.thisTransform = base.transform;
		this.RetrieveArgs();
		this.lastRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0001DBB4 File Offset: 0x0001BFB4
	private IEnumerator Start()
	{
		if (this.delay > 0f)
		{
			yield return base.StartCoroutine("TweenDelay");
		}
		this.TweenStart();
		yield break;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0001DBD0 File Offset: 0x0001BFD0
	private void Update()
	{
		if (this.isRunning && !this.physics)
		{
			if (!this.reverse)
			{
				if (this.percentage < 1f)
				{
					this.TweenUpdate();
				}
				else
				{
					this.TweenComplete();
				}
			}
			else if (this.percentage > 0f)
			{
				this.TweenUpdate();
			}
			else
			{
				this.TweenComplete();
			}
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0001DC48 File Offset: 0x0001C048
	private void FixedUpdate()
	{
		if (this.isRunning && this.physics)
		{
			if (!this.reverse)
			{
				if (this.percentage < 1f)
				{
					this.TweenUpdate();
				}
				else
				{
					this.TweenComplete();
				}
			}
			else if (this.percentage > 0f)
			{
				this.TweenUpdate();
			}
			else
			{
				this.TweenComplete();
			}
		}
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0001DCC0 File Offset: 0x0001C0C0
	private void LateUpdate()
	{
		if (this.tweenArguments.Contains("looktarget") && this.isRunning && (this.type == "move" || this.type == "shake" || this.type == "punch"))
		{
			iTween.LookUpdate(base.gameObject, this.tweenArguments);
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0001DD40 File Offset: 0x0001C140
	private void OnEnable()
	{
		if (this.isRunning)
		{
			this.EnableKinematic();
		}
		if (this.isPaused)
		{
			this.isPaused = false;
			if (this.delay > 0f)
			{
				this.wasPaused = true;
				this.ResumeDelay();
			}
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0001DD8D File Offset: 0x0001C18D
	private void OnDisable()
	{
		this.DisableKinematic();
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0001DD98 File Offset: 0x0001C198
	private static void DrawLineHelper(Vector3[] line, Color color, string method)
	{
		Gizmos.color = color;
		for (int i = 0; i < line.Length - 1; i++)
		{
			if (method == "gizmos")
			{
				Gizmos.DrawLine(line[i], line[i + 1]);
			}
			else if (method == "handles")
			{
				Debug.LogError("iTween Error: Drawing a line with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
			}
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0001DE10 File Offset: 0x0001C210
	private static void DrawPathHelper(Vector3[] path, Color color, string method)
	{
		Vector3[] pts = iTween.PathControlPointGenerator(path);
		Vector3 to = iTween.Interp(pts, 0f);
		Gizmos.color = color;
		int num = path.Length * 20;
		for (int i = 1; i <= num; i++)
		{
			float t = (float)i / (float)num;
			Vector3 vector = iTween.Interp(pts, t);
			if (method == "gizmos")
			{
				Gizmos.DrawLine(vector, to);
			}
			else if (method == "handles")
			{
				Debug.LogError("iTween Error: Drawing a path with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
			}
			to = vector;
		}
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0001DE9C File Offset: 0x0001C29C
	private static Vector3[] PathControlPointGenerator(Vector3[] path)
	{
		int num = 2;
		Vector3[] array = new Vector3[path.Length + num];
		Array.Copy(path, 0, array, 1, path.Length);
		array[0] = array[1] + (array[1] - array[2]);
		array[array.Length - 1] = array[array.Length - 2] + (array[array.Length - 2] - array[array.Length - 3]);
		if (array[1] == array[array.Length - 2])
		{
			Vector3[] array2 = new Vector3[array.Length];
			Array.Copy(array, array2, array.Length);
			array2[0] = array2[array2.Length - 3];
			array2[array2.Length - 1] = array2[2];
			array = new Vector3[array2.Length];
			Array.Copy(array2, array, array2.Length);
		}
		return array;
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0001DFD0 File Offset: 0x0001C3D0
	private static Vector3 Interp(Vector3[] pts, float t)
	{
		int num = pts.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 a = pts[num2];
		Vector3 a2 = pts[num2 + 1];
		Vector3 vector = pts[num2 + 2];
		Vector3 b = pts[num2 + 3];
		return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num3 * num3 * num3) + (2f * a - 5f * a2 + 4f * vector - b) * (num3 * num3) + (-a + vector) * num3 + 2f * a2);
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0001E0E8 File Offset: 0x0001C4E8
	private static void Launch(GameObject target, Hashtable args)
	{
		if (!args.Contains("id"))
		{
			args["id"] = iTween.GenerateID();
		}
		if (!args.Contains("target"))
		{
			args["target"] = target;
		}
		iTween.tweens.Insert(0, args);
		target.AddComponent<iTween>();
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0001E144 File Offset: 0x0001C544
	private static Hashtable CleanArgs(Hashtable args)
	{
		Hashtable hashtable = new Hashtable(args.Count);
		Hashtable hashtable2 = new Hashtable(args.Count);
		IDictionaryEnumerator enumerator = args.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				hashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		IDictionaryEnumerator enumerator2 = hashtable.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				if (dictionaryEntry2.Value.GetType() == typeof(int))
				{
					int num = (int)dictionaryEntry2.Value;
					float num2 = (float)num;
					args[dictionaryEntry2.Key] = num2;
				}
				if (dictionaryEntry2.Value.GetType() == typeof(double))
				{
					double num3 = (double)dictionaryEntry2.Value;
					float num4 = (float)num3;
					args[dictionaryEntry2.Key] = num4;
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
		IDictionaryEnumerator enumerator3 = args.GetEnumerator();
		try
		{
			while (enumerator3.MoveNext())
			{
				object obj3 = enumerator3.Current;
				DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
				hashtable2.Add(dictionaryEntry3.Key.ToString().ToLower(), dictionaryEntry3.Value);
			}
		}
		finally
		{
			IDisposable disposable3;
			if ((disposable3 = (enumerator3 as IDisposable)) != null)
			{
				disposable3.Dispose();
			}
		}
		args = hashtable2;
		return args;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0001E310 File Offset: 0x0001C710
	private static string GenerateID()
	{
		return Guid.NewGuid().ToString();
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0001E330 File Offset: 0x0001C730
	private void RetrieveArgs()
	{
		foreach (Hashtable hashtable in iTween.tweens)
		{
			if ((GameObject)hashtable["target"] == base.gameObject)
			{
				this.tweenArguments = hashtable;
				break;
			}
		}
		this.id = (string)this.tweenArguments["id"];
		this.type = (string)this.tweenArguments["type"];
		this._name = (string)this.tweenArguments["name"];
		this.method = (string)this.tweenArguments["method"];
		if (this.tweenArguments.Contains("time"))
		{
			this.time = (float)this.tweenArguments["time"];
		}
		else
		{
			this.time = iTween.Defaults.time;
		}
		if (base.GetComponent<Rigidbody>() != null)
		{
			this.physics = true;
		}
		if (this.tweenArguments.Contains("delay"))
		{
			this.delay = (float)this.tweenArguments["delay"];
		}
		else
		{
			this.delay = iTween.Defaults.delay;
		}
		if (this.tweenArguments.Contains("namedcolorvalue"))
		{
			if (this.tweenArguments["namedcolorvalue"].GetType() == typeof(iTween.NamedValueColor))
			{
				this.namedcolorvalue = (iTween.NamedValueColor)this.tweenArguments["namedcolorvalue"];
			}
			else
			{
				try
				{
					this.namedcolorvalue = (iTween.NamedValueColor)Enum.Parse(typeof(iTween.NamedValueColor), (string)this.tweenArguments["namedcolorvalue"], true);
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported namedcolorvalue supplied! Default will be used.");
					this.namedcolorvalue = iTween.NamedValueColor._Color;
				}
			}
		}
		else
		{
			this.namedcolorvalue = iTween.Defaults.namedColorValue;
		}
		if (this.tweenArguments.Contains("looptype"))
		{
			if (this.tweenArguments["looptype"].GetType() == typeof(iTween.LoopType))
			{
				this.loopType = (iTween.LoopType)this.tweenArguments["looptype"];
			}
			else
			{
				try
				{
					this.loopType = (iTween.LoopType)Enum.Parse(typeof(iTween.LoopType), (string)this.tweenArguments["looptype"], true);
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported loopType supplied! Default will be used.");
					this.loopType = iTween.LoopType.none;
				}
			}
		}
		else
		{
			this.loopType = iTween.LoopType.none;
		}
		if (this.tweenArguments.Contains("easetype"))
		{
			if (this.tweenArguments["easetype"].GetType() == typeof(iTween.EaseType))
			{
				this.easeType = (iTween.EaseType)this.tweenArguments["easetype"];
			}
			else
			{
				try
				{
					this.easeType = (iTween.EaseType)Enum.Parse(typeof(iTween.EaseType), (string)this.tweenArguments["easetype"], true);
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported easeType supplied! Default will be used.");
					this.easeType = iTween.Defaults.easeType;
				}
			}
		}
		else
		{
			this.easeType = iTween.Defaults.easeType;
		}
		if (this.tweenArguments.Contains("space"))
		{
			if (this.tweenArguments["space"].GetType() == typeof(Space))
			{
				this.space = (Space)this.tweenArguments["space"];
			}
			else
			{
				try
				{
					this.space = (Space)Enum.Parse(typeof(Space), (string)this.tweenArguments["space"], true);
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported space supplied! Default will be used.");
					this.space = iTween.Defaults.space;
				}
			}
		}
		else
		{
			this.space = iTween.Defaults.space;
		}
		if (this.tweenArguments.Contains("islocal"))
		{
			this.isLocal = (bool)this.tweenArguments["islocal"];
		}
		else
		{
			this.isLocal = iTween.Defaults.isLocal;
		}
		if (this.tweenArguments.Contains("ignoretimescale"))
		{
			this.useRealTime = (bool)this.tweenArguments["ignoretimescale"];
		}
		else
		{
			this.useRealTime = iTween.Defaults.useRealTime;
		}
		this.GetEasingFunction();
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0001E844 File Offset: 0x0001CC44
	private void GetEasingFunction()
	{
		switch (this.easeType)
		{
		case iTween.EaseType.easeInQuad:
			this.ease = new iTween.EasingFunction(this.easeInQuad);
			break;
		case iTween.EaseType.easeOutQuad:
			this.ease = new iTween.EasingFunction(this.easeOutQuad);
			break;
		case iTween.EaseType.easeInOutQuad:
			this.ease = new iTween.EasingFunction(this.easeInOutQuad);
			break;
		case iTween.EaseType.easeInCubic:
			this.ease = new iTween.EasingFunction(this.easeInCubic);
			break;
		case iTween.EaseType.easeOutCubic:
			this.ease = new iTween.EasingFunction(this.easeOutCubic);
			break;
		case iTween.EaseType.easeInOutCubic:
			this.ease = new iTween.EasingFunction(this.easeInOutCubic);
			break;
		case iTween.EaseType.easeInQuart:
			this.ease = new iTween.EasingFunction(this.easeInQuart);
			break;
		case iTween.EaseType.easeOutQuart:
			this.ease = new iTween.EasingFunction(this.easeOutQuart);
			break;
		case iTween.EaseType.easeInOutQuart:
			this.ease = new iTween.EasingFunction(this.easeInOutQuart);
			break;
		case iTween.EaseType.easeInQuint:
			this.ease = new iTween.EasingFunction(this.easeInQuint);
			break;
		case iTween.EaseType.easeOutQuint:
			this.ease = new iTween.EasingFunction(this.easeOutQuint);
			break;
		case iTween.EaseType.easeInOutQuint:
			this.ease = new iTween.EasingFunction(this.easeInOutQuint);
			break;
		case iTween.EaseType.easeInSine:
			this.ease = new iTween.EasingFunction(this.easeInSine);
			break;
		case iTween.EaseType.easeOutSine:
			this.ease = new iTween.EasingFunction(this.easeOutSine);
			break;
		case iTween.EaseType.easeInOutSine:
			this.ease = new iTween.EasingFunction(this.easeInOutSine);
			break;
		case iTween.EaseType.easeInExpo:
			this.ease = new iTween.EasingFunction(this.easeInExpo);
			break;
		case iTween.EaseType.easeOutExpo:
			this.ease = new iTween.EasingFunction(this.easeOutExpo);
			break;
		case iTween.EaseType.easeInOutExpo:
			this.ease = new iTween.EasingFunction(this.easeInOutExpo);
			break;
		case iTween.EaseType.easeInCirc:
			this.ease = new iTween.EasingFunction(this.easeInCirc);
			break;
		case iTween.EaseType.easeOutCirc:
			this.ease = new iTween.EasingFunction(this.easeOutCirc);
			break;
		case iTween.EaseType.easeInOutCirc:
			this.ease = new iTween.EasingFunction(this.easeInOutCirc);
			break;
		case iTween.EaseType.linear:
			this.ease = new iTween.EasingFunction(this.linear);
			break;
		case iTween.EaseType.spring:
			this.ease = new iTween.EasingFunction(this.spring);
			break;
		case iTween.EaseType.easeInBounce:
			this.ease = new iTween.EasingFunction(this.easeInBounce);
			break;
		case iTween.EaseType.easeOutBounce:
			this.ease = new iTween.EasingFunction(this.easeOutBounce);
			break;
		case iTween.EaseType.easeInOutBounce:
			this.ease = new iTween.EasingFunction(this.easeInOutBounce);
			break;
		case iTween.EaseType.easeInBack:
			this.ease = new iTween.EasingFunction(this.easeInBack);
			break;
		case iTween.EaseType.easeOutBack:
			this.ease = new iTween.EasingFunction(this.easeOutBack);
			break;
		case iTween.EaseType.easeInOutBack:
			this.ease = new iTween.EasingFunction(this.easeInOutBack);
			break;
		case iTween.EaseType.easeInElastic:
			this.ease = new iTween.EasingFunction(this.easeInElastic);
			break;
		case iTween.EaseType.easeOutElastic:
			this.ease = new iTween.EasingFunction(this.easeOutElastic);
			break;
		case iTween.EaseType.easeInOutElastic:
			this.ease = new iTween.EasingFunction(this.easeInOutElastic);
			break;
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001EBC4 File Offset: 0x0001CFC4
	private void UpdatePercentage()
	{
		if (this.useRealTime)
		{
			this.runningTime += Time.realtimeSinceStartup - this.lastRealTime;
		}
		else
		{
			this.runningTime += Time.deltaTime;
		}
		if (this.reverse)
		{
			this.percentage = 1f - this.runningTime / this.time;
		}
		else
		{
			this.percentage = this.runningTime / this.time;
		}
		this.lastRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001EC54 File Offset: 0x0001D054
	private void CallBack(string callbackType)
	{
		if (this.tweenArguments.Contains(callbackType) && !this.tweenArguments.Contains("ischild"))
		{
			GameObject gameObject;
			if (this.tweenArguments.Contains(callbackType + "target"))
			{
				gameObject = (GameObject)this.tweenArguments[callbackType + "target"];
			}
			else
			{
				gameObject = base.gameObject;
			}
			if (this.tweenArguments[callbackType].GetType() == typeof(string))
			{
				gameObject.SendMessage((string)this.tweenArguments[callbackType], this.tweenArguments[callbackType + "params"], SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				Debug.LogError("iTween Error: Callback method references must be passed as a String!");
				UnityEngine.Object.Destroy(this);
			}
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001ED30 File Offset: 0x0001D130
	private void Dispose()
	{
		for (int i = 0; i < iTween.tweens.Count; i++)
		{
			Hashtable hashtable = iTween.tweens[i];
			if ((string)hashtable["id"] == this.id)
			{
				iTween.tweens.RemoveAt(i);
				break;
			}
		}
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001ED9C File Offset: 0x0001D19C
	private void ConflictCheck()
	{
		Component[] components = base.GetComponents<iTween>();
		foreach (iTween iTween in components)
		{
			if (iTween.type == "value")
			{
				return;
			}
			if (iTween.isRunning && iTween.type == this.type)
			{
				if (iTween.method != this.method)
				{
					return;
				}
				if (iTween.tweenArguments.Count != this.tweenArguments.Count)
				{
					iTween.Dispose();
					return;
				}
				IDictionaryEnumerator enumerator = this.tweenArguments.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!iTween.tweenArguments.Contains(dictionaryEntry.Key))
						{
							iTween.Dispose();
							return;
						}
						if (!iTween.tweenArguments[dictionaryEntry.Key].Equals(this.tweenArguments[dictionaryEntry.Key]) && (string)dictionaryEntry.Key != "id")
						{
							iTween.Dispose();
							return;
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				this.Dispose();
			}
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001EF14 File Offset: 0x0001D314
	private void EnableKinematic()
	{
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001EF16 File Offset: 0x0001D316
	private void DisableKinematic()
	{
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001EF18 File Offset: 0x0001D318
	private void ResumeDelay()
	{
		base.StartCoroutine("TweenDelay");
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001EF26 File Offset: 0x0001D326
	private float linear(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, value);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001EF30 File Offset: 0x0001D330
	private float clerp(float start, float end, float value)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) * 0.5f);
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

	// Token: 0x0600055F RID: 1375 RVA: 0x0001EFA8 File Offset: 0x0001D3A8
	private float spring(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * 3.14159274f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
		return start + (end - start) * value;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001F00C File Offset: 0x0001D40C
	private float easeInQuad(float start, float end, float value)
	{
		end -= start;
		return end * value * value + start;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001F01A File Offset: 0x0001D41A
	private float easeOutQuad(float start, float end, float value)
	{
		end -= start;
		return -end * value * (value - 2f) + start;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001F030 File Offset: 0x0001D430
	private float easeInOutQuad(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value + start;
		}
		value -= 1f;
		return -end * 0.5f * (value * (value - 2f) - 1f) + start;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001F087 File Offset: 0x0001D487
	private float easeInCubic(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value + start;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001F097 File Offset: 0x0001D497
	private float easeOutCubic(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value + 1f) + start;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001F0B8 File Offset: 0x0001D4B8
	private float easeInOutCubic(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value * value + start;
		}
		value -= 2f;
		return end * 0.5f * (value * value * value + 2f) + start;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0001F10C File Offset: 0x0001D50C
	private float easeInQuart(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value + start;
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001F11E File Offset: 0x0001D51E
	private float easeOutQuart(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return -end * (value * value * value * value - 1f) + start;
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001F140 File Offset: 0x0001D540
	private float easeInOutQuart(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value * value * value + start;
		}
		value -= 2f;
		return -end * 0.5f * (value * value * value * value - 2f) + start;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001F199 File Offset: 0x0001D599
	private float easeInQuint(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value * value + start;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001F1AD File Offset: 0x0001D5AD
	private float easeOutQuint(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value * value * value + 1f) + start;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001F1D0 File Offset: 0x0001D5D0
	private float easeInOutQuint(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * value * value * value * value * value + start;
		}
		value -= 2f;
		return end * 0.5f * (value * value * value * value * value + 2f) + start;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0001F22C File Offset: 0x0001D62C
	private float easeInSine(float start, float end, float value)
	{
		end -= start;
		return -end * Mathf.Cos(value * 1.57079637f) + end + start;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0001F246 File Offset: 0x0001D646
	private float easeOutSine(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Sin(value * 1.57079637f) + start;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0001F25D File Offset: 0x0001D65D
	private float easeInOutSine(float start, float end, float value)
	{
		end -= start;
		return -end * 0.5f * (Mathf.Cos(3.14159274f * value) - 1f) + start;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001F281 File Offset: 0x0001D681
	private float easeInExpo(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0001F2A3 File Offset: 0x0001D6A3
	private float easeOutExpo(float start, float end, float value)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * value) + 1f) + start;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0001F2C8 File Offset: 0x0001D6C8
	private float easeInOutExpo(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end * 0.5f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}
		value -= 1f;
		return end * 0.5f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0001F33B File Offset: 0x0001D73B
	private float easeInCirc(float start, float end, float value)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0001F35B File Offset: 0x0001D75B
	private float easeOutCirc(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - value * value) + start;
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0001F380 File Offset: 0x0001D780
	private float easeInOutCirc(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return -end * 0.5f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}
		value -= 2f;
		return end * 0.5f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0001F3F0 File Offset: 0x0001D7F0
	private float easeInBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		return end - this.easeOutBounce(0f, end, num - value) + start;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0001F41C File Offset: 0x0001D81C
	private float easeOutBounce(float start, float end, float value)
	{
		value /= 1f;
		end -= start;
		if (value < 0.363636374f)
		{
			return end * (7.5625f * value * value) + start;
		}
		if (value < 0.727272749f)
		{
			value -= 0.545454562f;
			return end * (7.5625f * value * value + 0.75f) + start;
		}
		if ((double)value < 0.90909090909090906)
		{
			value -= 0.8181818f;
			return end * (7.5625f * value * value + 0.9375f) + start;
		}
		value -= 0.954545438f;
		return end * (7.5625f * value * value + 0.984375f) + start;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0001F4C4 File Offset: 0x0001D8C4
	private float easeInOutBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		if (value < num * 0.5f)
		{
			return this.easeInBounce(0f, end, value * 2f) * 0.5f + start;
		}
		return this.easeOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0001F52C File Offset: 0x0001D92C
	private float easeInBack(float start, float end, float value)
	{
		end -= start;
		value /= 1f;
		float num = 1.70158f;
		return end * value * value * ((num + 1f) * value - num) + start;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0001F560 File Offset: 0x0001D960
	private float easeOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value -= 1f;
		return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0001F59C File Offset: 0x0001D99C
	private float easeInOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value /= 0.5f;
		if (value < 1f)
		{
			num *= 1.525f;
			return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
		}
		value -= 2f;
		num *= 1.525f;
		return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0001F61C File Offset: 0x0001DA1C
	private float punch(float amplitude, float value)
	{
		if (value == 0f)
		{
			return 0f;
		}
		if (value == 1f)
		{
			return 0f;
		}
		float num = 0.3f;
		float num2 = num / 6.28318548f * Mathf.Asin(0f);
		return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num2) * 6.28318548f / num);
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0001F694 File Offset: 0x0001DA94
	private float easeInElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
		}
		return -(num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.28318548f / num2)) + start;
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0001F74C File Offset: 0x0001DB4C
	private float easeOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 * 0.25f;
		}
		else
		{
			num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
		}
		return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * 6.28318548f / num2) + end + start;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0001F7FC File Offset: 0x0001DBFC
	private float easeInOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num * 0.5f) == 2f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / 6.28318548f * Mathf.Asin(end / num3);
		}
		if (value < 1f)
		{
			return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.28318548f / num2)) + start;
		}
		return num3 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.28318548f / num2) * 0.5f + end + start;
	}

	// Token: 0x0400028F RID: 655
	public static List<Hashtable> tweens = new List<Hashtable>();

	// Token: 0x04000290 RID: 656
	private static GameObject cameraFade;

	// Token: 0x04000291 RID: 657
	public string id;

	// Token: 0x04000292 RID: 658
	public string type;

	// Token: 0x04000293 RID: 659
	public string method;

	// Token: 0x04000294 RID: 660
	public iTween.EaseType easeType;

	// Token: 0x04000295 RID: 661
	public float time;

	// Token: 0x04000296 RID: 662
	public float delay;

	// Token: 0x04000297 RID: 663
	public iTween.LoopType loopType;

	// Token: 0x04000298 RID: 664
	public bool isRunning;

	// Token: 0x04000299 RID: 665
	public bool isPaused;

	// Token: 0x0400029A RID: 666
	public string _name;

	// Token: 0x0400029B RID: 667
	private float runningTime;

	// Token: 0x0400029C RID: 668
	private float percentage;

	// Token: 0x0400029D RID: 669
	private float delayStarted;

	// Token: 0x0400029E RID: 670
	private bool kinematic;

	// Token: 0x0400029F RID: 671
	private bool isLocal;

	// Token: 0x040002A0 RID: 672
	private bool loop;

	// Token: 0x040002A1 RID: 673
	private bool reverse;

	// Token: 0x040002A2 RID: 674
	private bool wasPaused;

	// Token: 0x040002A3 RID: 675
	private bool physics;

	// Token: 0x040002A4 RID: 676
	private Hashtable tweenArguments;

	// Token: 0x040002A5 RID: 677
	private Space space;

	// Token: 0x040002A6 RID: 678
	private iTween.EasingFunction ease;

	// Token: 0x040002A7 RID: 679
	private iTween.ApplyTween apply;

	// Token: 0x040002A8 RID: 680
	private AudioSource audioSource;

	// Token: 0x040002A9 RID: 681
	private Vector3[] vector3s;

	// Token: 0x040002AA RID: 682
	private Vector2[] vector2s;

	// Token: 0x040002AB RID: 683
	private Color[,] colors;

	// Token: 0x040002AC RID: 684
	private float[] floats;

	// Token: 0x040002AD RID: 685
	private Rect[] rects;

	// Token: 0x040002AE RID: 686
	private iTween.CRSpline path;

	// Token: 0x040002AF RID: 687
	private Vector3 preUpdate;

	// Token: 0x040002B0 RID: 688
	private Vector3 postUpdate;

	// Token: 0x040002B1 RID: 689
	private iTween.NamedValueColor namedcolorvalue;

	// Token: 0x040002B2 RID: 690
	private float lastRealTime;

	// Token: 0x040002B3 RID: 691
	private bool useRealTime;

	// Token: 0x040002B4 RID: 692
	private Transform thisTransform;

	// Token: 0x0200007A RID: 122
	// (Invoke) Token: 0x06000581 RID: 1409
	private delegate float EasingFunction(float start, float end, float Value);

	// Token: 0x0200007B RID: 123
	// (Invoke) Token: 0x06000585 RID: 1413
	private delegate void ApplyTween();

	// Token: 0x0200007C RID: 124
	public enum EaseType
	{
		// Token: 0x040002B7 RID: 695
		easeInQuad,
		// Token: 0x040002B8 RID: 696
		easeOutQuad,
		// Token: 0x040002B9 RID: 697
		easeInOutQuad,
		// Token: 0x040002BA RID: 698
		easeInCubic,
		// Token: 0x040002BB RID: 699
		easeOutCubic,
		// Token: 0x040002BC RID: 700
		easeInOutCubic,
		// Token: 0x040002BD RID: 701
		easeInQuart,
		// Token: 0x040002BE RID: 702
		easeOutQuart,
		// Token: 0x040002BF RID: 703
		easeInOutQuart,
		// Token: 0x040002C0 RID: 704
		easeInQuint,
		// Token: 0x040002C1 RID: 705
		easeOutQuint,
		// Token: 0x040002C2 RID: 706
		easeInOutQuint,
		// Token: 0x040002C3 RID: 707
		easeInSine,
		// Token: 0x040002C4 RID: 708
		easeOutSine,
		// Token: 0x040002C5 RID: 709
		easeInOutSine,
		// Token: 0x040002C6 RID: 710
		easeInExpo,
		// Token: 0x040002C7 RID: 711
		easeOutExpo,
		// Token: 0x040002C8 RID: 712
		easeInOutExpo,
		// Token: 0x040002C9 RID: 713
		easeInCirc,
		// Token: 0x040002CA RID: 714
		easeOutCirc,
		// Token: 0x040002CB RID: 715
		easeInOutCirc,
		// Token: 0x040002CC RID: 716
		linear,
		// Token: 0x040002CD RID: 717
		spring,
		// Token: 0x040002CE RID: 718
		easeInBounce,
		// Token: 0x040002CF RID: 719
		easeOutBounce,
		// Token: 0x040002D0 RID: 720
		easeInOutBounce,
		// Token: 0x040002D1 RID: 721
		easeInBack,
		// Token: 0x040002D2 RID: 722
		easeOutBack,
		// Token: 0x040002D3 RID: 723
		easeInOutBack,
		// Token: 0x040002D4 RID: 724
		easeInElastic,
		// Token: 0x040002D5 RID: 725
		easeOutElastic,
		// Token: 0x040002D6 RID: 726
		easeInOutElastic,
		// Token: 0x040002D7 RID: 727
		punch
	}

	// Token: 0x0200007D RID: 125
	public enum LoopType
	{
		// Token: 0x040002D9 RID: 729
		none,
		// Token: 0x040002DA RID: 730
		loop,
		// Token: 0x040002DB RID: 731
		pingPong
	}

	// Token: 0x0200007E RID: 126
	public enum NamedValueColor
	{
		// Token: 0x040002DD RID: 733
		_Color,
		// Token: 0x040002DE RID: 734
		_SpecColor,
		// Token: 0x040002DF RID: 735
		_Emission,
		// Token: 0x040002E0 RID: 736
		_ReflectColor
	}

	// Token: 0x0200007F RID: 127
	public static class Defaults
	{
		// Token: 0x040002E1 RID: 737
		public static float time = 1f;

		// Token: 0x040002E2 RID: 738
		public static float delay = 0f;

		// Token: 0x040002E3 RID: 739
		public static iTween.NamedValueColor namedColorValue = iTween.NamedValueColor._Color;

		// Token: 0x040002E4 RID: 740
		public static iTween.LoopType loopType = iTween.LoopType.none;

		// Token: 0x040002E5 RID: 741
		public static iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

		// Token: 0x040002E6 RID: 742
		public static float lookSpeed = 3f;

		// Token: 0x040002E7 RID: 743
		public static bool isLocal = false;

		// Token: 0x040002E8 RID: 744
		public static Space space = Space.Self;

		// Token: 0x040002E9 RID: 745
		public static bool orientToPath = false;

		// Token: 0x040002EA RID: 746
		public static Color color = Color.white;

		// Token: 0x040002EB RID: 747
		public static float updateTimePercentage = 0.05f;

		// Token: 0x040002EC RID: 748
		public static float updateTime = 1f * iTween.Defaults.updateTimePercentage;

		// Token: 0x040002ED RID: 749
		public static int cameraFadeDepth = 999999;

		// Token: 0x040002EE RID: 750
		public static float lookAhead = 0.05f;

		// Token: 0x040002EF RID: 751
		public static bool useRealTime = false;

		// Token: 0x040002F0 RID: 752
		public static Vector3 up = Vector3.up;
	}

	// Token: 0x02000080 RID: 128
	private class CRSpline
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0001F9A8 File Offset: 0x0001DDA8
		public CRSpline(params Vector3[] pts)
		{
			this.pts = new Vector3[pts.Length];
			Array.Copy(pts, this.pts, pts.Length);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001F9D0 File Offset: 0x0001DDD0
		public Vector3 Interp(float t)
		{
			int num = this.pts.Length - 3;
			int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
			float num3 = t * (float)num - (float)num2;
			Vector3 a = this.pts[num2];
			Vector3 a2 = this.pts[num2 + 1];
			Vector3 vector = this.pts[num2 + 2];
			Vector3 b = this.pts[num2 + 3];
			return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num3 * num3 * num3) + (2f * a - 5f * a2 + 4f * vector - b) * (num3 * num3) + (-a + vector) * num3 + 2f * a2);
		}

		// Token: 0x040002F1 RID: 753
		public Vector3[] pts;
	}
}
