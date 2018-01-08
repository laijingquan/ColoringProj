using System;
using System.Collections.Generic;
using GoogleMobileAds.Api.Mediation;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000046 RID: 70
	public class AdRequest
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000D4F4 File Offset: 0x0000B8F4
		private AdRequest(AdRequest.Builder builder)
		{
			this.TestDevices = new List<string>(builder.TestDevices);
			this.Keywords = new HashSet<string>(builder.Keywords);
			this.Birthday = builder.Birthday;
			this.Gender = builder.Gender;
			this.TagForChildDirectedTreatment = builder.ChildDirectedTreatmentTag;
			this.Extras = new Dictionary<string, string>(builder.Extras);
			this.MediationExtras = builder.MediationExtras;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000D56A File Offset: 0x0000B96A
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000D572 File Offset: 0x0000B972
		public List<string> TestDevices { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000D57B File Offset: 0x0000B97B
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000D583 File Offset: 0x0000B983
		public HashSet<string> Keywords { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000D58C File Offset: 0x0000B98C
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000D594 File Offset: 0x0000B994
		public DateTime? Birthday { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000D59D File Offset: 0x0000B99D
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000D5A5 File Offset: 0x0000B9A5
		public Gender? Gender { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000D5AE File Offset: 0x0000B9AE
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000D5B6 File Offset: 0x0000B9B6
		public bool? TagForChildDirectedTreatment { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000D5BF File Offset: 0x0000B9BF
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000D5C7 File Offset: 0x0000B9C7
		public Dictionary<string, string> Extras { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000D5D0 File Offset: 0x0000B9D0
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000D5D8 File Offset: 0x0000B9D8
		public List<MediationExtras> MediationExtras { get; private set; }

		// Token: 0x040001A8 RID: 424
		public const string Version = "3.4.0";

		// Token: 0x040001A9 RID: 425
		public const string TestDeviceSimulator = "SIMULATOR";

		// Token: 0x02000047 RID: 71
		public class Builder
		{
			// Token: 0x06000229 RID: 553 RVA: 0x0000D5E4 File Offset: 0x0000B9E4
			public Builder()
			{
				this.TestDevices = new List<string>();
				this.Keywords = new HashSet<string>();
				this.Birthday = null;
				this.Gender = null;
				this.ChildDirectedTreatmentTag = null;
				this.Extras = new Dictionary<string, string>();
				this.MediationExtras = new List<MediationExtras>();
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x0600022A RID: 554 RVA: 0x0000D650 File Offset: 0x0000BA50
			// (set) Token: 0x0600022B RID: 555 RVA: 0x0000D658 File Offset: 0x0000BA58
			internal List<string> TestDevices { get; private set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x0600022C RID: 556 RVA: 0x0000D661 File Offset: 0x0000BA61
			// (set) Token: 0x0600022D RID: 557 RVA: 0x0000D669 File Offset: 0x0000BA69
			internal HashSet<string> Keywords { get; private set; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x0600022E RID: 558 RVA: 0x0000D672 File Offset: 0x0000BA72
			// (set) Token: 0x0600022F RID: 559 RVA: 0x0000D67A File Offset: 0x0000BA7A
			internal DateTime? Birthday { get; private set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000230 RID: 560 RVA: 0x0000D683 File Offset: 0x0000BA83
			// (set) Token: 0x06000231 RID: 561 RVA: 0x0000D68B File Offset: 0x0000BA8B
			internal Gender? Gender { get; private set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000232 RID: 562 RVA: 0x0000D694 File Offset: 0x0000BA94
			// (set) Token: 0x06000233 RID: 563 RVA: 0x0000D69C File Offset: 0x0000BA9C
			internal bool? ChildDirectedTreatmentTag { get; private set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000234 RID: 564 RVA: 0x0000D6A5 File Offset: 0x0000BAA5
			// (set) Token: 0x06000235 RID: 565 RVA: 0x0000D6AD File Offset: 0x0000BAAD
			internal Dictionary<string, string> Extras { get; private set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000236 RID: 566 RVA: 0x0000D6B6 File Offset: 0x0000BAB6
			// (set) Token: 0x06000237 RID: 567 RVA: 0x0000D6BE File Offset: 0x0000BABE
			internal List<MediationExtras> MediationExtras { get; private set; }

			// Token: 0x06000238 RID: 568 RVA: 0x0000D6C7 File Offset: 0x0000BAC7
			public AdRequest.Builder AddKeyword(string keyword)
			{
				this.Keywords.Add(keyword);
				return this;
			}

			// Token: 0x06000239 RID: 569 RVA: 0x0000D6D7 File Offset: 0x0000BAD7
			public AdRequest.Builder AddTestDevice(string deviceId)
			{
				this.TestDevices.Add(deviceId);
				return this;
			}

			// Token: 0x0600023A RID: 570 RVA: 0x0000D6E6 File Offset: 0x0000BAE6
			public AdRequest Build()
			{
				return new AdRequest(this);
			}

			// Token: 0x0600023B RID: 571 RVA: 0x0000D6EE File Offset: 0x0000BAEE
			public AdRequest.Builder SetBirthday(DateTime birthday)
			{
				this.Birthday = new DateTime?(birthday);
				return this;
			}

			// Token: 0x0600023C RID: 572 RVA: 0x0000D6FD File Offset: 0x0000BAFD
			public AdRequest.Builder SetGender(Gender gender)
			{
				this.Gender = new Gender?(gender);
				return this;
			}

			// Token: 0x0600023D RID: 573 RVA: 0x0000D70C File Offset: 0x0000BB0C
			public AdRequest.Builder AddMediationExtras(MediationExtras extras)
			{
				this.MediationExtras.Add(extras);
				return this;
			}

			// Token: 0x0600023E RID: 574 RVA: 0x0000D71B File Offset: 0x0000BB1B
			public AdRequest.Builder TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
			{
				this.ChildDirectedTreatmentTag = new bool?(tagForChildDirectedTreatment);
				return this;
			}

			// Token: 0x0600023F RID: 575 RVA: 0x0000D72A File Offset: 0x0000BB2A
			public AdRequest.Builder AddExtra(string key, string value)
			{
				this.Extras.Add(key, value);
				return this;
			}
		}
	}
}
