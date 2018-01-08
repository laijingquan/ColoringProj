using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameAnalyticsSDK.Setup
{
	// Token: 0x02000037 RID: 55
	public class Settings : ScriptableObject
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000B8BC File Offset: 0x00009CBC
		public void SetCustomUserID(string customID)
		{
			if (customID != string.Empty)
			{
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000B8D0 File Offset: 0x00009CD0
		public void RemovePlatformAtIndex(int index)
		{
			if (index >= 0 && index < this.Platforms.Count)
			{
				this.gameKey.RemoveAt(index);
				this.secretKey.RemoveAt(index);
				this.Build.RemoveAt(index);
				this.SelectedPlatformStudio.RemoveAt(index);
				this.SelectedPlatformGame.RemoveAt(index);
				this.SelectedPlatformGameID.RemoveAt(index);
				this.SelectedStudio.RemoveAt(index);
				this.SelectedGame.RemoveAt(index);
				this.PlatformFoldOut.RemoveAt(index);
				this.Platforms.RemoveAt(index);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000B970 File Offset: 0x00009D70
		public void AddPlatform(RuntimePlatform platform)
		{
			this.gameKey.Add(string.Empty);
			this.secretKey.Add(string.Empty);
			this.Build.Add("0.1");
			this.SelectedPlatformStudio.Add(string.Empty);
			this.SelectedPlatformGame.Add(string.Empty);
			this.SelectedPlatformGameID.Add(-1);
			this.SelectedStudio.Add(0);
			this.SelectedGame.Add(0);
			this.PlatformFoldOut.Add(true);
			this.Platforms.Add(platform);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BA0C File Offset: 0x00009E0C
		public string[] GetAvailablePlatforms()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < Settings.AvailablePlatforms.Length; i++)
			{
				RuntimePlatform runtimePlatform = Settings.AvailablePlatforms[i];
				if (runtimePlatform == RuntimePlatform.IPhonePlayer)
				{
					if (!this.Platforms.Contains(RuntimePlatform.tvOS) && !this.Platforms.Contains(runtimePlatform))
					{
						list.Add(runtimePlatform.ToString());
					}
					else if (!this.Platforms.Contains(runtimePlatform))
					{
						list.Add(runtimePlatform.ToString());
					}
				}
				else if (runtimePlatform == RuntimePlatform.tvOS)
				{
					if (!this.Platforms.Contains(RuntimePlatform.IPhonePlayer) && !this.Platforms.Contains(runtimePlatform))
					{
						list.Add(runtimePlatform.ToString());
					}
					else if (!this.Platforms.Contains(runtimePlatform))
					{
						list.Add(runtimePlatform.ToString());
					}
				}
				else if (runtimePlatform == RuntimePlatform.MetroPlayerARM)
				{
					if (!this.Platforms.Contains(runtimePlatform))
					{
						list.Add("WSA");
					}
				}
				else if (!this.Platforms.Contains(runtimePlatform))
				{
					list.Add(runtimePlatform.ToString());
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BB68 File Offset: 0x00009F68
		public bool IsGameKeyValid(int index, string value)
		{
			bool result = true;
			for (int i = 0; i < this.Platforms.Count; i++)
			{
				if (index != i && value.Equals(this.gameKey[i]))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BBBC File Offset: 0x00009FBC
		public bool IsSecretKeyValid(int index, string value)
		{
			bool result = true;
			for (int i = 0; i < this.Platforms.Count; i++)
			{
				if (index != i && value.Equals(this.secretKey[i]))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BC10 File Offset: 0x0000A010
		public void UpdateGameKey(int index, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				bool flag = this.IsGameKeyValid(index, value);
				if (flag)
				{
					this.gameKey[index] = value;
				}
				else if (this.gameKey[index].Equals(value))
				{
					this.gameKey[index] = string.Empty;
				}
			}
			else
			{
				this.gameKey[index] = value;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BC84 File Offset: 0x0000A084
		public void UpdateSecretKey(int index, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				bool flag = this.IsSecretKeyValid(index, value);
				if (flag)
				{
					this.secretKey[index] = value;
				}
				else if (this.secretKey[index].Equals(value))
				{
					this.secretKey[index] = string.Empty;
				}
			}
			else
			{
				this.secretKey[index] = value;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BCF7 File Offset: 0x0000A0F7
		public string GetGameKey(int index)
		{
			return this.gameKey[index];
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BD05 File Offset: 0x0000A105
		public string GetSecretKey(int index)
		{
			return this.secretKey[index];
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000BD13 File Offset: 0x0000A113
		public void SetCustomArea(string customArea)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BD15 File Offset: 0x0000A115
		public void SetKeys(string gamekey, string secretkey)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000BD17 File Offset: 0x0000A117
		static Settings()
		{
			// Note: this type is marked as 'beforefieldinit'.
			RuntimePlatform[] array = new RuntimePlatform[9];
			//RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.$field-6E5E6830F0A82B8603B122C02111F18D3639978F).FieldHandle);
			Settings.AvailablePlatforms = array;
		}

		// Token: 0x0400010F RID: 271
		[HideInInspector]
		public static string VERSION = "3.10.0";

		// Token: 0x04000110 RID: 272
		[HideInInspector]
		public static bool CheckingForUpdates = false;

		// Token: 0x04000111 RID: 273
		public int TotalMessagesSubmitted;

		// Token: 0x04000112 RID: 274
		public int TotalMessagesFailed;

		// Token: 0x04000113 RID: 275
		public int DesignMessagesSubmitted;

		// Token: 0x04000114 RID: 276
		public int DesignMessagesFailed;

		// Token: 0x04000115 RID: 277
		public int QualityMessagesSubmitted;

		// Token: 0x04000116 RID: 278
		public int QualityMessagesFailed;

		// Token: 0x04000117 RID: 279
		public int ErrorMessagesSubmitted;

		// Token: 0x04000118 RID: 280
		public int ErrorMessagesFailed;

		// Token: 0x04000119 RID: 281
		public int BusinessMessagesSubmitted;

		// Token: 0x0400011A RID: 282
		public int BusinessMessagesFailed;

		// Token: 0x0400011B RID: 283
		public int UserMessagesSubmitted;

		// Token: 0x0400011C RID: 284
		public int UserMessagesFailed;

		// Token: 0x0400011D RID: 285
		public string CustomArea = string.Empty;

		// Token: 0x0400011E RID: 286
		[SerializeField]
		private List<string> gameKey = new List<string>();

		// Token: 0x0400011F RID: 287
		[SerializeField]
		private List<string> secretKey = new List<string>();

		// Token: 0x04000120 RID: 288
		[SerializeField]
		public List<string> Build = new List<string>();

		// Token: 0x04000121 RID: 289
		[SerializeField]
		public List<string> SelectedPlatformStudio = new List<string>();

		// Token: 0x04000122 RID: 290
		[SerializeField]
		public List<string> SelectedPlatformGame = new List<string>();

		// Token: 0x04000123 RID: 291
		[SerializeField]
		public List<int> SelectedPlatformGameID = new List<int>();

		// Token: 0x04000124 RID: 292
		[SerializeField]
		public List<int> SelectedStudio = new List<int>();

		// Token: 0x04000125 RID: 293
		[SerializeField]
		public List<int> SelectedGame = new List<int>();

		// Token: 0x04000126 RID: 294
		public string NewVersion = string.Empty;

		// Token: 0x04000127 RID: 295
		public string Changes = string.Empty;

		// Token: 0x04000128 RID: 296
		public bool SignUpOpen = true;

		// Token: 0x04000129 RID: 297
		public string FirstName = string.Empty;

		// Token: 0x0400012A RID: 298
		public string LastName = string.Empty;

		// Token: 0x0400012B RID: 299
		public string StudioName = string.Empty;

		// Token: 0x0400012C RID: 300
		public string GameName = string.Empty;

		// Token: 0x0400012D RID: 301
		public string PasswordConfirm = string.Empty;

		// Token: 0x0400012E RID: 302
		public bool EmailOptIn = true;

		// Token: 0x0400012F RID: 303
		public string EmailGA = string.Empty;

		// Token: 0x04000130 RID: 304
		[NonSerialized]
		public string PasswordGA = string.Empty;

		// Token: 0x04000131 RID: 305
		[NonSerialized]
		public string TokenGA = string.Empty;

		// Token: 0x04000132 RID: 306
		[NonSerialized]
		public string ExpireTime = string.Empty;

		// Token: 0x04000133 RID: 307
		[NonSerialized]
		public string LoginStatus = "Not logged in.";

		// Token: 0x04000134 RID: 308
		[NonSerialized]
		public bool JustSignedUp;

		// Token: 0x04000135 RID: 309
		[NonSerialized]
		public bool HideSignupWarning;

		// Token: 0x04000136 RID: 310
		public bool IntroScreen = true;

		// Token: 0x04000137 RID: 311
		[NonSerialized]
		public List<Studio> Studios;

		// Token: 0x04000138 RID: 312
		public bool InfoLogEditor = true;

		// Token: 0x04000139 RID: 313
		public bool InfoLogBuild = true;

		// Token: 0x0400013A RID: 314
		public bool VerboseLogBuild;

		// Token: 0x0400013B RID: 315
		public bool UseManualSessionHandling;

		// Token: 0x0400013C RID: 316
		public bool SendExampleGameDataToMyGame;

		// Token: 0x0400013D RID: 317
		public bool InternetConnectivity;

		// Token: 0x0400013E RID: 318
		public List<string> CustomDimensions01 = new List<string>();

		// Token: 0x0400013F RID: 319
		public List<string> CustomDimensions02 = new List<string>();

		// Token: 0x04000140 RID: 320
		public List<string> CustomDimensions03 = new List<string>();

		// Token: 0x04000141 RID: 321
		public List<string> ResourceItemTypes = new List<string>();

		// Token: 0x04000142 RID: 322
		public List<string> ResourceCurrencies = new List<string>();

		// Token: 0x04000143 RID: 323
		public RuntimePlatform LastCreatedGamePlatform;

		// Token: 0x04000144 RID: 324
		public List<RuntimePlatform> Platforms = new List<RuntimePlatform>();

		// Token: 0x04000145 RID: 325
		public Settings.InspectorStates CurrentInspectorState;

		// Token: 0x04000146 RID: 326
		public List<Settings.HelpTypes> ClosedHints = new List<Settings.HelpTypes>();

		// Token: 0x04000147 RID: 327
		public bool DisplayHints;

		// Token: 0x04000148 RID: 328
		public Vector2 DisplayHintsScrollState;

		// Token: 0x04000149 RID: 329
		public Texture2D Logo;

		// Token: 0x0400014A RID: 330
		public Texture2D UpdateIcon;

		// Token: 0x0400014B RID: 331
		public Texture2D InfoIcon;

		// Token: 0x0400014C RID: 332
		public Texture2D DeleteIcon;

		// Token: 0x0400014D RID: 333
		public Texture2D GameIcon;

		// Token: 0x0400014E RID: 334
		public Texture2D HomeIcon;

		// Token: 0x0400014F RID: 335
		public Texture2D InstrumentIcon;

		// Token: 0x04000150 RID: 336
		public Texture2D QuestionIcon;

		// Token: 0x04000151 RID: 337
		public Texture2D UserIcon;

		// Token: 0x04000152 RID: 338
		public Texture2D AmazonIcon;

		// Token: 0x04000153 RID: 339
		public Texture2D GooglePlayIcon;

		// Token: 0x04000154 RID: 340
		public Texture2D iosIcon;

		// Token: 0x04000155 RID: 341
		public Texture2D macIcon;

		// Token: 0x04000156 RID: 342
		public Texture2D windowsPhoneIcon;

		// Token: 0x04000157 RID: 343
		[NonSerialized]
		public GUIStyle SignupButton;

		// Token: 0x04000158 RID: 344
		public bool UseCustomId;

		// Token: 0x04000159 RID: 345
		public bool UsePlayerSettingsBundleVersion;

		// Token: 0x0400015A RID: 346
		public bool SubmitErrors = true;

		// Token: 0x0400015B RID: 347
		public int MaxErrorCount = 10;

		// Token: 0x0400015C RID: 348
		public bool SubmitFpsAverage = true;

		// Token: 0x0400015D RID: 349
		public bool SubmitFpsCritical = true;

		// Token: 0x0400015E RID: 350
		public bool IncludeGooglePlay = true;

		// Token: 0x0400015F RID: 351
		public int FpsCriticalThreshold = 20;

		// Token: 0x04000160 RID: 352
		public int FpsCirticalSubmitInterval = 1;

		// Token: 0x04000161 RID: 353
		public List<bool> PlatformFoldOut = new List<bool>();

		// Token: 0x04000162 RID: 354
		public bool CustomDimensions01FoldOut;

		// Token: 0x04000163 RID: 355
		public bool CustomDimensions02FoldOut;

		// Token: 0x04000164 RID: 356
		public bool CustomDimensions03FoldOut;

		// Token: 0x04000165 RID: 357
		public bool ResourceItemTypesFoldOut;

		// Token: 0x04000166 RID: 358
		public bool ResourceCurrenciesFoldOut;

		// Token: 0x04000167 RID: 359
		public static readonly RuntimePlatform[] AvailablePlatforms;

		// Token: 0x02000038 RID: 56
		public enum HelpTypes
		{
			// Token: 0x04000169 RID: 361
			None,
			// Token: 0x0400016A RID: 362
			IncludeSystemSpecsHelp,
			// Token: 0x0400016B RID: 363
			ProvideCustomUserID
		}

		// Token: 0x02000039 RID: 57
		public enum MessageTypes
		{
			// Token: 0x0400016D RID: 365
			None,
			// Token: 0x0400016E RID: 366
			Error,
			// Token: 0x0400016F RID: 367
			Info,
			// Token: 0x04000170 RID: 368
			Warning
		}

		// Token: 0x0200003A RID: 58
		public struct HelpInfo
		{
			// Token: 0x04000171 RID: 369
			public string Message;

			// Token: 0x04000172 RID: 370
			public Settings.MessageTypes MsgType;

			// Token: 0x04000173 RID: 371
			public Settings.HelpTypes HelpType;
		}

		// Token: 0x0200003B RID: 59
		public enum InspectorStates
		{
			// Token: 0x04000175 RID: 373
			Account,
			// Token: 0x04000176 RID: 374
			Basic,
			// Token: 0x04000177 RID: 375
			Debugging,
			// Token: 0x04000178 RID: 376
			Pref
		}
	}
}
