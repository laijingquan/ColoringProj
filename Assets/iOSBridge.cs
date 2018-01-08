using System;
using System.Runtime.InteropServices;

// Token: 0x02000081 RID: 129
public class iOSBridge
{
	// Token: 0x0600058C RID: 1420
	[DllImport("__Internal")]
	private static extern void _AddNotification(string title, string body, string cancelLabel, string firstLabel, string secondLabel);

	// Token: 0x0600058D RID: 1421 RVA: 0x0001FD51 File Offset: 0x0001E151
	public static void AddNotification(string title, string body, string cancelLabel, string firstLabel, string secondLabel)
	{
		iOSBridge._AddNotification(title, body, cancelLabel, firstLabel, secondLabel);
	}

	// Token: 0x0600058E RID: 1422
	[DllImport("__Internal")]
	private static extern bool _CheckInterNetConnection();

	// Token: 0x0600058F RID: 1423 RVA: 0x0001FD5E File Offset: 0x0001E15E
	public static bool CheckInterNetConnection()
	{
		return iOSBridge._CheckInterNetConnection();
	}

	// Token: 0x06000590 RID: 1424
	[DllImport("__Internal")]
	private static extern void _AddSharing(string text, string url);

	// Token: 0x06000591 RID: 1425 RVA: 0x0001FD65 File Offset: 0x0001E165
	public static void AddSharing(string text, string url)
	{
		iOSBridge._AddSharing(text, url);
	}

	// Token: 0x06000592 RID: 1426
	[DllImport("__Internal")]
	private static extern void _TakeScreenShot();

	// Token: 0x06000593 RID: 1427 RVA: 0x0001FD6E File Offset: 0x0001E16E
	public static void TakeScreenShot()
	{
		iOSBridge._TakeScreenShot();
	}

	// Token: 0x06000594 RID: 1428
	[DllImport("__Internal")]
	private static extern void _NointerNetConnectionDialog();

	// Token: 0x06000595 RID: 1429 RVA: 0x0001FD75 File Offset: 0x0001E175
	public static void NointerNetConnectionDialog()
	{
		iOSBridge._NointerNetConnectionDialog();
	}
}
