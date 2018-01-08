using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class LocalizationUpdateComponentText : MonoBehaviour
{
	// Token: 0x060003C4 RID: 964 RVA: 0x000116A0 File Offset: 0x0000FAA0
	private void Start()
	{
		this.SetAllObjects();
		this.SetAllText();
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x000116AE File Offset: 0x0000FAAE
	private void Update()
	{
		if (LocalizationText.GetLanguage() != this._language)
		{
			this._language = LocalizationText.GetLanguage();
			this.SetAllText();
		}
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x000116D6 File Offset: 0x0000FAD6
	private void SetAllObjects()
	{
		this.WelcomeText = GameObject.Find("Welcome");
		this.lblCastle = GameObject.Find("lblCastle");
		this.CarName = GameObject.Find("carName");
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00011708 File Offset: 0x0000FB08
	private void SetAllText()
	{
		if (this.WelcomeText != null)
		{
			this.WelcomeText.GetComponent<TextMesh>().text = LocalizationText.GetText("lblDoor111");
		}
		if (this.lblCastle != null)
		{
			this.lblCastle.GetComponent<TextMesh>().text = LocalizationText.GetText("lblCastle");
		}
		if (this.CarName != null)
		{
			this.CarName.GetComponent<TextMesh>().text = LocalizationText.GetText("CarName");
		}
	}

	// Token: 0x04000232 RID: 562
	private string _language = LocalizationText.GetLanguage();

	// Token: 0x04000233 RID: 563
	private GameObject WelcomeText;

	// Token: 0x04000234 RID: 564
	private GameObject lblCastle;

	// Token: 0x04000235 RID: 565
	private GameObject CarName;
}
