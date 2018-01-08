using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000010 RID: 16
public class testPan : MonoBehaviour
{
	// Token: 0x06000057 RID: 87 RVA: 0x00004D82 File Offset: 0x00003182
	private void Awake()
	{
		this.cam = base.GetComponent<Camera>();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00004D90 File Offset: 0x00003190
	private void Update()
	{
		if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
		{
			this.HandleTouch();
		}
		else
		{
			this.HandleMouse();
		}
		if (this.cam.orthographicSize < 30f)
		{
			this.gridmanager.SetActive(true);
			this.TextCanvas.GetComponent<Canvas>().enabled = true;
		}
		else
		{
			this.gridmanager.SetActive(false);
			this.TextCanvas.GetComponent<Canvas>().enabled = false;
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00004E18 File Offset: 0x00003218
	private void HandleTouch()
	{
		int touchCount = Input.touchCount;
		if (touchCount != 1)
		{
			if (touchCount != 2)
			{
				this.wasZoomingLastFrame = false;
			}
			else
			{
				Vector2[] array = new Vector2[]
				{
					Input.GetTouch(0).position,
					Input.GetTouch(1).position
				};
				if (!this.wasZoomingLastFrame)
				{
					this.lastZoomPositions = array;
					this.wasZoomingLastFrame = true;
				}
				else
				{
					float num = Vector2.Distance(array[0], array[1]);
					float num2 = Vector2.Distance(this.lastZoomPositions[0], this.lastZoomPositions[1]);
					float offset = num - num2;
					this.ZoomCamera(offset, testPan.ZoomSpeedTouch);
					this.lastZoomPositions = array;
				}
			}
		}
		else
		{
			this.wasZoomingLastFrame = false;
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				this.lastPanPosition = touch.position;
				this.panFingerId = touch.fingerId;
			}
			else if (touch.fingerId == this.panFingerId && touch.phase == TouchPhase.Moved)
			{
				if (EventSystem.current.currentSelectedGameObject == null)
				{
					this.PanCamera(touch.position);
				}
				else if (!EventSystem.current.currentSelectedGameObject.tag.Equals("ColorButton"))
				{
					Debug.Log("Moved:My GameObject: " + EventSystem.current.currentSelectedGameObject.name);
					Debug.Log("Moved:My GameObjectTag: " + EventSystem.current.currentSelectedGameObject.tag);
					this.PanCamera(touch.position);
				}
			}
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00005008 File Offset: 0x00003408
	private void HandleMouse()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.lastPanPosition = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
			this.PanCamera(Input.mousePosition);
		}
		float axis = Input.GetAxis("Mouse ScrollWheel");
		this.ZoomCamera(axis, testPan.ZoomSpeedMouse);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00005060 File Offset: 0x00003460
	private void PanCamera(Vector3 newPanPosition)
	{
		if (Vector2.Distance(this.lastPanPosition, newPanPosition) > 10f)
		{
			Vector3 vector = this.cam.ScreenToViewportPoint(this.lastPanPosition - newPanPosition);
			Vector3 translation = new Vector3(vector.x * testPan.PanSpeed, vector.y * testPan.PanSpeed, 0f);
			base.transform.Translate(translation, Space.World);
			Vector3 position = base.transform.position;
			position.x = Mathf.Clamp(base.transform.position.x, -(textwritten.tempwidth / 2f), textwritten.tempwidth / 2f);
			position.y = Mathf.Clamp(base.transform.position.y, -(textwritten.tempheight / 2f), textwritten.tempheight / 2f);
			base.transform.position = position;
			this.lastPanPosition = newPanPosition;
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00005164 File Offset: 0x00003564
	private void ZoomCamera(float offset, float speed)
	{
		if (offset == 0f)
		{
			return;
		}
		this.cam.orthographicSize = Mathf.Clamp(this.cam.orthographicSize - offset * speed, testPan.ZoomBounds[0], testPan.ZoomBounds[1]);
	}

	// Token: 0x04000047 RID: 71
	public GameObject gridmanager;

	// Token: 0x04000048 RID: 72
	public GameObject TextCanvas;

	// Token: 0x04000049 RID: 73
	private static readonly float PanSpeed = 20f;

	// Token: 0x0400004A RID: 74
	private static readonly float ZoomSpeedTouch = 0.1f;

	// Token: 0x0400004B RID: 75
	private static readonly float ZoomSpeedMouse = 0.5f;

	// Token: 0x0400004C RID: 76
	private static readonly float[] BoundsX = new float[]
	{
		-15f,
		15f
	};

	// Token: 0x0400004D RID: 77
	private static readonly float[] BoundsY = new float[]
	{
		-15f,
		15f
	};

	// Token: 0x0400004E RID: 78
	private static readonly float[] ZoomBounds = new float[]
	{
		10f,
		85f
	};

	// Token: 0x0400004F RID: 79
	private Camera cam;

	// Token: 0x04000050 RID: 80
	private Vector3 lastPanPosition;

	// Token: 0x04000051 RID: 81
	private int panFingerId;

	// Token: 0x04000052 RID: 82
	private bool wasZoomingLastFrame;

	// Token: 0x04000053 RID: 83
	private Vector2[] lastZoomPositions;
}
