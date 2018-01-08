using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x0200002F RID: 47
	public static class GA_Debug
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000A9B8 File Offset: 0x00008DB8
		public static void HandleLog(string logString, string stackTrace, LogType type)
		{
			if (GA_Debug._showLogOnGUI)
			{
				if (GA_Debug.Messages == null)
				{
					GA_Debug.Messages = new List<string>();
				}
				GA_Debug.Messages.Add(logString);
			}
			if (GameAnalytics.SettingsGA.SubmitErrors && GA_Debug._errorCount < GA_Debug.MaxErrorCount && type != LogType.Log)
			{
				GA_Debug._errorCount++;
				string str = logString.Replace('"', '\'').Replace('\n', ' ').Replace('\r', ' ');
				string str2 = stackTrace.Replace('"', '\'').Replace('\n', ' ').Replace('\r', ' ');
				GA_Debug.SubmitError(str + " " + str2, type);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000AA6C File Offset: 0x00008E6C
		private static void SubmitError(string message, LogType type)
		{
			GAErrorSeverity severity = GAErrorSeverity.Info;
			switch (type)
			{
			case LogType.Error:
				severity = GAErrorSeverity.Error;
				break;
			case LogType.Assert:
				severity = GAErrorSeverity.Info;
				break;
			case LogType.Warning:
				severity = GAErrorSeverity.Warning;
				break;
			case LogType.Log:
				severity = GAErrorSeverity.Debug;
				break;
			case LogType.Exception:
				severity = GAErrorSeverity.Critical;
				break;
			}
			GA_Error.NewEvent(severity, message);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000AAC4 File Offset: 0x00008EC4
		public static void EnabledLog()
		{
			GA_Debug._showLogOnGUI = true;
		}

		// Token: 0x04000103 RID: 259
		public static int MaxErrorCount = 10;

		// Token: 0x04000104 RID: 260
		private static int _errorCount;

		// Token: 0x04000105 RID: 261
		private static bool _showLogOnGUI;

		// Token: 0x04000106 RID: 262
		public static List<string> Messages;
	}
}
