using System;

namespace GameAnalyticsSDK.Setup
{
	// Token: 0x0200003D RID: 61
	public class Game
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000BF13 File Offset: 0x0000A313
		public Game(string name, int id, string gameKey, string secretKey)
		{
			this.Name = name;
			this.ID = id;
			this.GameKey = gameKey;
			this.SecretKey = secretKey;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000BF38 File Offset: 0x0000A338
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000BF40 File Offset: 0x0000A340
		public string Name { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000BF49 File Offset: 0x0000A349
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000BF51 File Offset: 0x0000A351
		public int ID { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000BF5A File Offset: 0x0000A35A
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000BF62 File Offset: 0x0000A362
		public string GameKey { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000BF6B File Offset: 0x0000A36B
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000BF73 File Offset: 0x0000A373
		public string SecretKey { get; private set; }
	}
}
