using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnhancedScollerDemos.MainMenu
{
	// Token: 0x02000015 RID: 21
	public class MainMenu : MonoBehaviour
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00006516 File Offset: 0x00004916
		public void SceneButton_OnClick(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}
