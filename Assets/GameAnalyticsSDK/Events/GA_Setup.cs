using System;
using System.Collections.Generic;
using GameAnalyticsSDK.Utilities;
using GameAnalyticsSDK.Validators;
using GameAnalyticsSDK.Wrapper;

namespace GameAnalyticsSDK.Events
{
	// Token: 0x02000034 RID: 52
	public static class GA_Setup
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000AC04 File Offset: 0x00009004
		public static void SetAvailableCustomDimensions01(List<string> customDimensions)
		{
			if (GAValidator.ValidateCustomDimensions(customDimensions.ToArray()))
			{
				string availableCustomDimensions = GA_MiniJSON.JsonEncode(customDimensions.ToArray());
				GA_Wrapper.SetAvailableCustomDimensions01(availableCustomDimensions);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AC34 File Offset: 0x00009034
		public static void SetAvailableCustomDimensions02(List<string> customDimensions)
		{
			if (GAValidator.ValidateCustomDimensions(customDimensions.ToArray()))
			{
				string availableCustomDimensions = GA_MiniJSON.JsonEncode(customDimensions.ToArray());
				GA_Wrapper.SetAvailableCustomDimensions02(availableCustomDimensions);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AC64 File Offset: 0x00009064
		public static void SetAvailableCustomDimensions03(List<string> customDimensions)
		{
			if (GAValidator.ValidateCustomDimensions(customDimensions.ToArray()))
			{
				string availableCustomDimensions = GA_MiniJSON.JsonEncode(customDimensions.ToArray());
				GA_Wrapper.SetAvailableCustomDimensions03(availableCustomDimensions);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AC94 File Offset: 0x00009094
		public static void SetAvailableResourceCurrencies(List<string> resourceCurrencies)
		{
			if (GAValidator.ValidateResourceCurrencies(resourceCurrencies.ToArray()))
			{
				string availableResourceCurrencies = GA_MiniJSON.JsonEncode(resourceCurrencies.ToArray());
				GA_Wrapper.SetAvailableResourceCurrencies(availableResourceCurrencies);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000ACC4 File Offset: 0x000090C4
		public static void SetAvailableResourceItemTypes(List<string> resourceItemTypes)
		{
			if (GAValidator.ValidateResourceItemTypes(resourceItemTypes.ToArray()))
			{
				string availableResourceItemTypes = GA_MiniJSON.JsonEncode(resourceItemTypes.ToArray());
				GA_Wrapper.SetAvailableResourceItemTypes(availableResourceItemTypes);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000ACF3 File Offset: 0x000090F3
		public static void SetInfoLog(bool enabled)
		{
			GA_Wrapper.SetInfoLog(enabled);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000ACFB File Offset: 0x000090FB
		public static void SetVerboseLog(bool enabled)
		{
			GA_Wrapper.SetVerboseLog(enabled);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000AD03 File Offset: 0x00009103
		public static void SetFacebookId(string facebookId)
		{
			GA_Wrapper.SetFacebookId(facebookId);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000AD0C File Offset: 0x0000910C
		public static void SetGender(GAGender gender)
		{
			if (gender != GAGender.male)
			{
				if (gender == GAGender.female)
				{
					GA_Wrapper.SetGender(GAGender.female.ToString());
				}
			}
			else
			{
				GA_Wrapper.SetGender(GAGender.male.ToString());
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000AD5E File Offset: 0x0000915E
		public static void SetBirthYear(int birthYear)
		{
			GA_Wrapper.SetBirthYear(birthYear);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000AD66 File Offset: 0x00009166
		public static void SetCustomDimension01(string customDimension)
		{
			GA_Wrapper.SetCustomDimension01(customDimension);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000AD6E File Offset: 0x0000916E
		public static void SetCustomDimension02(string customDimension)
		{
			GA_Wrapper.SetCustomDimension02(customDimension);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AD76 File Offset: 0x00009176
		public static void SetCustomDimension03(string customDimension)
		{
			GA_Wrapper.SetCustomDimension03(customDimension);
		}
	}
}
