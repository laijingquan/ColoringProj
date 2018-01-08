using System;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class GlobalValues : MonoBehaviour
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001274A File Offset: 0x00010B4A
	// (set) Token: 0x0600040D RID: 1037 RVA: 0x00012757 File Offset: 0x00010B57
	public static int removeAds
	{
		get
		{
			return PlayerPrefs.GetInt("removeAds", 0);
		}
		set
		{
			PlayerPrefs.SetInt("removeAds", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600040E RID: 1038 RVA: 0x00012769 File Offset: 0x00010B69
	// (set) Token: 0x0600040F RID: 1039 RVA: 0x00012776 File Offset: 0x00010B76
	public static int unlockAll
	{
		get
		{
			return PlayerPrefs.GetInt("unlockAll", 0);
		}
		set
		{
			PlayerPrefs.SetInt("unlockAll", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000410 RID: 1040 RVA: 0x00012788 File Offset: 0x00010B88
	// (set) Token: 0x06000411 RID: 1041 RVA: 0x00012795 File Offset: 0x00010B95
	public static int like
	{
		get
		{
			return PlayerPrefs.GetInt("like", 0);
		}
		set
		{
			PlayerPrefs.SetInt("like", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000412 RID: 1042 RVA: 0x000127A7 File Offset: 0x00010BA7
	// (set) Token: 0x06000413 RID: 1043 RVA: 0x000127B4 File Offset: 0x00010BB4
	public static int rate
	{
		get
		{
			return PlayerPrefs.GetInt("rate", 0);
		}
		set
		{
			PlayerPrefs.SetInt("rate", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06000414 RID: 1044 RVA: 0x000127C6 File Offset: 0x00010BC6
	// (set) Token: 0x06000415 RID: 1045 RVA: 0x000127D3 File Offset: 0x00010BD3
	public static int cat1_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat1_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat1_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06000416 RID: 1046 RVA: 0x000127E5 File Offset: 0x00010BE5
	// (set) Token: 0x06000417 RID: 1047 RVA: 0x000127F2 File Offset: 0x00010BF2
	public static int cat2_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat2_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat2_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000418 RID: 1048 RVA: 0x00012804 File Offset: 0x00010C04
	// (set) Token: 0x06000419 RID: 1049 RVA: 0x00012811 File Offset: 0x00010C11
	public static int cat3_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat3_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat3_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600041A RID: 1050 RVA: 0x00012823 File Offset: 0x00010C23
	// (set) Token: 0x0600041B RID: 1051 RVA: 0x00012830 File Offset: 0x00010C30
	public static int cat4_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat4_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat4_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x0600041C RID: 1052 RVA: 0x00012842 File Offset: 0x00010C42
	// (set) Token: 0x0600041D RID: 1053 RVA: 0x0001284F File Offset: 0x00010C4F
	public static int cat5_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat5_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat5_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600041E RID: 1054 RVA: 0x00012861 File Offset: 0x00010C61
	// (set) Token: 0x0600041F RID: 1055 RVA: 0x0001286E File Offset: 0x00010C6E
	public static int cat6_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat6_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat6_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000420 RID: 1056 RVA: 0x00012880 File Offset: 0x00010C80
	// (set) Token: 0x06000421 RID: 1057 RVA: 0x0001288D File Offset: 0x00010C8D
	public static int cat7_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat7_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat7_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001289F File Offset: 0x00010C9F
	// (set) Token: 0x06000423 RID: 1059 RVA: 0x000128AC File Offset: 0x00010CAC
	public static int cat8_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat8_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat8_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x000128BE File Offset: 0x00010CBE
	// (set) Token: 0x06000425 RID: 1061 RVA: 0x000128CB File Offset: 0x00010CCB
	public static int cat9_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat9_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat9_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000426 RID: 1062 RVA: 0x000128DD File Offset: 0x00010CDD
	// (set) Token: 0x06000427 RID: 1063 RVA: 0x000128EA File Offset: 0x00010CEA
	public static int cat10_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat10_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat10_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000428 RID: 1064 RVA: 0x000128FC File Offset: 0x00010CFC
	// (set) Token: 0x06000429 RID: 1065 RVA: 0x00012909 File Offset: 0x00010D09
	public static int cat11_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat11_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat11_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001291B File Offset: 0x00010D1B
	// (set) Token: 0x0600042B RID: 1067 RVA: 0x00012928 File Offset: 0x00010D28
	public static int cat12_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat12_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat12_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001293A File Offset: 0x00010D3A
	// (set) Token: 0x0600042D RID: 1069 RVA: 0x00012947 File Offset: 0x00010D47
	public static int cat13_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat13_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat13_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600042E RID: 1070 RVA: 0x00012959 File Offset: 0x00010D59
	// (set) Token: 0x0600042F RID: 1071 RVA: 0x00012966 File Offset: 0x00010D66
	public static int cat14_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat14_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat14_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000430 RID: 1072 RVA: 0x00012978 File Offset: 0x00010D78
	// (set) Token: 0x06000431 RID: 1073 RVA: 0x00012985 File Offset: 0x00010D85
	public static int cat15_unlocked
	{
		get
		{
			return PlayerPrefs.GetInt("cat15_unlocked", 0);
		}
		set
		{
			PlayerPrefs.SetInt("cat15_unlocked", value);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x04000253 RID: 595
	public static bool categoryOne;

	// Token: 0x04000254 RID: 596
	public static bool categoryTwo;

	// Token: 0x04000255 RID: 597
	public static bool categoryThree;

	// Token: 0x04000256 RID: 598
	public static bool categoryFour;

	// Token: 0x04000257 RID: 599
	public static bool categoryFive;

	// Token: 0x04000258 RID: 600
	public static bool categorySix;

	// Token: 0x04000259 RID: 601
	public static bool categorySeven;

	// Token: 0x0400025A RID: 602
	public static bool categoryEight;

	// Token: 0x0400025B RID: 603
	public static bool categoryNine;

	// Token: 0x0400025C RID: 604
	public static bool categoryTen;

	// Token: 0x0400025D RID: 605
	public static bool categoryEleven;

	// Token: 0x0400025E RID: 606
	public static bool categoryTwelve;

	// Token: 0x0400025F RID: 607
	public static bool categoryThirteen;

	// Token: 0x04000260 RID: 608
	public static bool categoryFourteen;

	// Token: 0x04000261 RID: 609
	public static bool categoryFifteen;

	// Token: 0x04000262 RID: 610
	public static string currentLvlId;

	// Token: 0x04000263 RID: 611
	public static string AmazonRateUs = "amzn://apps/android?p=" + Application.bundleIdentifier;

	// Token: 0x04000264 RID: 612
	public static string amazonMoreApps = "amzn://apps/android?p=" + Application.bundleIdentifier + "&showAll=1";

	// Token: 0x04000265 RID: 613
	public static string GoogleRateUs = "market://details?id=" + Application.bundleIdentifier;

	// Token: 0x04000266 RID: 614
	public static string FacebookWeb = "https://www.facebook.com/Vector-Entertainment-557257244422350/";

	// Token: 0x04000267 RID: 615
	public static string FacebookApp = "fb://page/557257244422350";

	// Token: 0x04000268 RID: 616
	public static string IOSRateUs = string.Empty;
}
