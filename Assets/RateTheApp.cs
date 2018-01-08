using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class RateTheApp : MonoBehaviour
{
	// Token: 0x060003CD RID: 973 RVA: 0x000118FB File Offset: 0x0000FCFB
	private void Start()
	{
	}

	// Token: 0x060003CE RID: 974 RVA: 0x000118FD File Offset: 0x0000FCFD
	private void Update()
	{
	}

	// Token: 0x060003CF RID: 975 RVA: 0x000118FF File Offset: 0x0000FCFF
	public void NoRatePanel()
	{
		PlayerPrefs.SetInt("AppRated", 1);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00011918 File Offset: 0x0000FD18
	public void RemindRatePanel()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00011926 File Offset: 0x0000FD26
	public void YesRatePanel()
	{
		Application.OpenURL(GlobalValues.GoogleRateUs);
		PlayerPrefs.SetInt("AppRated", 1);
		base.gameObject.SetActive(false);
	}
}
