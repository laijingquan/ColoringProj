using System;
using GameAnalyticsSDK.Wrapper;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x02000030 RID: 48
	public static class GA_Design
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000AAD5 File Offset: 0x00008ED5
		public static void NewEvent(string eventName, float eventValue)
		{
			GA_Design.CreateNewEvent(eventName, new float?(eventValue));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000AAE4 File Offset: 0x00008EE4
		public static void NewEvent(string eventName)
		{
			GA_Design.CreateNewEvent(eventName, null);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000AB00 File Offset: 0x00008F00
		private static void CreateNewEvent(string eventName, float? eventValue)
		{
			if (eventValue != null)
			{
				GA_Wrapper.AddDesignEvent(eventName, eventValue.Value);
			}
			else
			{
				GA_Wrapper.AddDesignEvent(eventName);
			}
		}
	}
}
