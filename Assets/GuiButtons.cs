using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class GuiButtons : MonoBehaviour
{
	// Token: 0x060003B7 RID: 951 RVA: 0x00010BB8 File Offset: 0x0000EFB8
	private void OnGUI()
	{
		GUI.Box(new Rect(10f, 10f, 120f, 100f), LocalizationText.GetText("lblLanguage"));
		if (this.ShowPlayerStats)
		{
			GUI.Box(new Rect(10f, 300f, 300f, 600f), LocalizationText.GetText("lblPlayerStats"));
			GUI.Label(new Rect(20f, 320f, 130f, 20f), LocalizationText.GetText("lblStrength"));
			GUI.Label(new Rect(20f, 340f, 130f, 20f), LocalizationText.GetText("lblLife"));
			GUI.Label(new Rect(20f, 360f, 130f, 20f), LocalizationText.GetText("lblEndurance"));
			GUI.Label(new Rect(20f, 380f, 130f, 20f), LocalizationText.GetText("lblWisdom"));
			GUI.Label(new Rect(20f, 400f, 130f, 20f), LocalizationText.GetText("lblIntelligence"));
			GUI.Label(new Rect(20f, 420f, 130f, 20f), LocalizationText.GetText("lblWeight"));
			GUI.Label(new Rect(20f, 440f, 130f, 20f), LocalizationText.GetText("lblHeight"));
			GUI.Label(new Rect(20f, 460f, 130f, 20f), LocalizationText.GetText("lblOld"));
			GUI.Label(new Rect(20f, 480f, 130f, 20f), LocalizationText.GetText("lblWilderness"));
			GUI.Label(new Rect(20f, 500f, 130f, 20f), LocalizationText.GetText("lblStreet"));
			GUI.Label(new Rect(20f, 520f, 130f, 20f), LocalizationText.GetText("lblFood"));
			GUI.Label(new Rect(20f, 540f, 130f, 20f), LocalizationText.GetText("lblThirst"));
			GUI.Label(new Rect(20f, 560f, 130f, 20f), LocalizationText.GetText("lblLvl"));
			GUI.Label(new Rect(20f, 580f, 130f, 20f), LocalizationText.GetText("lblSpellpower"));
			GUI.Label(new Rect(20f, 600f, 130f, 20f), LocalizationText.GetText("lblRunspeed"));
			GUI.Label(new Rect(20f, 620f, 130f, 20f), LocalizationText.GetText("lblCountry"));
			GUI.Label(new Rect(20f, 640f, 130f, 20f), LocalizationText.GetText("lblFriends"));
			GUI.Label(new Rect(20f, 660f, 130f, 20f), LocalizationText.GetText("lblEnemies"));
			GUI.Label(new Rect(20f, 680f, 130f, 20f), LocalizationText.GetText("lblMoney"));
			GUI.Label(new Rect(20f, 700f, 130f, 20f), LocalizationText.GetText("lblEarnings"));
			GUI.Label(new Rect(20f, 720f, 130f, 20f), LocalizationText.GetText("lblName"));
			GUI.Label(new Rect(20f, 740f, 130f, 20f), LocalizationText.GetText("lblSurName"));
			GUI.Label(new Rect(20f, 760f, 130f, 20f), LocalizationText.GetText("lblBorn"));
			GUI.Label(new Rect(200f, 320f, 120f, 20f), "110");
			GUI.Label(new Rect(200f, 340f, 120f, 20f), "52");
			GUI.Label(new Rect(200f, 360f, 120f, 20f), "40");
			GUI.Label(new Rect(200f, 380f, 120f, 20f), "60");
			GUI.Label(new Rect(200f, 400f, 120f, 20f), "80");
			GUI.Label(new Rect(200f, 420f, 120f, 20f), "100");
			GUI.Label(new Rect(200f, 440f, 120f, 20f), "200");
			GUI.Label(new Rect(200f, 460f, 120f, 20f), "500");
			GUI.Label(new Rect(200f, 480f, 120f, 20f), "800");
			GUI.Label(new Rect(200f, 500f, 120f, 20f), "20");
			GUI.Label(new Rect(200f, 520f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 540f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 560f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 580f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 600f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 620f, 120f, 20f), LocalizationText.GetText("Country"));
			GUI.Label(new Rect(200f, 640f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 660f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 680f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 700f, 120f, 20f), "12");
			GUI.Label(new Rect(200f, 720f, 120f, 20f), LocalizationText.GetText("Name"));
			GUI.Label(new Rect(200f, 740f, 120f, 20f), LocalizationText.GetText("SurName"));
			GUI.Label(new Rect(200f, 760f, 120f, 20f), LocalizationText.GetText("BornCity"));
			GUI.TextArea(new Rect(20f, 780f, 280f, 110f), LocalizationText.GetText("PlayerText"));
		}
		if (GUI.Button(new Rect(10f, 280f, 100f, 20f), LocalizationText.GetText("lblPlayerStats")))
		{
			this.ShowPlayerStats = !this.ShowPlayerStats;
		}
		if (GUI.Button(new Rect(30f, 40f, 80f, 20f), LocalizationText.GetText("btnEnglish")))
		{
			LocalizationText.SetLanguage("EN");
		}
		if (GUI.Button(new Rect(30f, 70f, 80f, 20f), LocalizationText.GetText("btnGerman")))
		{
			LocalizationText.SetLanguage("DE");
		}
	}

	// Token: 0x0400022F RID: 559
	private bool ShowPlayerStats;
}
