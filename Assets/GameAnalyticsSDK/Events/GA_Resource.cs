using System;
using GameAnalyticsSDK.Wrapper;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x02000033 RID: 51
	public static class GA_Resource
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000ABF7 File Offset: 0x00008FF7
		public static void NewEvent(GAResourceFlowType flowType, string currency, float amount, string itemType, string itemId)
		{
			GA_Wrapper.AddResourceEvent(flowType, currency, amount, itemType, itemId);
		}
	}
}
