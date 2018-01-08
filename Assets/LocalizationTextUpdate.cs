using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006A RID: 106
public class LocalizationTextUpdate : MonoBehaviour
{
	// Token: 0x060003C9 RID: 969 RVA: 0x00011809 File Offset: 0x0000FC09
	private void Start()
	{
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0001180B File Offset: 0x0000FC0B
	private void Update()
	{
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00011810 File Offset: 0x0000FC10
	private void DetectSystemLanguage()
	{
		SystemLanguage systemLanguage = Application.systemLanguage;
		if (systemLanguage != SystemLanguage.French)
		{
			if (systemLanguage != SystemLanguage.German)
			{
				switch (systemLanguage)
				{
				case SystemLanguage.Portuguese:
					LocalizationText.SetLanguage("PT");
					break;
				default:
					if (systemLanguage != SystemLanguage.Arabic)
					{
						if (systemLanguage != SystemLanguage.English)
						{
							if (systemLanguage != SystemLanguage.Italian)
							{
								if (systemLanguage != SystemLanguage.Spanish)
								{
									LocalizationText.SetLanguage("EN");
								}
								else
								{
									LocalizationText.SetLanguage("ES");
								}
							}
							else
							{
								LocalizationText.SetLanguage("IT");
							}
						}
						else
						{
							LocalizationText.SetLanguage("EN");
						}
					}
					else
					{
						LocalizationText.SetLanguage("AR");
					}
					break;
				case SystemLanguage.Russian:
					LocalizationText.SetLanguage("RU");
					break;
				}
			}
			else
			{
				LocalizationText.SetLanguage("DE");
			}
		}
		else
		{
			LocalizationText.SetLanguage("FR");
		}
	}

	// Token: 0x04000236 RID: 566
	public Text[] allTexts;

	// Token: 0x04000237 RID: 567
	private string[] keys = new string[]
	{
		"play",
		"more",
		"removeads",
		"restorepurchase",
		"back",
		"lovethisapp",
		"rateapp",
		"yes",
		"no",
		"remindlater"
	};
}
