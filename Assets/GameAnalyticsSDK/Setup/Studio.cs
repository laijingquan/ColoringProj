using System;
using System.Collections.Generic;

namespace GameAnalyticsSDK.Setup
{
	// Token: 0x0200003C RID: 60
	public class Studio
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000BD40 File Offset: 0x0000A140
		public Studio(string name, string id, List<Game> games)
		{
			this.Name = name;
			this.ID = id;
			this.Games = games;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000BD5D File Offset: 0x0000A15D
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000BD65 File Offset: 0x0000A165
		public string Name { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000BD6E File Offset: 0x0000A16E
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000BD76 File Offset: 0x0000A176
		public string ID { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000BD7F File Offset: 0x0000A17F
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000BD87 File Offset: 0x0000A187
		public List<Game> Games { get; private set; }

		// Token: 0x06000196 RID: 406 RVA: 0x0000BD90 File Offset: 0x0000A190
		public static string[] GetStudioNames(List<Studio> studios, bool addFirstEmpty = true)
		{
			if (studios == null)
			{
				return new string[]
				{
					"-"
				};
			}
			if (addFirstEmpty)
			{
				string[] array = new string[studios.Count + 1];
				array[0] = "-";
				string text = string.Empty;
				for (int i = 0; i < studios.Count; i++)
				{
					array[i + 1] = studios[i].Name + text;
					text += " ";
				}
				return array;
			}
			string[] array2 = new string[studios.Count];
			string text2 = string.Empty;
			for (int j = 0; j < studios.Count; j++)
			{
				array2[j] = studios[j].Name + text2;
				text2 += " ";
			}
			return array2;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000BE68 File Offset: 0x0000A268
		public static string[] GetGameNames(int index, List<Studio> studios)
		{
			if (studios == null || studios[index].Games == null)
			{
				return new string[]
				{
					"-"
				};
			}
			string[] array = new string[studios[index].Games.Count + 1];
			array[0] = "-";
			string text = string.Empty;
			for (int i = 0; i < studios[index].Games.Count; i++)
			{
				array[i + 1] = studios[index].Games[i].Name + text;
				text += " ";
			}
			return array;
		}
	}
}
