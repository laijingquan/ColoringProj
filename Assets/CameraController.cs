using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class CameraController : MonoBehaviour
{
	// Token: 0x06000032 RID: 50 RVA: 0x000031A0 File Offset: 0x000015A0
	private void Update()
	{
		if (Input.touchCount == 2)
		{
			this.TouchCount = 2;
		}
		else if (Input.touchCount == 1)
		{
			this.TouchCount = 1;
		}
		int touchCount = this.TouchCount;
		if (touchCount == 2)
		{
			this.zoom_camera();
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000031FC File Offset: 0x000015FC
	private void rotate_camera()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.R = base.transform.position.magnitude;
			this.startPosition = Input.mousePosition;
		}
		else if (!Input.GetMouseButtonUp(0))
		{
			if (Input.GetMouseButton(0))
			{
				if (this.startPosition.x < Input.mousePosition.x)
				{
					base.transform.position -= base.transform.right * 10f * Time.deltaTime;
				}
				if (this.startPosition.x > Input.mousePosition.x)
				{
					base.transform.position += base.transform.right * 10f * Time.deltaTime;
				}
				if (this.startPosition.y < Input.mousePosition.y)
				{
					base.transform.position -= base.transform.up * 10f * Time.deltaTime;
				}
				if (this.startPosition.y > Input.mousePosition.y)
				{
					base.transform.position += base.transform.up * 10f * Time.deltaTime;
				}
			}
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000033A4 File Offset: 0x000017A4
	private void zoom_camera()
	{
		Touch touch = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);
		if (touch.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
		{
			Vector2 a = touch.position - touch.deltaPosition;
			Vector2 b = touch2.position - touch2.deltaPosition;
			float magnitude = (a - b).magnitude;
			float magnitude2 = (touch.position - touch2.position).magnitude;
			float d = (magnitude - magnitude2) / 10f;
			base.transform.position -= base.transform.forward * d * this.perspectiveZoomSpeed * Time.deltaTime * 50f;
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003488 File Offset: 0x00001888
	private IEnumerator RotateCamera(Vector3 direction)
	{
		for (float magnitude = Mathf.Abs(this.input.y); magnitude > 0f; magnitude -= 3f)
		{
			base.transform.parent.RotateAround(Vector3.zero, new Vector3(0f, this.input.y, 0f), magnitude * Time.deltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000034A4 File Offset: 0x000018A4
	private void LateUpdate()
	{
		this.zvalue = Mathf.Clamp(base.transform.position.z, -100f, -5f);
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, this.zvalue);
		if (base.transform.position.z > -50f)
		{
			this.gridmanager.SetActive(true);
			this.TextCanvas.SetActive(true);
		}
		else
		{
			this.gridmanager.SetActive(false);
			this.TextCanvas.SetActive(false);
		}
	}

	// Token: 0x04000020 RID: 32
	private float xAngle;

	// Token: 0x04000021 RID: 33
	private Vector3 startPosition;

	// Token: 0x04000022 RID: 34
	public float perspectiveZoomSpeed = 0.5f;

	// Token: 0x04000023 RID: 35
	private Vector3 input;

	// Token: 0x04000024 RID: 36
	private float R = 10f;

	// Token: 0x04000025 RID: 37
	private int TouchCount;

	// Token: 0x04000026 RID: 38
	private float zvalue;

	// Token: 0x04000027 RID: 39
	public GameObject gridmanager;

	// Token: 0x04000028 RID: 40
	public GameObject TextCanvas;
}
