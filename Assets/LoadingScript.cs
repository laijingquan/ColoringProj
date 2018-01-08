using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000066 RID: 102
public class LoadingScript : MonoBehaviour
{
	// Token: 0x060003B2 RID: 946 RVA: 0x000109F4 File Offset: 0x0000EDF4
	private void Start()
	{
		this.ChangeScene("MainScene 1");
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00010A01 File Offset: 0x0000EE01
	private void Update()
	{
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00010A03 File Offset: 0x0000EE03
	public void ChangeScene(string sceneName)
	{
		this.loadingText.text = "LOADING...";
		base.StartCoroutine(this.LoadingSceneRealProgress(sceneName));
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00010A24 File Offset: 0x0000EE24
	private IEnumerator LoadingSceneRealProgress(string sceneName)
	{
		yield return new WaitForSeconds(1f);
		this.sceneAO = SceneManager.LoadSceneAsync(sceneName);
		this.sceneAO.allowSceneActivation = false;
		while (!this.sceneAO.isDone)
		{
			this.loadingProgbar.value = this.sceneAO.progress;
			if (this.sceneAO.progress >= 0.9f)
			{
				this.loadingProgbar.value = 1f;
				this.sceneAO.allowSceneActivation = true;
			}
			Debug.Log(this.sceneAO.progress);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400022B RID: 555
	private AsyncOperation sceneAO;

	// Token: 0x0400022C RID: 556
	[SerializeField]
	private Slider loadingProgbar;

	// Token: 0x0400022D RID: 557
	[SerializeField]
	private Text loadingText;

	// Token: 0x0400022E RID: 558
	private const float LOAD_READY_PERCENTAGE = 0.9f;
}
