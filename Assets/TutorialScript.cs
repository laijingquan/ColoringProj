using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000083 RID: 131
public class TutorialScript : MonoBehaviour
{
	// Token: 0x0600059A RID: 1434 RVA: 0x0001FEE5 File Offset: 0x0001E2E5
	private void Start()
	{
		base.StartCoroutine(this.MovingTowardBrush());
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0001FEF4 File Offset: 0x0001E2F4
	private void Update()
	{
		if (this._tutorialState == TutorialScript.TutorialState.Move_Toward_Brush_Icon)
		{
			this.hand.transform.position = Vector3.Lerp(this.hand.transform.position, this.Brush.transform.position, Time.deltaTime * this.speed);
			if (Vector3.Distance(this.hand.transform.position, this.Brush.transform.position) < 1f)
			{
				this._tutorialState = TutorialScript.TutorialState.None;
				base.StartCoroutine(this.PressBrush());
			}
		}
		else if (this._tutorialState == TutorialScript.TutorialState.Move_Toward_Image)
		{
			this.hand.transform.position = Vector3.Lerp(this.hand.transform.position, this.Pic.transform.position, Time.deltaTime * this.speed);
			if (Vector3.Distance(this.hand.transform.position, this.Pic.transform.position) < 0.5f)
			{
				this._tutorialState = TutorialScript.TutorialState.Brush_Painting;
				base.StartCoroutine(this.Painting());
				base.StartCoroutine(this.switchImages());
			}
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00020034 File Offset: 0x0001E434
	private IEnumerator MovingTowardBrush()
	{
		this.Brush.sprite = this.brushes[0];
		yield return new WaitForSeconds(0.5f);
		this.index = 0;
		this.Pic.sprite = this.Images[this.index];
		this._tutorialState = TutorialScript.TutorialState.Move_Toward_Brush_Icon;
		yield break;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00020050 File Offset: 0x0001E450
	private IEnumerator PressBrush()
	{
		this.Brush.sprite = this.brushes[1];
		this.hand.sprite = this.hands[1];
		yield return new WaitForSeconds(0.5f);
		this.hand.sprite = this.hands[0];
		this._tutorialState = TutorialScript.TutorialState.Move_Toward_Image;
		yield break;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0002006C File Offset: 0x0001E46C
	private IEnumerator Painting()
	{
		iTween.MoveTo(this.hand.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.EndObj.transform.position,
			"time",
			1.6f,
			"easetype",
			iTween.EaseType.linear
		}));
		yield return new WaitForSeconds(0f);
		yield break;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00020088 File Offset: 0x0001E488
	private IEnumerator switchImages()
	{
		this.index++;
		yield return new WaitForSeconds(0.2f);
		if (this.index < this.Images.Length)
		{
			this.Pic.sprite = this.Images[this.index];
			base.StartCoroutine(this.switchImages());
		}
		else
		{
			base.StartCoroutine(this.MovingTowardBrush());
		}
		yield break;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x000200A3 File Offset: 0x0001E4A3
	public void hideTutorial(Toggle t)
	{
		if (t.isOn)
		{
			PlayerPrefs.SetInt("ShowTutorial", 0);
		}
		else
		{
			PlayerPrefs.SetInt("ShowTutorial", 1);
		}
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x000200CB File Offset: 0x0001E4CB
	public void GotItTutorial()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040002F5 RID: 757
	public Image hand;

	// Token: 0x040002F6 RID: 758
	public Image Brush;

	// Token: 0x040002F7 RID: 759
	public Sprite[] brushes;

	// Token: 0x040002F8 RID: 760
	public Sprite[] hands;

	// Token: 0x040002F9 RID: 761
	public Sprite[] Images;

	// Token: 0x040002FA RID: 762
	public float speed;

	// Token: 0x040002FB RID: 763
	public Image Pic;

	// Token: 0x040002FC RID: 764
	private int index;

	// Token: 0x040002FD RID: 765
	public GameObject EndObj;

	// Token: 0x040002FE RID: 766
	public bool Painted;

	// Token: 0x040002FF RID: 767
	public TutorialScript.TutorialState _tutorialState;

	// Token: 0x02000084 RID: 132
	public enum TutorialState
	{
		// Token: 0x04000301 RID: 769
		None,
		// Token: 0x04000302 RID: 770
		Move_Toward_Brush_Icon,
		// Token: 0x04000303 RID: 771
		Move_Toward_Image,
		// Token: 0x04000304 RID: 772
		Brush_Painting,
		// Token: 0x04000305 RID: 773
		Move_Image,
		// Token: 0x04000306 RID: 774
		Pixel_Color
	}
}
