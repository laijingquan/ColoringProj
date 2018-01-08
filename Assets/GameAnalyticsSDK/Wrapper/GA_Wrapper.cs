using System;
using System.Collections;
using GameAnalyticsSDK.State;
using GameAnalyticsSDK.Utilities;
using UnityEngine;

namespace GameAnalyticsSDK.Wrapper
{
	// Token: 0x02000040 RID: 64
	public class GA_Wrapper
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000CBAC File Offset: 0x0000AFAC
		private static void configureAvailableCustomDimensions01(string list)
		{
			ArrayList arrayList = (ArrayList)GA_MiniJSON.JsonDecode(list);
			GA_Wrapper.GA.CallStatic("configureAvailableCustomDimensions01", new object[]
			{
				arrayList.ToArray(typeof(string))
			});
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000CBF0 File Offset: 0x0000AFF0
		private static void configureAvailableCustomDimensions02(string list)
		{
			ArrayList arrayList = (ArrayList)GA_MiniJSON.JsonDecode(list);
			GA_Wrapper.GA.CallStatic("configureAvailableCustomDimensions02", new object[]
			{
				arrayList.ToArray(typeof(string))
			});
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000CC34 File Offset: 0x0000B034
		private static void configureAvailableCustomDimensions03(string list)
		{
			ArrayList arrayList = (ArrayList)GA_MiniJSON.JsonDecode(list);
			GA_Wrapper.GA.CallStatic("configureAvailableCustomDimensions03", new object[]
			{
				arrayList.ToArray(typeof(string))
			});
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000CC78 File Offset: 0x0000B078
		private static void configureAvailableResourceCurrencies(string list)
		{
			ArrayList arrayList = (ArrayList)GA_MiniJSON.JsonDecode(list);
			GA_Wrapper.GA.CallStatic("configureAvailableResourceCurrencies", new object[]
			{
				arrayList.ToArray(typeof(string))
			});
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000CCBC File Offset: 0x0000B0BC
		private static void configureAvailableResourceItemTypes(string list)
		{
			ArrayList arrayList = (ArrayList)GA_MiniJSON.JsonDecode(list);
			GA_Wrapper.GA.CallStatic("configureAvailableResourceItemTypes", new object[]
			{
				arrayList.ToArray(typeof(string))
			});
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000CCFD File Offset: 0x0000B0FD
		private static void configureSdkGameEngineVersion(string unitySdkVersion)
		{
			GA_Wrapper.GA.CallStatic("configureSdkGameEngineVersion", new object[]
			{
				unitySdkVersion
			});
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000CD18 File Offset: 0x0000B118
		private static void configureGameEngineVersion(string unityEngineVersion)
		{
			GA_Wrapper.GA.CallStatic("configureGameEngineVersion", new object[]
			{
				unityEngineVersion
			});
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000CD33 File Offset: 0x0000B133
		private static void configureBuild(string build)
		{
			GA_Wrapper.GA.CallStatic("configureBuild", new object[]
			{
				build
			});
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CD4E File Offset: 0x0000B14E
		private static void configureUserId(string userId)
		{
			GA_Wrapper.GA.CallStatic("configureUserId", new object[]
			{
				userId
			});
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000CD6C File Offset: 0x0000B16C
		private static void initialize(string gamekey, string gamesecret)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.gameanalytics.sdk.GAPlatform");
			androidJavaClass2.CallStatic("initializeWithActivity", new object[]
			{
				@static
			});
			GA_Wrapper.GA.CallStatic("initializeWithGameKey", new object[]
			{
				gamekey,
				gamesecret
			});
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000CDCD File Offset: 0x0000B1CD
		private static void setCustomDimension01(string customDimension)
		{
			GA_Wrapper.GA.CallStatic("setCustomDimension01", new object[]
			{
				customDimension
			});
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000CDE8 File Offset: 0x0000B1E8
		private static void setCustomDimension02(string customDimension)
		{
			GA_Wrapper.GA.CallStatic("setCustomDimension02", new object[]
			{
				customDimension
			});
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000CE03 File Offset: 0x0000B203
		private static void setCustomDimension03(string customDimension)
		{
			GA_Wrapper.GA.CallStatic("setCustomDimension03", new object[]
			{
				customDimension
			});
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000CE1E File Offset: 0x0000B21E
		private static void addBusinessEvent(string currency, int amount, string itemType, string itemId, string cartType)
		{
			GA_Wrapper.GA.CallStatic("addBusinessEventWithCurrency", new object[]
			{
				currency,
				amount,
				itemType,
				itemId,
				cartType
			});
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000CE4F File Offset: 0x0000B24F
		private static void addBusinessEventWithReceipt(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string store, string signature)
		{
			GA_Wrapper.GA.CallStatic("addBusinessEventWithCurrency", new object[]
			{
				currency,
				amount,
				itemType,
				itemId,
				cartType,
				receipt,
				store,
				signature
			});
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000CE8F File Offset: 0x0000B28F
		private static void addResourceEvent(int flowType, string currency, float amount, string itemType, string itemId)
		{
			GA_Wrapper.GA.CallStatic("addResourceEventWithFlowType", new object[]
			{
				flowType,
				currency,
				amount,
				itemType,
				itemId
			});
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CEC5 File Offset: 0x0000B2C5
		private static void addProgressionEvent(int progressionStatus, string progression01, string progression02, string progression03)
		{
			GA_Wrapper.GA.CallStatic("addProgressionEventWithProgressionStatus", new object[]
			{
				progressionStatus,
				progression01,
				progression02,
				progression03
			});
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CEF1 File Offset: 0x0000B2F1
		private static void addProgressionEventWithScore(int progressionStatus, string progression01, string progression02, string progression03, int score)
		{
			GA_Wrapper.GA.CallStatic("addProgressionEventWithProgressionStatus", new object[]
			{
				progressionStatus,
				progression01,
				progression02,
				progression03,
				(double)score
			});
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000CF28 File Offset: 0x0000B328
		private static void addDesignEvent(string eventId)
		{
			GA_Wrapper.GA.CallStatic("addDesignEventWithEventId", new object[]
			{
				eventId
			});
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000CF43 File Offset: 0x0000B343
		private static void addDesignEventWithValue(string eventId, float value)
		{
			GA_Wrapper.GA.CallStatic("addDesignEventWithEventId", new object[]
			{
				eventId,
				(double)value
			});
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000CF68 File Offset: 0x0000B368
		private static void addErrorEvent(int severity, string message)
		{
			GA_Wrapper.GA.CallStatic("addErrorEventWithSeverity", new object[]
			{
				severity,
				message
			});
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000CF8C File Offset: 0x0000B38C
		private static void setEnabledInfoLog(bool enabled)
		{
			GA_Wrapper.GA.CallStatic("setEnabledInfoLog", new object[]
			{
				enabled
			});
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000CFAC File Offset: 0x0000B3AC
		private static void setEnabledVerboseLog(bool enabled)
		{
			GA_Wrapper.GA.CallStatic("setEnabledVerboseLog", new object[]
			{
				enabled
			});
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000CFCC File Offset: 0x0000B3CC
		private static void setFacebookId(string facebookId)
		{
			GA_Wrapper.GA.CallStatic("setFacebookId", new object[]
			{
				facebookId
			});
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000CFE8 File Offset: 0x0000B3E8
		private static void setGender(string gender)
		{
			if (gender != null)
			{
				if (!(gender == "male"))
				{
					if (gender == "female")
					{
						GA_Wrapper.GA.CallStatic("setGender", new object[]
						{
							2
						});
					}
				}
				else
				{
					GA_Wrapper.GA.CallStatic("setGender", new object[]
					{
						1
					});
				}
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D066 File Offset: 0x0000B466
		private static void setBirthYear(int birthYear)
		{
			GA_Wrapper.GA.CallStatic("setBirthYear", new object[]
			{
				birthYear
			});
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D086 File Offset: 0x0000B486
		private static void setManualSessionHandling(bool enabled)
		{
			GA_Wrapper.GA.CallStatic("setEnabledManualSessionHandling", new object[]
			{
				enabled
			});
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000D0A6 File Offset: 0x0000B4A6
		private static void gameAnalyticsStartSession()
		{
			GA_Wrapper.GA.CallStatic("startSession", new object[0]);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D0BD File Offset: 0x0000B4BD
		private static void gameAnalyticsEndSession()
		{
			GA_Wrapper.GA.CallStatic("endSession", new object[0]);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D0D4 File Offset: 0x0000B4D4
		public static void SetAvailableCustomDimensions01(string list)
		{
			GA_Wrapper.configureAvailableCustomDimensions01(list);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000D0DC File Offset: 0x0000B4DC
		public static void SetAvailableCustomDimensions02(string list)
		{
			GA_Wrapper.configureAvailableCustomDimensions02(list);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D0E4 File Offset: 0x0000B4E4
		public static void SetAvailableCustomDimensions03(string list)
		{
			GA_Wrapper.configureAvailableCustomDimensions03(list);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D0EC File Offset: 0x0000B4EC
		public static void SetAvailableResourceCurrencies(string list)
		{
			GA_Wrapper.configureAvailableResourceCurrencies(list);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D0F4 File Offset: 0x0000B4F4
		public static void SetAvailableResourceItemTypes(string list)
		{
			GA_Wrapper.configureAvailableResourceItemTypes(list);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D0FC File Offset: 0x0000B4FC
		public static void SetUnitySdkVersion(string unitySdkVersion)
		{
			GA_Wrapper.configureSdkGameEngineVersion(unitySdkVersion);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D104 File Offset: 0x0000B504
		public static void SetUnityEngineVersion(string unityEngineVersion)
		{
			GA_Wrapper.configureGameEngineVersion(unityEngineVersion);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D10C File Offset: 0x0000B50C
		public static void SetBuild(string build)
		{
			GA_Wrapper.configureBuild(build);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D114 File Offset: 0x0000B514
		public static void SetCustomUserId(string userId)
		{
			GA_Wrapper.configureUserId(userId);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D11C File Offset: 0x0000B51C
		public static void SetEnabledManualSessionHandling(bool enabled)
		{
			GA_Wrapper.setManualSessionHandling(enabled);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D124 File Offset: 0x0000B524
		public static void StartSession()
		{
			if (GAState.IsManualSessionHandlingEnabled())
			{
				GA_Wrapper.gameAnalyticsStartSession();
			}
			else
			{
				Debug.Log("Manual session handling is not enabled. \nPlease check the \"Use manual session handling\" option in the \"Advanced\" section of the Settings object.");
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000D144 File Offset: 0x0000B544
		public static void EndSession()
		{
			if (GAState.IsManualSessionHandlingEnabled())
			{
				GA_Wrapper.gameAnalyticsEndSession();
			}
			else
			{
				Debug.Log("Manual session handling is not enabled. \nPlease check the \"Use manual session handling\" option in the \"Advanced\" section of the Settings object.");
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D164 File Offset: 0x0000B564
		public static void Initialize(string gamekey, string gamesecret)
		{
			GA_Wrapper.initialize(gamekey, gamesecret);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D16D File Offset: 0x0000B56D
		public static void SetCustomDimension01(string customDimension)
		{
			GA_Wrapper.setCustomDimension01(customDimension);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D175 File Offset: 0x0000B575
		public static void SetCustomDimension02(string customDimension)
		{
			GA_Wrapper.setCustomDimension02(customDimension);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D17D File Offset: 0x0000B57D
		public static void SetCustomDimension03(string customDimension)
		{
			GA_Wrapper.setCustomDimension03(customDimension);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D185 File Offset: 0x0000B585
		public static void AddBusinessEventWithReceipt(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string store, string signature)
		{
			GA_Wrapper.addBusinessEventWithReceipt(currency, amount, itemType, itemId, cartType, receipt, store, signature);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D198 File Offset: 0x0000B598
		public static void AddBusinessEvent(string currency, int amount, string itemType, string itemId, string cartType)
		{
			GA_Wrapper.addBusinessEvent(currency, amount, itemType, itemId, cartType);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D1A5 File Offset: 0x0000B5A5
		public static void AddResourceEvent(GAResourceFlowType flowType, string currency, float amount, string itemType, string itemId)
		{
			GA_Wrapper.addResourceEvent((int)flowType, currency, amount, itemType, itemId);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D1B2 File Offset: 0x0000B5B2
		public static void AddProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
		{
			GA_Wrapper.addProgressionEvent((int)progressionStatus, progression01, progression02, progression03);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000D1BD File Offset: 0x0000B5BD
		public static void AddProgressionEventWithScore(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
		{
			GA_Wrapper.addProgressionEventWithScore((int)progressionStatus, progression01, progression02, progression03, score);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D1CA File Offset: 0x0000B5CA
		public static void AddDesignEvent(string eventID, float eventValue)
		{
			GA_Wrapper.addDesignEventWithValue(eventID, eventValue);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D1D3 File Offset: 0x0000B5D3
		public static void AddDesignEvent(string eventID)
		{
			GA_Wrapper.addDesignEvent(eventID);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D1DB File Offset: 0x0000B5DB
		public static void AddErrorEvent(GAErrorSeverity severity, string message)
		{
			GA_Wrapper.addErrorEvent((int)severity, message);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D1E4 File Offset: 0x0000B5E4
		public static void SetInfoLog(bool enabled)
		{
			GA_Wrapper.setEnabledInfoLog(enabled);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000D1EC File Offset: 0x0000B5EC
		public static void SetVerboseLog(bool enabled)
		{
			GA_Wrapper.setEnabledVerboseLog(enabled);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000D1F4 File Offset: 0x0000B5F4
		public static void SetFacebookId(string facebookId)
		{
			GA_Wrapper.setFacebookId(facebookId);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000D1FC File Offset: 0x0000B5FC
		public static void SetGender(string gender)
		{
			GA_Wrapper.setGender(gender);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000D204 File Offset: 0x0000B604
		public static void SetBirthYear(int birthYear)
		{
			GA_Wrapper.setBirthYear(birthYear);
		}

		// Token: 0x04000191 RID: 401
		private static readonly AndroidJavaClass GA = new AndroidJavaClass("com.gameanalytics.sdk.GameAnalytics");
	}
}
