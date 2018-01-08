using System;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;

namespace GameAnalyticsSDK.State
{
	// Token: 0x0200003E RID: 62
	internal static class GAState
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000BF7C File Offset: 0x0000A37C
		public static void Init()
		{
			try
			{
				GAState._settings = (Settings)Resources.Load("GameAnalytics/Settings", typeof(Settings));
			}
			catch (Exception ex)
			{
				Debug.Log("Could not get Settings during event validation \n" + ex.ToString());
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000BFD8 File Offset: 0x0000A3D8
		private static bool ListContainsString(List<string> _list, string _string)
		{
			return _list.Contains(_string);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000BFE9 File Offset: 0x0000A3E9
		public static bool IsManualSessionHandlingEnabled()
		{
			return GAState._settings.UseManualSessionHandling;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000BFF5 File Offset: 0x0000A3F5
		public static bool HasAvailableResourceCurrency(string _currency)
		{
			return GAState.ListContainsString(GAState._settings.ResourceCurrencies, _currency);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C00F File Offset: 0x0000A40F
		public static bool HasAvailableResourceItemType(string _itemType)
		{
			return GAState.ListContainsString(GAState._settings.ResourceItemTypes, _itemType);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C029 File Offset: 0x0000A429
		public static bool HasAvailableCustomDimensions01(string _dimension01)
		{
			return GAState.ListContainsString(GAState._settings.CustomDimensions01, _dimension01);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000C043 File Offset: 0x0000A443
		public static bool HasAvailableCustomDimensions02(string _dimension02)
		{
			return GAState.ListContainsString(GAState._settings.CustomDimensions02, _dimension02);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000C05D File Offset: 0x0000A45D
		public static bool HasAvailableCustomDimensions03(string _dimension03)
		{
			return GAState.ListContainsString(GAState._settings.CustomDimensions03, _dimension03);
		}

		// Token: 0x04000180 RID: 384
		private static Settings _settings;
	}
}
