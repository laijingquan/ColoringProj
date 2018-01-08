using System;
using GameAnalyticsSDK.Wrapper;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x0200002E RID: 46
	public static class GA_Business
	{
		// Token: 0x06000138 RID: 312 RVA: 0x0000A994 File Offset: 0x00008D94
		public static void NewEventGooglePlay(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string signature)
		{
			GA_Wrapper.AddBusinessEventWithReceipt(currency, amount, itemType, itemId, cartType, receipt, "google_play", signature);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A9AA File Offset: 0x00008DAA
		public static void NewEvent(string currency, int amount, string itemType, string itemId, string cartType)
		{
			GA_Wrapper.AddBusinessEvent(currency, amount, itemType, itemId, cartType);
		}
	}
}
