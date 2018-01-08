using System;
using System.Collections;
using UnityEngine;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x02000035 RID: 53
	public class GA_SpecialEvents : MonoBehaviour
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000AD86 File Offset: 0x00009186
		public void Start()
		{
			base.StartCoroutine(this.SubmitFPSRoutine());
			base.StartCoroutine(this.CheckCriticalFPSRoutine());
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000ADA4 File Offset: 0x000091A4
		private IEnumerator SubmitFPSRoutine()
		{
			while (Application.isPlaying && GameAnalytics.SettingsGA.SubmitFpsAverage)
			{
				yield return new WaitForSeconds(30f);
				GA_SpecialEvents.SubmitFPS();
			}
			yield break;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000ADB8 File Offset: 0x000091B8
		private IEnumerator CheckCriticalFPSRoutine()
		{
			while (Application.isPlaying && GameAnalytics.SettingsGA.SubmitFpsCritical)
			{
				yield return new WaitForSeconds((float)GameAnalytics.SettingsGA.FpsCirticalSubmitInterval);
				this.CheckCriticalFPS();
			}
			yield break;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000ADD3 File Offset: 0x000091D3
		public void Update()
		{
			if (GameAnalytics.SettingsGA.SubmitFpsAverage)
			{
				GA_SpecialEvents._frameCountAvg++;
			}
			if (GameAnalytics.SettingsGA.SubmitFpsCritical)
			{
				this._frameCountCrit++;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AE10 File Offset: 0x00009210
		public static void SubmitFPS()
		{
			if (GameAnalytics.SettingsGA.SubmitFpsAverage)
			{
				float num = Time.time - GA_SpecialEvents._lastUpdateAvg;
				if (num > 1f)
				{
					float num2 = (float)GA_SpecialEvents._frameCountAvg / num;
					GA_SpecialEvents._lastUpdateAvg = Time.time;
					GA_SpecialEvents._frameCountAvg = 0;
					if (num2 > 0f)
					{
						GameAnalytics.NewDesignEvent("GA:AverageFPS", (float)((int)num2));
					}
				}
			}
			if (GameAnalytics.SettingsGA.SubmitFpsCritical && GA_SpecialEvents._criticalFpsCount > 0)
			{
				GameAnalytics.NewDesignEvent("GA:CriticalFPS", (float)GA_SpecialEvents._criticalFpsCount);
				GA_SpecialEvents._criticalFpsCount = 0;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AEA4 File Offset: 0x000092A4
		public void CheckCriticalFPS()
		{
			if (GameAnalytics.SettingsGA.SubmitFpsCritical)
			{
				float num = Time.time - this._lastUpdateCrit;
				if (num >= 1f)
				{
					float num2 = (float)this._frameCountCrit / num;
					this._lastUpdateCrit = Time.time;
					this._frameCountCrit = 0;
					if (num2 <= (float)GameAnalytics.SettingsGA.FpsCriticalThreshold)
					{
						GA_SpecialEvents._criticalFpsCount++;
					}
				}
			}
		}

		// Token: 0x04000107 RID: 263
		private static int _frameCountAvg;

		// Token: 0x04000108 RID: 264
		private static float _lastUpdateAvg;

		// Token: 0x04000109 RID: 265
		private int _frameCountCrit;

		// Token: 0x0400010A RID: 266
		private float _lastUpdateCrit;

		// Token: 0x0400010B RID: 267
		private static int _criticalFpsCount;
	}
}
