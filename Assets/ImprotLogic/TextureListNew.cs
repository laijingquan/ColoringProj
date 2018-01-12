using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class TextureListNew : MonoBehaviour
{
	// Token: 0x06000597 RID: 1431 RVA: 0x0001FD84 File Offset: 0x0001E184
	private void Awake()
	{
		int num = HomeScript.selectedimage + 1;
		this.ColorImage.sprite = (Resources.Load("Original/" + num + "Pic", typeof(Sprite)) as Sprite);
		this.GreyImage.sprite = (Resources.Load("Grayscale/" + num + "PicGrey", typeof(Sprite)) as Sprite);
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001FE04 File Offset: 0x0001E204
	private void Start()
	{
		if (this.GreyImage.sprite.texture.width % 2 != 0)
		{
			this.ImageBoard.transform.position = new Vector3(0.5f, this.ImageBoard.transform.position.y, this.ImageBoard.transform.position.z);
		}
		if (this.GreyImage.sprite.texture.height % 2 != 0)
		{
			this.ImageBoard.transform.position = new Vector3(this.ImageBoard.transform.position.x, 0.5f, this.ImageBoard.transform.position.z);
		}
	}

	// Token: 0x040002F2 RID: 754
	public GameObject ImageBoard;

	// Token: 0x040002F3 RID: 755
	public SpriteRenderer ColorImage;

	// Token: 0x040002F4 RID: 756
	public SpriteRenderer GreyImage;
}
