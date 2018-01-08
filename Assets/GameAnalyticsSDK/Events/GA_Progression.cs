using System;
using GameAnalyticsSDK.Wrapper;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x02000032 RID: 50
	public static class GA_Progression
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000AB38 File Offset: 0x00008F38
		public static void NewEvent(GAProgressionStatus progressionStatus, string progression01)
		{
			GA_Progression.CreateEvent(progressionStatus, progression01, null, null, null);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AB58 File Offset: 0x00008F58
		public static void NewEvent(GAProgressionStatus progressionStatus, string progression01, string progression02)
		{
			GA_Progression.CreateEvent(progressionStatus, progression01, progression02, null, null);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AB78 File Offset: 0x00008F78
		public static void NewEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
		{
			GA_Progression.CreateEvent(progressionStatus, progression01, progression02, progression03, null);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AB97 File Offset: 0x00008F97
		public static void NewEvent(GAProgressionStatus progressionStatus, string progression01, int score)
		{
			GA_Progression.CreateEvent(progressionStatus, progression01, null, null, new int?(score));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000ABA8 File Offset: 0x00008FA8
		public static void NewEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, int score)
		{
			GA_Progression.CreateEvent(progressionStatus, progression01, progression02, null, new int?(score));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000ABB9 File Offset: 0x00008FB9
		public static void NewEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
		{
			GA_Progression.CreateEvent(progressionStatus, progression01, progression02, progression03, new int?(score));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000ABCB File Offset: 0x00008FCB
		private static void CreateEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int? score)
		{
			if (score != null)
			{
				GA_Wrapper.AddProgressionEventWithScore(progressionStatus, progression01, progression02, progression03, score.Value);
			}
			else
			{
				GA_Wrapper.AddProgressionEvent(progressionStatus, progression01, progression02, progression03);
			}
		}
	}
}
