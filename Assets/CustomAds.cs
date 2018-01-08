using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class CustomAds : MonoBehaviour
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001212B File Offset: 0x0001052B
	// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00012138 File Offset: 0x00010538
	public int FirstHolder
	{
		get
		{
			return PlayerPrefs.GetInt("FirstHolder", 0);
		}
		set
		{
			PlayerPrefs.SetInt("FirstHolder", value);
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00012145 File Offset: 0x00010545
	// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00012152 File Offset: 0x00010552
	public int SecondHolder
	{
		get
		{
			return PlayerPrefs.GetInt("SecondHolder", 1);
		}
		set
		{
			PlayerPrefs.SetInt("SecondHolder", value);
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001215F File Offset: 0x0001055F
	// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0001216C File Offset: 0x0001056C
	public int ThirdHolder
	{
		get
		{
			return PlayerPrefs.GetInt("ThirdHolder", 2);
		}
		set
		{
			PlayerPrefs.SetInt("ThirdHolder", value);
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00012179 File Offset: 0x00010579
	// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00012186 File Offset: 0x00010586
	public int ForthHolder
	{
		get
		{
			return PlayerPrefs.GetInt("ForthHolder", 3);
		}
		set
		{
			PlayerPrefs.SetInt("ForthHolder", value);
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00012193 File Offset: 0x00010593
	// (set) Token: 0x060003F9 RID: 1017 RVA: 0x000121A0 File Offset: 0x000105A0
	public int NextIndex
	{
		get
		{
			return PlayerPrefs.GetInt("NextIndex", 4);
		}
		set
		{
			PlayerPrefs.SetInt("NextIndex", value);
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x000121B0 File Offset: 0x000105B0
	private void Start()
	{
		this.script.applinks[0] = this.appUrls[this.FirstHolder];
		this.script.applinks[1] = this.appUrls[this.SecondHolder];
		this.script.applinks[2] = this.appUrls[this.ThirdHolder];
		this.script.applinks[3] = this.appUrls[this.ForthHolder];
		this.script.appIcons[0] = this.appIcons[this.FirstHolder];
		this.script.appIcons[1] = this.appIcons[this.SecondHolder];
		this.script.appIcons[2] = this.appIcons[this.ThirdHolder];
		this.script.appIcons[3] = this.appIcons[this.ForthHolder];
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x000122CD File Offset: 0x000106CD
	private void Update()
	{
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x000122D0 File Offset: 0x000106D0
	public void switchAds(int holder_id)
	{
		switch (holder_id + 4)
		{
		case 0:
			if (!this.script.applinks.Contains(this.appUrls[this.NextIndex]) && this.NextIndex < 6)
			{
				this.script.applinks[0] = this.appUrls[this.NextIndex];
				this.script.appIcons[0] = this.appIcons[this.NextIndex];
				this.FirstHolder = this.NextIndex;
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
			}
			else
			{
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
				this.switchAds(holder_id);
			}
			break;
		case 1:
			if (!this.script.applinks.Contains(this.appUrls[this.NextIndex]) && this.NextIndex < 6)
			{
				this.script.applinks[1] = this.appUrls[this.NextIndex];
				this.script.appIcons[1] = this.appIcons[this.NextIndex];
				this.SecondHolder = this.NextIndex;
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
			}
			else
			{
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
				this.switchAds(holder_id);
			}
			break;
		case 2:
			if (!this.script.applinks.Contains(this.appUrls[this.NextIndex]))
			{
				this.script.applinks[2] = this.appUrls[this.NextIndex];
				this.script.appIcons[2] = this.appIcons[this.NextIndex];
				this.ThirdHolder = this.NextIndex;
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
			}
			else
			{
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
				this.switchAds(holder_id);
			}
			break;
		case 3:
			if (!this.script.applinks.Contains(this.appUrls[this.NextIndex]))
			{
				this.script.applinks[3] = this.appUrls[this.NextIndex];
				this.script.appIcons[3] = this.appIcons[this.NextIndex];
				this.ForthHolder = this.NextIndex;
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
			}
			else
			{
				this.NextIndex++;
				if (this.NextIndex >= this.appUrls.Count)
				{
					this.NextIndex = 0;
				}
				this.switchAds(holder_id);
			}
			break;
		}
	}

	// Token: 0x04000248 RID: 584
	public List<string> appUrls = new List<string>();

	// Token: 0x04000249 RID: 585
	public List<Sprite> appIcons = new List<Sprite>();

	// Token: 0x0400024A RID: 586
	private static int currentAdIndex = -1;

	// Token: 0x0400024B RID: 587
	public HomeScript script;

	// Token: 0x0400024C RID: 588
	private int firstHolder;

	// Token: 0x0400024D RID: 589
	private int secondHolder;

	// Token: 0x0400024E RID: 590
	private int thirdHolder;

	// Token: 0x0400024F RID: 591
	private int forthHolder;

	// Token: 0x04000250 RID: 592
	private int nextIndex;
}
