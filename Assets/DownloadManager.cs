using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class DownloadManager : Singleton<DownloadManager>
{
	// Token: 0x060005A3 RID: 1443 RVA: 0x000207C0 File Offset: 0x0001EBC0
	public void DownloadTexture2dAsync(string url, Action<Texture2D> callback)
	{
		base.StartCoroutine(this.DownloadTexture2D(url, callback));
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x000207D4 File Offset: 0x0001EBD4
	private IEnumerator DownloadTexture2D(string url, Action<Texture2D> callback)
	{
		if (url.IsUrl())
		{
			while (this.imageDownloader != null)
			{
				yield return null;
			}
			this.imageDownloader = new WWW(url);
			yield return this.imageDownloader;
			if (!string.IsNullOrEmpty(this.imageDownloader.error))
			{
				Debug.LogErrorFormat("error : {0} retrieving URL = {1}", new object[]
				{
					this.imageDownloader.error,
					url
				});
			}
			else
			{
				Texture2D texture2D = new Texture2D(this.imageDownloader.texture.width, this.imageDownloader.texture.height, this.imageDownloader.texture.format, false);
				this.imageDownloader.LoadImageIntoTexture(texture2D);
				callback(texture2D);
			}
			this.imageDownloader.Dispose();
			this.imageDownloader = null;
		}
		else
		{
			Debug.LogErrorFormat("invalid url {0}", new object[]
			{
				url
			});
		}
		yield break;
	}

	// Token: 0x04000307 RID: 775
	private WWW imageDownloader;
}
