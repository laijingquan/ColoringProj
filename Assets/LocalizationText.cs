using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class LocalizationText
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060003B9 RID: 953 RVA: 0x000113CC File Offset: 0x0000F7CC
	// (set) Token: 0x060003BA RID: 954 RVA: 0x000113D3 File Offset: 0x0000F7D3
	private static string Language
	{
		get
		{
			return LocalizationText._language;
		}
		set
		{
			if (LocalizationText._language != value)
			{
				LocalizationText._language = value;
				LocalizationText.CreateContent();
			}
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x000113F0 File Offset: 0x0000F7F0
	public static string GetText(string key)
	{
		string empty = string.Empty;
		LocalizationText.Content.TryGetValue(key, out empty);
		if (string.IsNullOrEmpty(empty))
		{
			return key + "[" + LocalizationText.Language + "] No Text defined";
		}
		return empty;
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00011433 File Offset: 0x0000F833
	public static string GetLanguage()
	{
		return LocalizationText.Language;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0001143A File Offset: 0x0000F83A
	public static void SetLanguage(string language)
	{
		LocalizationText.Language = language;
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060003BE RID: 958 RVA: 0x00011442 File Offset: 0x0000F842
	private static IDictionary<string, string> Content
	{
		get
		{
			if (LocalizationText._content == null || LocalizationText._content.Count == 0)
			{
				LocalizationText.CreateContent();
			}
			return LocalizationText._content;
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00011467 File Offset: 0x0000F867
	private static IDictionary<string, string> GetContent()
	{
		if (LocalizationText._content == null || LocalizationText._content.Count == 0)
		{
			LocalizationText.CreateContent();
		}
		return LocalizationText._content;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0001148C File Offset: 0x0000F88C
	private static void AddContent(XmlNode xNode)
	{
		IEnumerator enumerator = xNode.ChildNodes.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.LocalName == "TextKey")
				{
					string value = xmlNode.Attributes.GetNamedItem("name").Value;
					string text = string.Empty;
					IEnumerator enumerator2 = xmlNode.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.LocalName == LocalizationText._language)
							{
								text = xmlNode2.InnerText;
								if (LocalizationText._content.ContainsKey(value))
								{
									LocalizationText._content.Remove(value);
									LocalizationText._content.Add(value, value + " has been found multiple times in the XML allowed only once!");
								}
								else
								{
									LocalizationText._content.Add(value, string.IsNullOrEmpty(text) ? ("No Text for " + value + " found") : text);
								}
								break;
							}
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator2 as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0001160C File Offset: 0x0000FA0C
	private static void CreateContent()
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(Resources.Load("LocalizationText").ToString());
		if (xmlDocument == null)
		{
			Console.WriteLine("Couldnt Load Xml");
			return;
		}
		if (LocalizationText._content != null)
		{
			LocalizationText._content.Clear();
		}
		XmlNode xNode = xmlDocument.ChildNodes.Item(1).ChildNodes.Item(0);
		LocalizationText.AddContent(xNode);
	}

	// Token: 0x04000230 RID: 560
	private static IDictionary<string, string> _content = new Dictionary<string, string>();

	// Token: 0x04000231 RID: 561
	private static string _language = "EN";
}
