using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using GameAnalyticsSDK.Events;
using GameAnalyticsSDK.Setup;
using GameAnalyticsSDK.State;
using GameAnalyticsSDK.Wrapper;
using UnityEngine;

namespace GameAnalyticsSDK
{
	// Token: 0x02000036 RID: 54
	[RequireComponent(typeof(GA_SpecialEvents))]
	[ExecuteInEditMode]
	public class GameAnalytics : MonoBehaviour
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000B086 File Offset: 0x00009486
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000B0A2 File Offset: 0x000094A2
		public static Settings SettingsGA
		{
			get
			{
				if (GameAnalytics._settings == null)
				{
					GameAnalytics.InitAPI();
				}
				return GameAnalytics._settings;
			}
			private set
			{
				GameAnalytics._settings = value;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B0AC File Offset: 0x000094AC
		public void Awake()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (GameAnalytics._instance != null)
			{
				Debug.LogWarning("Destroying duplicate GameAnalytics object - only one is allowed per scene!");
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			GameAnalytics._instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			//if (GameAnalytics.<>f__mg$cache0 == null)
			//{
			//	GameAnalytics.<>f__mg$cache0 = new Application.LogCallback(GA_Debug.HandleLog);
			//}
			//Application.logMessageReceived += GameAnalytics.<>f__mg$cache0;
			GameAnalytics.Initialize();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B122 File Offset: 0x00009522
		private void OnDestroy()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (GameAnalytics._instance == this)
			{
				GameAnalytics._instance = null;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B148 File Offset: 0x00009548
		private void OnApplicationPause(bool pauseStatus)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.gameanalytics.sdk.GAPlatform");
			if (pauseStatus)
			{
				androidJavaClass2.CallStatic("onActivityPaused", new object[]
				{
					@static
				});
			}
			else
			{
				androidJavaClass2.CallStatic("onActivityResumed", new object[]
				{
					@static
				});
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B1AC File Offset: 0x000095AC
		private void OnApplicationQuit()
		{
			if (!GameAnalytics.SettingsGA.UseManualSessionHandling)
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.gameanalytics.sdk.GAPlatform");
				androidJavaClass2.CallStatic("onActivityStopped", new object[]
				{
					@static
				});
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B200 File Offset: 0x00009600
		private static void InitAPI()
		{
			try
			{
				GameAnalytics._settings = (Settings)Resources.Load("GameAnalytics/Settings", typeof(Settings));
				GAState.Init();
			}
			catch (Exception ex)
			{
				Debug.Log("Error getting Settings in InitAPI: " + ex.Message);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B260 File Offset: 0x00009660
		private static void Initialize()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (GameAnalytics.SettingsGA.InfoLogBuild)
			{
				GA_Setup.SetInfoLog(true);
			}
			if (GameAnalytics.SettingsGA.VerboseLogBuild)
			{
				GA_Setup.SetVerboseLog(true);
			}
			int platformIndex = GameAnalytics.GetPlatformIndex();
			GA_Wrapper.SetUnitySdkVersion("unity " + Settings.VERSION);
			GA_Wrapper.SetUnityEngineVersion("unity " + GameAnalytics.GetUnityVersion());
			if (platformIndex >= 0)
			{
				GA_Wrapper.SetBuild(GameAnalytics.SettingsGA.Build[platformIndex]);
			}
			if (GameAnalytics.SettingsGA.CustomDimensions01.Count > 0)
			{
				GA_Setup.SetAvailableCustomDimensions01(GameAnalytics.SettingsGA.CustomDimensions01);
			}
			if (GameAnalytics.SettingsGA.CustomDimensions02.Count > 0)
			{
				GA_Setup.SetAvailableCustomDimensions02(GameAnalytics.SettingsGA.CustomDimensions02);
			}
			if (GameAnalytics.SettingsGA.CustomDimensions03.Count > 0)
			{
				GA_Setup.SetAvailableCustomDimensions03(GameAnalytics.SettingsGA.CustomDimensions03);
			}
			if (GameAnalytics.SettingsGA.ResourceItemTypes.Count > 0)
			{
				GA_Setup.SetAvailableResourceItemTypes(GameAnalytics.SettingsGA.ResourceItemTypes);
			}
			if (GameAnalytics.SettingsGA.ResourceCurrencies.Count > 0)
			{
				GA_Setup.SetAvailableResourceCurrencies(GameAnalytics.SettingsGA.ResourceCurrencies);
			}
			if (GameAnalytics.SettingsGA.UseManualSessionHandling)
			{
				GameAnalytics.SetEnabledManualSessionHandling(true);
			}
			if (platformIndex >= 0)
			{
				if (!GameAnalytics.SettingsGA.UseCustomId)
				{
					GA_Wrapper.Initialize(GameAnalytics.SettingsGA.GetGameKey(platformIndex), GameAnalytics.SettingsGA.GetSecretKey(platformIndex));
				}
				else
				{
					Debug.Log("Custom id is enabled. Initialize is delayed until custom id has been set.");
				}
			}
			else
			{
				Debug.LogWarning("GameAnalytics: Unsupported platform (events will not be sent in editor; or missing platform in settings): " + Application.platform);
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B413 File Offset: 0x00009813
		public static void NewBusinessEvent(string currency, int amount, string itemType, string itemId, string cartType)
		{
			GA_Business.NewEvent(currency, amount, itemType, itemId, cartType);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B420 File Offset: 0x00009820
		public static void NewBusinessEventGooglePlay(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string signature)
		{
			GA_Business.NewEventGooglePlay(currency, amount, itemType, itemId, cartType, receipt, signature);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B431 File Offset: 0x00009831
		public static void NewDesignEvent(string eventName)
		{
			GA_Design.NewEvent(eventName);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B439 File Offset: 0x00009839
		public static void NewDesignEvent(string eventName, float eventValue)
		{
			GA_Design.NewEvent(eventName, eventValue);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B442 File Offset: 0x00009842
		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01)
		{
			GA_Progression.NewEvent(progressionStatus, progression01);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B44B File Offset: 0x0000984B
		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02)
		{
			GA_Progression.NewEvent(progressionStatus, progression01, progression02);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B455 File Offset: 0x00009855
		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
		{
			GA_Progression.NewEvent(progressionStatus, progression01, progression02, progression03);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B460 File Offset: 0x00009860
		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, int score)
		{
			GA_Progression.NewEvent(progressionStatus, progression01, score);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B46A File Offset: 0x0000986A
		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, int score)
		{
			GA_Progression.NewEvent(progressionStatus, progression01, progression02, score);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B475 File Offset: 0x00009875
		public static void NewProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
		{
			GA_Progression.NewEvent(progressionStatus, progression01, progression02, progression03, score);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B482 File Offset: 0x00009882
		public static void NewResourceEvent(GAResourceFlowType flowType, string currency, float amount, string itemType, string itemId)
		{
			GA_Resource.NewEvent(flowType, currency, amount, itemType, itemId);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B48F File Offset: 0x0000988F
		public static void NewErrorEvent(GAErrorSeverity severity, string message)
		{
			GA_Error.NewEvent(severity, message);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000B498 File Offset: 0x00009898
		public static void SetFacebookId(string facebookId)
		{
			GA_Setup.SetFacebookId(facebookId);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000B4A0 File Offset: 0x000098A0
		public static void SetGender(GAGender gender)
		{
			GA_Setup.SetGender(gender);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000B4A8 File Offset: 0x000098A8
		public static void SetBirthYear(int birthYear)
		{
			GA_Setup.SetBirthYear(birthYear);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000B4B0 File Offset: 0x000098B0
		public static void SetCustomId(string userId)
		{
			if (GameAnalytics.SettingsGA.UseCustomId)
			{
				Debug.Log("Initializing with custom id: " + userId);
				GA_Wrapper.SetCustomUserId(userId);
				int platformIndex = GameAnalytics.GetPlatformIndex();
				if (platformIndex >= 0)
				{
					GA_Wrapper.Initialize(GameAnalytics.SettingsGA.GetGameKey(platformIndex), GameAnalytics.SettingsGA.GetSecretKey(platformIndex));
				}
				else
				{
					Debug.LogWarning("Unsupported platform (or missing platform in settings): " + Application.platform);
				}
			}
			else
			{
				Debug.LogWarning("Custom id is not enabled");
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B537 File Offset: 0x00009937
		public static void SetEnabledManualSessionHandling(bool enabled)
		{
			GA_Wrapper.SetEnabledManualSessionHandling(enabled);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B53F File Offset: 0x0000993F
		public static void StartSession()
		{
			GA_Wrapper.StartSession();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B546 File Offset: 0x00009946
		public static void EndSession()
		{
			GA_Wrapper.EndSession();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B54D File Offset: 0x0000994D
		public static void SetCustomDimension01(string customDimension)
		{
			GA_Setup.SetCustomDimension01(customDimension);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B555 File Offset: 0x00009955
		public static void SetCustomDimension02(string customDimension)
		{
			GA_Setup.SetCustomDimension02(customDimension);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B55D File Offset: 0x0000995D
		public static void SetCustomDimension03(string customDimension)
		{
			GA_Setup.SetCustomDimension03(customDimension);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B568 File Offset: 0x00009968
		private static string GetUnityVersion()
		{
			string text = string.Empty;
			string[] array = Application.unityVersion.Split(new char[]
			{
				'.'
			});
			for (int i = 0; i < array.Length; i++)
			{
				int num;
				if (int.TryParse(array[i], out num))
				{
					if (i == 0)
					{
						text = array[i];
					}
					else
					{
						text = text + "." + array[i];
					}
				}
				else
				{
					string[] array2 = Regex.Split(array[i], "[^\\d]+");
					if (array2.Length > 0 && int.TryParse(array2[0], out num))
					{
						text = text + "." + array2[0];
					}
				}
			}
			return text;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B614 File Offset: 0x00009A14
		private static int GetPlatformIndex()
		{
			RuntimePlatform platform = Application.platform;
			int result;
			if (platform == RuntimePlatform.IPhonePlayer)
			{
				if (!GameAnalytics.SettingsGA.Platforms.Contains(platform))
				{
					result = GameAnalytics.SettingsGA.Platforms.IndexOf(RuntimePlatform.tvOS);
				}
				else
				{
					result = GameAnalytics.SettingsGA.Platforms.IndexOf(platform);
				}
			}
			else if (platform == RuntimePlatform.tvOS)
			{
				if (!GameAnalytics.SettingsGA.Platforms.Contains(platform))
				{
					result = GameAnalytics.SettingsGA.Platforms.IndexOf(RuntimePlatform.IPhonePlayer);
				}
				else
				{
					result = GameAnalytics.SettingsGA.Platforms.IndexOf(platform);
				}
			}
			else if (platform == RuntimePlatform.MetroPlayerARM || platform == RuntimePlatform.MetroPlayerX64 || platform == RuntimePlatform.MetroPlayerX86 || platform == RuntimePlatform.MetroPlayerARM || platform == RuntimePlatform.MetroPlayerX64 || platform == RuntimePlatform.MetroPlayerX86)
			{
				result = GameAnalytics.SettingsGA.Platforms.IndexOf(RuntimePlatform.MetroPlayerARM);
			}
			else
			{
				result = GameAnalytics.SettingsGA.Platforms.IndexOf(platform);
			}
			return result;
		}

		// Token: 0x0400010C RID: 268
		private static Settings _settings;

		// Token: 0x0400010D RID: 269
		private static GameAnalytics _instance;

		// Token: 0x0400010E RID: 270
		//[CompilerGenerated]
		//private static Application.LogCallback <>f__mg$cache0;
	}
}
