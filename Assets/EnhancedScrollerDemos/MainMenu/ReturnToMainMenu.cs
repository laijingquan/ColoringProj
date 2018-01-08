using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnhancedScrollerDemos.MainMenu
{
	// Token: 0x02000016 RID: 22
	public class ReturnToMainMenu : MonoBehaviour
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00006526 File Offset: 0x00004926
		public void ReturnToMainMenuButton_OnClick()
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
