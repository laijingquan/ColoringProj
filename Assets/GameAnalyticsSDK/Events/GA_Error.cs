using System;
using GameAnalyticsSDK.Wrapper;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x02000031 RID: 49
	public static class GA_Error
	{
		// Token: 0x06000141 RID: 321 RVA: 0x0000AB26 File Offset: 0x00008F26
		public static void NewEvent(GAErrorSeverity severity, string message)
		{
			GA_Error.CreateNewEvent(severity, message);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000AB2F File Offset: 0x00008F2F
		private static void CreateNewEvent(GAErrorSeverity severity, string message)
		{
			GA_Wrapper.AddErrorEvent(severity, message);
		}
	}
}
